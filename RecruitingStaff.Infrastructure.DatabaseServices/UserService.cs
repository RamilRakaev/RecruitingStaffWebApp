using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model;
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
        private readonly WebAppOptions _options;
        private readonly ILogger<UserService> _logger;
        private ApplicationUser defaultUser;

        public UserService(IServiceProvider service,
            IOptions<WebAppOptions> options,
            ILogger<UserService> logger)
        {
            _service = service;
            _options = options.Value;
            _logger = logger;
        }


        private async Task InitializeAsync(UserManager<ApplicationUser> userManager)
        {
            try
            {
                if (await userManager.FindByEmailAsync(_options.DefaultUserEmail) == null)
                {
                    defaultUser = new ApplicationUser() { Id = 1, UserName = _options.DefaultUserEmail, Email = _options.DefaultUserEmail };
                    await userManager.CreateAsync(defaultUser, _options.DefaultUserPassword);
                    await userManager.AddToRoleAsync(defaultUser, "user");
                    await userManager.UpdateAsync(defaultUser);
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
