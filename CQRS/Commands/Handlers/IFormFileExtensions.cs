using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers
{
    public static class IFormFileExtensions
    {
        public static async Task CreateNewFileAsync(this IFormFile formFile, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using var stream = new FileStream(path, FileMode.CreateNew);
            await formFile.CopyToAsync(stream);
        }
    }
}