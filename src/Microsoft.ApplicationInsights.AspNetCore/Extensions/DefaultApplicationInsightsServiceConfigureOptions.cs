﻿namespace Microsoft.AspNetCore.Hosting
{
    using System.Diagnostics;
    using System.Globalization;
    using Microsoft.ApplicationInsights.AspNetCore.Extensibility.Implementation.Tracing;
    using Microsoft.ApplicationInsights.AspNetCore.Extensions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// <see cref="IConfigureOptions&lt;ApplicationInsightsServiceOptions&gt;"/> implemetation that reads options from 'appsettings.json',
    /// environment variables and sets developer mode based on debugger state.
    /// </summary>
    internal class DefaultApplicationInsightsServiceConfigureOptions : IConfigureOptions<ApplicationInsightsServiceOptions>
    {
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Creates a new instance of <see cref="DefaultApplicationInsightsServiceConfigureOptions"/>
        /// </summary>
        /// <param name="hostingEnvironment"><see cref="IHostingEnvironment"/> to use for retreiving ContentRootPath</param>
        public DefaultApplicationInsightsServiceConfigureOptions(IHostingEnvironment hostingEnvironment)
        {
            AspNetCoreEventSource.Instance.Logverbose("DefaultApplicationInsightsServiceConfigureOptions constructor");
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <inheritdoc />
        public void Configure(ApplicationInsightsServiceOptions options)
        {
            AspNetCoreEventSource.Instance.Logverbose("DefaultApplicationInsightsServiceConfigureOptions configure");

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(this.hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile(string.Format(CultureInfo.InvariantCulture,"appsettings.{0}.json", hostingEnvironment.EnvironmentName), true)
                .AddEnvironmentVariables();
            ApplicationInsightsExtensions.AddTelemetryConfiguration(configBuilder.Build(), options);

            if (Debugger.IsAttached)
            {
                options.DeveloperMode = true;
            }
        }
    }
}