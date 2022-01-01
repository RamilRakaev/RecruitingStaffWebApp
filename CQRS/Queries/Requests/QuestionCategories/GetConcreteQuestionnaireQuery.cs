using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using System.Collections.Generic;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories
{
    public class GetConcreteQuestionnaireQuery : IRequest<Dictionary<QuestionCategory,Question[]>>
    {
        public GetConcreteQuestionnaireQuery(int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
        }

        public int QuestionnaireId { get; set; }
    }
}
