using MediatR;
using System.Collections.Generic;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires
{
    public class GetJobQuestionnairesDictionaryQuery : IRequest<Dictionary<int, string>>
    {
    }
}
