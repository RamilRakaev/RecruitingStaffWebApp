using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Parse;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
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
        private RecruitingStaffWebAppFile _file;
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
            await _mediator.Send(new CreateOrChangeVacancyCommand(vacancy), cancellationToken);
            Questionnaire questionnaire = new()
            {
                Name = request.ParsedData.Questionnaire.Name,
                VacancyId = vacancy.Id,
            };
            await _mediator.Send(new CreateOrChangeQuestionnaireCommand(questionnaire), cancellationToken);
            _candidate = await CreateCandidate();
            await _mediator.Send(new CreateOrChangeCandidateCommand(_candidate, vacancy.Id, questionnaire.Id), cancellationToken);

            await SaveAnswers(questionnaire.Id, cancellationToken);
            await CreateCandidateDocument(request.ParsedData.CandidateId, questionnaire.Id);
            request.ParsedData.FileSource = _file.Name;
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
                await _mediator.Send(new CreateOrChangeQuestionCategoryCommand(questionCategory), cancellationToken);
                foreach (var questionItem in questionCategoryItem.ChildElements)
                {
                    Question question = new()
                    {
                        Name = questionItem.Name,
                        QuestionCategoryId = questionCategory.Id,
                    };
                    await _mediator.Send(new CreateOrChangeQuestionCommand(question), cancellationToken);
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
                                if(property.PropertyType != typeof(string))
                                {
                                    Convert.ChangeType(answerValue, property.PropertyType);
                                }
                                property.SetValue(answer, answerValue);
                            }
                        }
                        await _mediator.Send(new CreateAnswerCommand(answer), cancellationToken);
                    }
                }
            }
        }

        private async Task CreateCandidateDocument(int candidateId, int questionnaireId)
        {
            var candidate = await _mediator.Send(new GetCandidateQuery(candidateId));
            _file = new()
            {
                Name = $"{candidateId}.{candidate.Name}{parsedData.FileExtension}",
                FileType = FileType.Questionnaire,
                CandidateId = candidateId,
                QuestionnaireId = questionnaireId,
            };
            await _mediator.Send(new CreateRecruitingStaffWebAppFileCommand(_file));
        }

        private async Task<Candidate> CreateCandidate()
        {
            Candidate candidate = new();
            if (parsedData.Candidate == null)
            {
                return candidate;
            }
            candidate.Educations = await AssignValuesToPropertiesCompositeObject(
                parsedData.Candidate.Educations, () => new Education());
            candidate.Documents = await AssignValuesToPropertiesCompositeObject(
                parsedData.Candidate.Documents, "Name", () => new RecruitingStaffWebAppFile());
            candidate.Kids = await AssignValuesToPropertiesCompositeObject(
                parsedData.Candidate.Kids, () => new Kid());
            candidate.PreviousJobs = await AssignValuesToPropertiesCompositeObject(
                parsedData.Candidate.PreviousJobs, () => new PreviousJobPlacement());
            return candidate;
        }

        public static T AssignValuesToProperties<T, V>(T obj, V value)
        {
            var objProperties = obj.GetType().GetProperties();
            foreach (var valueProperty in value
                .GetType()
                .GetProperties()
                .Where(p => p.PropertyType.Name != "List`1"))
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

        public static Task<List<T>> AssignValuesToPropertiesCompositeObject<T, V>(List<V> values, Func<T> createObj)
        {
            if (values == null)
            {
                return null;
            }
            List<T> objects = new();
            foreach (var value in values)
            {
                objects.Add(AssignValuesToProperties(createObj(), value));
            }
            return Task.FromResult(objects);
        }
    }
}
