using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand.Maps;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.CandidateQuestionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries.Maps;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaff.WebApp.ViewModels.Maps;
using RecruitingStaffWebApp.Pages.User;
using System.IO;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Candidates.CandidateQuestionnaires
{
    public class CandidateQuestionnairesModel : BasePageModel
    {
        private readonly WebAppOptions _options;

        public CandidateQuestionnairesModel(IMediator mediator, ILogger<BasePageModel> logger, IOptions<WebAppOptions> options) : base(mediator, logger)
        {
            _options = options.Value;
        }

        public CandidateQuestionnaireViewModel[] CandidateQuestionnaireViewModels { get; set; }
        public SelectList AvailableVacancies { get; set; }
        public int CandidateId { get; set; }

        public async Task OnGet(int candidateId)
        {
            await Initialization(candidateId);
        }

        public async Task<IActionResult> OnGetDownloadDocument(int candidateId, string fileName)
        {
            await Initialization(candidateId);
            string source = _options.GetSource(FileType.CompletedQuestionnaire);
            string file_path = Path.Combine(source, fileName);
            string file_type = WebAppOptions.GetMimeType(FileType.CompletedQuestionnaire);
            return PhysicalFile(file_path, file_type, fileName);
        }

        public async Task OnPostCreateCandidateQuestionnaire(int candidateId, int questionnaireId)
        {
            if (questionnaireId != 0)
            {
                await _mediator.Send(new TryCreateMapCommand<CandidateQuestionnaire>(candidateId, questionnaireId));
            }
            await Initialization(candidateId);
        }

        public async Task OnPost(int candidateId, int candidateQuestionnaireId)
        {
            await _mediator.Send(new RemoveEntityCommand<CandidateQuestionnaire>(candidateQuestionnaireId));
            await Initialization(candidateId);
        }

        private async Task Initialization(int candidateId)
        {
            CandidateId = candidateId;
            var candidatqQuestionnaireEntities =
                await _mediator.Send(new GetMapsByFirstEntityIdQuery<CandidateQuestionnaire>(candidateId));
            var config = new MapperConfiguration(c => c.CreateMap<CandidateQuestionnaire, CandidateQuestionnaireViewModel>());
            var mapper = new Mapper(config);
            CandidateQuestionnaireViewModels = mapper.Map<CandidateQuestionnaireViewModel[]>(candidatqQuestionnaireEntities);
            foreach (var candidateQuestionnaire in CandidateQuestionnaireViewModels)
            {
                var questionnaireEntity =
                    await _mediator.Send(new GetEntityByIdQuery<Questionnaire>(candidateQuestionnaire.SecondEntityId));
                candidateQuestionnaire.QuestionnaireName = questionnaireEntity.Name;
                var file = await _mediator.Send(new GetCandidateQuestionnaireFileQuery(CandidateId, questionnaireEntity.Id));
                if (file != null)
                {
                    candidateQuestionnaire.IsParsed = true;
                    candidateQuestionnaire.FileName = file.Name;
                }
            }
            AvailableVacancies = new(await _mediator.Send(new GetAvailableQuestionnairesQuery(candidateId)), "Id", "Name");
        }
    }
}
