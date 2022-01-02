using MediatR;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.UniversalHandlers
{
    public class GetValuesHandler : IRequestHandler<GetValuesQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(GetValuesQuery request, CancellationToken cancellationToken)
        {
            var valuesArray = Enum.GetValues(request.Type);
            Dictionary<int, string> dictionary = new();
            for (int i = 0; i < valuesArray.Length; i++)
            {
                dictionary.Add(i, valuesArray.GetValue(i).ToString());
            }
            return Task.FromResult(dictionary);
        }
    }
}
