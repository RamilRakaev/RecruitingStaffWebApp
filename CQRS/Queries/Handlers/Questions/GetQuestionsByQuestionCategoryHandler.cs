using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Questions
{
    //public class GetQuestionsByQuestionCategoryHandler : IRequestHandler<GetQuestionsByQuestionCategoryQuery, Question[]>
    //{
    //    private readonly IRepository<Question> _questionRepository;

    //    public GetQuestionsByQuestionCategoryHandler(IRepository<Question> questionRepository)
    //    {
    //        _questionRepository = questionRepository;
    //    }

    //    public Task<Question[]> Handle(GetQuestionsByQuestionCategoryQuery request, CancellationToken cancellationToken)
    //    {
    //        int[] QuestionCategories = request.QuestionCategories.Select(qc => qc.Id).ToArray();
    //        return Task.FromResult(_questionRepository.GetAllAsNoTracking().Where(q => QuestionCategories.Contains(q.QuestionCategoryId)).ToArray());
    //    }
    //}
}
