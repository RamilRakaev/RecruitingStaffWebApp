using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies
{
    public class ChangeVacancyCommand : IRequest<bool>
    {
        public ChangeVacancyCommand(Vacancy vacancy)
        {
            Vacancy = vacancy;
        }

        public Vacancy Vacancy { get; set; }
    }
}
