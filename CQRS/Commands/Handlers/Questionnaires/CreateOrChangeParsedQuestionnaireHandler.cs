using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questionnaires
{
    public class CreateOrChangeParsedQuestionnaireHandler : IRequestHandler<CreateOrChangeParsedQuestionnaireCommand, CommandResult>
    {
        private readonly IRepository<Questionnaire> _questionnaireRepository;

        public CreateOrChangeParsedQuestionnaireHandler(IRepository<Questionnaire> questionnaireRepository)
        {
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task<CommandResult> Handle(CreateOrChangeParsedQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            var questionnaire = await _questionnaireRepository
                .GetAllAsNoTracking()
                .Where(q => q.Name == request.Questionnaire.Name)
                .FirstOrDefaultAsync(cancellationToken);
            if(questionnaire == null)
            {
                questionnaire = new()
                {
                    Name = request.Questionnaire.Name,
                    QuestionCategories = new(),
                };
                foreach (var questionCategory in request.Questionnaire.ChildElements)
                {

                }
            }
            throw new NotImplementedException();
        }
    }
}
