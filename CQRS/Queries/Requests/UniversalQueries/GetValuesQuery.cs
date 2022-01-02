using MediatR;
using System;
using System.Collections.Generic;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries
{
    public class GetValuesQuery : IRequest<Dictionary<int, string>>
    {
        public GetValuesQuery(Type type)
        {
            Type = type;
        }

        public Type Type { get; set; }
    }
}
