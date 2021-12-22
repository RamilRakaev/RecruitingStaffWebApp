using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.Repositories;
using RecruitingStaffWebApp.Services.DocParse;
using System;
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
            //ResetDb(db);
            _questionnaireManager = questionnaireManager;
            _options = options.Value;
        }

        public Task<bool> Handle(DocumentParseCommand request, CancellationToken cancellationToken)
        {
            if (request.FormFile != null)
            {
                var path = $"{_options.DocumentsSource}\\{Guid.NewGuid()}";
                using (var stream = new FileStream(path, FileMode.CreateNew))
                {
                    request.FormFile.CopyTo(stream);
                }
                if (request.ParseQuestions)
                {
                    return _questionnaireManager.ParseQuestionnaire(
                    path,
                    (JobQuestionnaire)request.JobQuestionnaire);
                }
                else
                {
                    return _questionnaireManager.ParseAnswersAndCandidateData(
                        path,
                        (JobQuestionnaire)request.JobQuestionnaire,
                        request.CandidateId);
                }
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        /// <summary>
        /// Зачистка бд, временный метод
        /// </summary>
        /// <param name="db"></param>
        public static void ResetDb(DataContext db)
        {
            ResetDb<Answer>(db);
            ResetDb<Question>(db);
            ResetDb<QuestionCategory>(db);
            ResetDb<RecruitingStaffWebAppFile>(db);
            ResetDb<Option>(db);
            ResetDb<Questionnaire>(db);
            //ResetDb<CandidateQuestionnaire>(db);
            //ResetDb<CandidateVacancy>(db);
            ResetDb<Vacancy>(db);
            ResetDb<Recommender>(db);
            ResetDb<PreviousJobPlacement>(db);
            ResetDb<Education>(db);
            ResetDb<Candidate>(db);
        }

        public static void ResetDb<Entity>(DataContext db) where Entity : CandidateQuestionnaireEntity
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
