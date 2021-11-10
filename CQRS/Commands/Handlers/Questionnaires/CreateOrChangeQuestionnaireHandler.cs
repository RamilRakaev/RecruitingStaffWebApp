﻿using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questionnaires
{
    public class CreateOrChangeQuestionnaireHandler : IRequestHandler<CreateOrChangeQuestionnaireCommand, bool>
    {
        private readonly IRepository<Questionnaire> _questionnaireRepository;

        public CreateOrChangeQuestionnaireHandler(IRepository<Questionnaire> questionnaireRepository)
        {
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task<bool> Handle(CreateOrChangeQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            var questionnaire = await _questionnaireRepository.FindAsync(request.Questionnaire.Id);
            if(questionnaire == null)
            {
                if(_questionnaireRepository.GetAllAsNoTracking().Where(q => q == request.Questionnaire).FirstOrDefault() == null)
                {
                    return false;
                }
                await _questionnaireRepository.AddAsync(request.Questionnaire);
            }
            else
            {
                questionnaire.Name = request.Questionnaire.Name;
                questionnaire.VacancyId = request.Questionnaire.VacancyId;
            }
            await _questionnaireRepository.SaveAsync();
            return true;
        }
    }
}
