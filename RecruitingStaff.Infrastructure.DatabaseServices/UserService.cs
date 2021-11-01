using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.UserIdentity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.DatabaseServices
{
    public class UserService : IHostedService
    {
        private readonly IServiceProvider _service;
        private readonly ILogger<UserService> _logger;
        private readonly List<ApplicationUser> users = new()
        {
            new ApplicationUser()
            {
                Id = 1,
                UserName = "DefaultUser",
                Email = "DefaultUser@gmail.com"
            },
        };
        private readonly List<string> passwords = new()
        {
            "e23D!23df32"
        };
        public UserService(IServiceProvider service,
            ILogger<UserService> logger)
        {
            _service = service;
            _logger = logger;
        }


        private async Task InitializeAsync(UserManager<ApplicationUser> userManager)
        {
            try
            {
                int i = 0;
                foreach (var user in users)
                {
                    if (await userManager.FindByEmailAsync(user.Email) == null)
                    {
                        await userManager.CreateAsync(user, passwords[i++]);
                        await userManager.AddToRoleAsync(user, "user");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start UserService");
            using var scope = _service.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            await InitializeAsync(userManager);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Stop UserService");
            return Task.CompletedTask;
        }
    }
}
