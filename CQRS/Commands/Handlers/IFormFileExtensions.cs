using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers
{
    public static class IFormFileExtensions
    {
        public static async Task CreateNewFileAsync(this IFormFile formFile, string path, CancellationToken cancellationToken)
        {
            using var stream = new FileStream(path, FileMode.CreateNew);
            await formFile.CopyToAsync(stream, cancellationToken);
        }
    }
}