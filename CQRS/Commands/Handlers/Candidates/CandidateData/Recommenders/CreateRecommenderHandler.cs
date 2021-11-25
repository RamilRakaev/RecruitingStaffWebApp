using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates.CandidateData.Recommenders;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates.CandidateData.Recommenders
{
    public class CreateRecommenderHandler : IRequestHandler<CreateRecommenderCommand, Recommender>
    {
        private readonly IRepository<Recommender> _recommenderRepository;

        public CreateRecommenderHandler(IRepository<Recommender> recommenderRepository)
        {
            _recommenderRepository = recommenderRepository;
        }

        public async Task<Recommender> Handle(CreateRecommenderCommand request, CancellationToken cancellationToken)
        {
            await _recommenderRepository.AddAsync(request.Recommender, cancellationToken);
            await _recommenderRepository.SaveAsync(cancellationToken);
            return request.Recommender;
        }
    }
}
