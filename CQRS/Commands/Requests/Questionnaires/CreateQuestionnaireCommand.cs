using MediatR;
using Microsoft.AspNetCore.Http;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires
{
    public class CreateQuestionnaireCommand : IRequest<bool>
    {
        public CreateQuestionnaireCommand(Questionnaire questionnaire, IFormFile formFile)
        {
            Questionnaire = questionnaire ?? throw new ArgumentNullException(nameof(CreateQuestionnaireCommand));
            UploadedFile = formFile ?? throw new ArgumentNullException(nameof(CreateQuestionnaireCommand));
        }

        public Questionnaire Questionnaire { get; set; }
        public IFormFile UploadedFile { get; set; }
    }
}
