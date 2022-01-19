using MediatR;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Parse;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand.Maps;
using RecruitingStaffWebApp.Services.DocParse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Parse
{
    public class CreateParsedAnswersAndCandidateDataHandler : IRequestHandler<CreateParsedAnswersAndCandidateDataCommand, bool>
    {
        private readonly IMediator _mediator;
        private ParsedData parsedData;
        private Candidate _candidate;

        public CreateParsedAnswersAndCandidateDataHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(CreateParsedAnswersAndCandidateDataCommand request, CancellationToken cancellationToken)
        {
            parsedData = request.ParsedData;
            Vacancy vacancy = new()
            {
                Name = request.ParsedData.Vacancy.Name,
            };
            await _mediator.Send(new CreateOrChangeEntityCommand<Vacancy>(vacancy), cancellationToken);
            Questionnaire questionnaire = new()
            {
                Name = request.ParsedData.Questionnaire.Name,
                VacancyId = vacancy.Id,
            };
            await _mediator.Send(new CreateOrChangeQuestionnaireCommand(questionnaire), cancellationToken);
            _candidate = await CreateCandidate();
            _candidate.Id = request.ParsedData.CandidateId;
            await _mediator.Send(new CreateOrChangeCandidateCommand(_candidate), cancellationToken);
            await _mediator.Send(new CreateMapCommand<CandidateQuestionnaire>(_candidate.Id, questionnaire.Id), cancellationToken);
            await _mediator.Send(new CreateMapCommand<CandidateVacancy>(_candidate.Id, vacancy.Id), cancellationToken);

            await SaveAnswers(questionnaire.Id, cancellationToken);
            request.ParsedData.FileSource = (await CreateCandidateDocument(_candidate, questionnaire.Id)).Name;
            return true;
        }

        private async Task SaveAnswers(int questionnaireId, CancellationToken cancellationToken)
        {
            foreach (var questionCategoryItem in parsedData.Questionnaire.ChildElements)
            {
                QuestionCategory questionCategory = new()
                {
                    Name = questionCategoryItem.Name,
                    QuestionnaireId = questionnaireId,
                };
                await _mediator.Send(new CreateOrChangeEntityCommand<QuestionCategory>(questionCategory), cancellationToken);
                foreach (var questionItem in questionCategoryItem.ChildElements)
                {
                    Question question = new()
                    {
                        Name = questionItem.Name,
                        QuestionCategoryId = questionCategory.Id,
                    };
                    await _mediator.Send(new CreateOrChangeEntityCommand<Question>(question), cancellationToken);
                    foreach (var answerItem in questionItem.ChildElements)
                    {
                        Answer answer = new();
                        answer.QuestionId = question.Id;
                        answer.CandidateId = _candidate.Id;
                        foreach (var property in typeof(Answer).GetProperties())
                        {
                            if (answerItem.Properties.ContainsKey(property.Name))
                            {
                                object answerValue = answerItem.Properties[property.Name];
                                if (property.PropertyType != typeof(string))
                                {
                                    answerValue = Convert.ChangeType(answerValue, property.PropertyType);
                                }
                                property.SetValue(answer, answerValue);
                            }
                        }
                        await _mediator.Send(new CreateEntityCommand<Answer>(answer), cancellationToken);
                    }
                }
            }
        }

        private async Task<RecruitingStaffWebAppFile> CreateCandidateDocument(Candidate candidate, int questionnaireId)
        {
            RecruitingStaffWebAppFile file = new()
            {
                Name = $"{candidate.Id}.{candidate.Name}{parsedData.FileExtension}",
                FileType = FileType.CompletedQuestionnaire,
                CandidateId = candidate.Id,
                QuestionnaireId = questionnaireId,
            };
            await _mediator.Send(new CreateEntityCommand<RecruitingStaffWebAppFile>(file));
            return file;
        }

        private async Task<Candidate> CreateCandidate()
        {
            Candidate candidate = new();
            if (parsedData.Candidate == null)
            {
                return candidate;
            }
            candidate.Educations = await AssignValuesToPropertiesListObjects(
                parsedData.Candidate.Educations, () => new Education());
            candidate.Documents = await AssignValuesToPropertiesCompositeObject(
                parsedData.Candidate.Documents, "Name", () => new RecruitingStaffWebAppFile());
            candidate.Kids = await AssignValuesToPropertiesListObjects(
                parsedData.Candidate.Kids, () => new Kid());
            candidate.PreviousJobs = await AssignValuesToPropertiesListObjects(
                parsedData.Candidate.PreviousJobs, () => new PreviousJobPlacement());
            for (int i = 0; i < candidate.PreviousJobs.Count; i++)
            {
                candidate.PreviousJobs[i].Recommenders = new();
                foreach (var parsedRecommender in parsedData.Candidate.PreviousJobs[i].Recommenders)
                {
                    Recommender recommender = new();
                    AssignValuesToProperties(recommender, parsedRecommender);
                    candidate.PreviousJobs[i].Recommenders.Add(recommender);
                }
            }
            AssignValuesToProperties(candidate, parsedData.Candidate);
            return candidate;
        }

        public static T AssignValuesToProperties<T, V>(T obj, V value)
        {
            var objProperties = obj.GetType().GetProperties();
            var properties = value
                .GetType()
                .GetProperties()
                .Where(p => p.PropertyType.Name != "List`1");
            foreach (var valueProperty in properties)
            {
                var objProperty = objProperties
                    .Where(p => p.Name == valueProperty.Name
                    && p.PropertyType.Name == valueProperty.PropertyType.Name)
                    .FirstOrDefault();
                if (objProperty != null)
                {
                    objProperty.SetValue(obj, valueProperty.GetValue(value));
                }
            }
            return obj;
        }

        public static Task<List<T>> AssignValuesToPropertiesListObjects<T, V>(List<V> values, Func<T> createObj)
        {
            if (values == null)
            {
                return Task.FromResult(new List<T>());
            }
            List<T> objects = new();
            foreach (var value in values)
            {
                objects.Add(AssignValuesToProperties(createObj(), value));
            }
            return Task.FromResult(objects);
        }



        public static Task<List<T>> AssignValuesToPropertiesCompositeObject<T>(List<string> values, string propertyName, Func<T> createObj)
        {
            List<T> objects = new();
            if (values == null)
            {
                return null;
            }
            var property = typeof(T).GetProperty(propertyName);
            if (property == null) { return null; }
            foreach (var value in values)
            {
                var obj = createObj();
                property.SetValue(obj, value);
                objects.Add(obj);
            }
            return Task.FromResult(objects);
        }
    }
}
