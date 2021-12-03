using MediatR;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using RecruitingStaffWebApp.Services.DocParse;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Questionnaires
{
    public class GetJobQuestionnairesDictionaryHandler : IRequestHandler<GetJobQuestionnairesDictionaryQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(GetJobQuestionnairesDictionaryQuery request, CancellationToken cancellationToken)
        {
            var questionnaires = Enum.GetValues(typeof(JobQuestionnaire));
            Dictionary<int, string> dictionary = new();
            for (int i = 0; i < questionnaires.Length; i++)
            {
                dictionary.Add(i, questionnaires.GetValue(i).ToString());
            }
            return Task.FromResult(dictionary);
        }
    }
}
