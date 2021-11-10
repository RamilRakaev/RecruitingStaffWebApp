﻿using MediatR;
using RecruitingStaff.Domain.Model;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Options
{
    public class GetOptionsQuery : IRequest<Option[]>
    {
    }
}
