using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.UniversalHandlers.Maps
{
    public class GetMapsByFirstEntityIdHandler<TMap> : IRequestHandler<GetMapsByFirstEntityIdQuery<TMap>, TMap[]>
        where TMap : BaseMap
    {
        private readonly IRepository<TMap> _mapRepository;

        public GetMapsByFirstEntityIdHandler(IRepository<TMap> mapRepository)
        {
            _mapRepository = mapRepository;
        }

        public async Task<TMap[]> Handle(GetMapsByFirstEntityIdQuery<TMap> request, CancellationToken cancellationToken)
        {
            return await _mapRepository
                .GetAllAsNoTracking()
                .Where(m => m.FirstEntityId == request.FirstEntityId)
                .ToArrayAsync(cancellationToken);
        }
    }
}
