﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaymentGateway.Infrastructure;
using PaymentGateway.Repository;
using PaymentGateway.Security;
using PaymentGateway.Services;

namespace PaymentGateway
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogDebug("Registering services");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Set up security services
            services.AddSingleton<IUserService, MockUserService>();

            // Set up authentication
            services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddSingleton<ILogger>(_logger);

            // Set up services
            services.AddSingleton<IPaymentRequestRepository, InMemoryRepository>();
            services.AddSingleton<IBankService>(new MockBankService(_logger)
            {
                Success = true
            });
            services.AddSingleton<IPaymentRequestValidator, PaymentRequestValidator>();
            services.AddSingleton<IPaymentRequestHandler, PaymentRequestHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            _logger.LogDebug("Configuring application");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
