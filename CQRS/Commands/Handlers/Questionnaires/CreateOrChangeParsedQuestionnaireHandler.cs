using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questionnaires
{
    public class CreateOrChangeParsedQuestionnaireHandler : IRequestHandler<CreateOrChangeParsedQuestionnaireCommand, Questionnaire>
    {
        private readonly IMediator _mediator;

        public CreateOrChangeParsedQuestionnaireHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Questionnaire> Handle(CreateOrChangeParsedQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            Vacancy vacancy = new()
            {
                Name = request.Vacancy.Name,
            };
            await _mediator.Send(new CreateOrChangeVacancyCommand(vacancy), cancellationToken);
            Questionnaire questionnaire = new()
            {
                Name = request.Questionnaire.Name,
                VacancyId = vacancy.Id,
            };
            await _mediator.Send(new CreateOrChangeQuestionnaireCommand(questionnaire), cancellationToken);
            if (questionnaire == null)
            {
                foreach (var questionCategoryItem in request.Questionnaire.ChildElements)
                {
                    QuestionCategory questionCategory = new()
                    {
                        Name = questionCategoryItem.Name,
                        QuestionnaireId = questionnaire.Id,
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
                    }
                }
            }
            return questionnaire;
        }
    }
}
