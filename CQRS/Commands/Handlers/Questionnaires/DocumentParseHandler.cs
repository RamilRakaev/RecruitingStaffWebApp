using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.Repositories;
using RecruitingStaffWebApp.Services.DocParse;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questionnaires
{
    public class DocumentParseHandler : IRequestHandler<DocumentParseCommand, bool>
    {
        private readonly IQuestionnaireManager _questionnaireManager;
        private readonly WebAppOptions _options;

        public DocumentParseHandler(IQuestionnaireManager questionnaireManager, IOptions<WebAppOptions> options, DataContext db)
        {
            ResetDb(db);
            _questionnaireManager = questionnaireManager;
            _options = options.Value;
        }

        public Task<bool> Handle(DocumentParseCommand request, CancellationToken cancellationToken)
        {
            using(var stream = new FileStream($"{_options.DocumentsSource}\\{request.FormFile.FileName}", FileMode.CreateNew))
            {
                request.FormFile.CopyTo(stream);
            }
            return _questionnaireManager.ParseAndSaved(request.FormFile.FileName);
        }

        public static void ResetDb(DataContext db)
        {
            ResetDb<Answer>(db);
            ResetDb<Question>(db);
            ResetDb<QuestionCategory>(db);
            ResetDb<Questionnaire>(db);
            ResetDb<CandidateVacancy>(db);
            ResetDb<Vacancy>(db);
            ResetDb<Option>(db);
            ResetDb<Candidate>(db);
            ResetDb<RecruitingStaffWebAppFile>(db);
        }

        public static void ResetDb<Entity>(DataContext db) where Entity : BaseEntity
        {
            var entities = db.Set<Entity>().ToArray();
            for (int i = 0; i < entities.Length; i++)
            {
                db.Set<Entity>().Remove(entities.ElementAt(i));
                db.SaveChanges();
            }
        }
    }
}
