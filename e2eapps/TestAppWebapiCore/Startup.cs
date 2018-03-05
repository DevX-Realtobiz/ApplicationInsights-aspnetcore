using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TestAppWebapiCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            /*
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(this.HostingEnvironment.ContentRootPath)
               .AddJsonFile("appsettings.json", true)               
               .AddEnvironmentVariables();            
            services.AddApplicationInsightsTelemetry(configBuilder.Build());
            */

            //var dep = services.FirstOrDefault<ServiceDescriptor>(t => t.ImplementationType == typeof(DependencyTrackingTelemetryModule));


            //var telemetryConfiguration =
            //services.BuildServiceProvider().GetService<TelemetryConfiguration>();
            //var builder = telemetryConfiguration.TelemetryProcessorChainBuilder;
            //builder.Use((next) => new MyTelemetryProcessor(next));
            //builder.Build();
            //builder.UseAdaptiveSampling(maxTelemetryItemsPerSecond:10);

            //var configuration = services.AddApplicationInsightsTelemetryProcessor(typeof(MyTelemetryProcessor));

            //var dep = app.ApplicationService s.GetService<DependencyTrackingTelemetryModule>()

             services.AddApplicationInsightsTelemetry("37aac79b-2ec5-47dc-8e76-ce9f33e82217");
          //  services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            /*
            DependencyTrackingTelemetryModule dep;
            var modules = app.ApplicationServices.GetServices<ITelemetryModule>();
            foreach(var module in modules)
            {
                if (module is DependencyTrackingTelemetryModule)
                {
                    dep = module as DependencyTrackingTelemetryModule;
                    dep.SetComponentCorrelationHttpHeaders = false;                    
                    dep.Initialize(TelemetryConfiguration.Active);                    
                }
            }
            */

            //.FirstOrDefault<ServiceDescriptor>(t => t.ImplementationType == typeof(DependencyTrackingTelemetryModule));

            //DependencyTrackingTelemetryModule dep = new DependencyTrackingTelemetryModule();

            loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Warning);
            
        }

        private class MyTelemetryProcessor : ITelemetryProcessor
        {
            private ITelemetryProcessor Next { get; set; }

            public MyTelemetryProcessor(ITelemetryProcessor next)
            {
                this.Next = next;
            }

            public void Process(ITelemetry item)
            {
                this.Next.Process(item);
            }
        }
    }
}
