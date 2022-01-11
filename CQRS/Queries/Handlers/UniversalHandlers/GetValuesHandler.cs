using MediatR;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.UniversalHandlers
{
    public class GetValuesHandler : IRequestHandler<GetValuesQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(GetValuesQuery request, CancellationToken cancellationToken)
        {
            Dictionary<int, string> dictionary = new();
            var members = request.Type.GetFields(BindingFlags.Static | BindingFlags.Public);
            for (int i = 0; i < members.Count(); i++)
            {
                var name = members.ElementAt(i).GetCustomAttribute<DisplayAttribute>()?.GetName();
                dictionary.Add(i, name);
            }
            return Task.FromResult(dictionary);
        }
    }
}
