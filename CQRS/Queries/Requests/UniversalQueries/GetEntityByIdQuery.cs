﻿using MediatR;
using RecruitingStaff.Domain.Model;
using System;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries
{
    public class GetEntityByIdQuery<TEntity> : IRequest<TEntity>
        where TEntity : CandidateQuestionnaireEntity, new()
    {
        public GetEntityByIdQuery(int entityId)
        {
            EntityId = entityId != 0 ? entityId : throw new ArgumentNullException();
        }

        public int EntityId { get; set; }
    }
}