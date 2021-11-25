using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates.CandidateData.Recommenders
{
    public class CreateRecommenderCommand : IRequest<Recommender>
    {
        public CreateRecommenderCommand(Recommender recommender)
        {
            Recommender = recommender;
        }

        public Recommender Recommender { get; set; }
    }
}
