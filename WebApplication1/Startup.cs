using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.AddApplicationInsightsTelemetry("e4df90f1-a749-4191-a000-26a389897209");

            services.AddApplicationInsightsTelemetryProcessor<MyTelemetryProcessor>();

            services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>
                (module => module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("thomas"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            
            app.UseMvc();
            
            DependencyTrackingTelemetryModule dep;
            var modules = app.ApplicationServices.GetServices<ITelemetryModule>();
            foreach (var module in modules)
            {
                if (module is DependencyTrackingTelemetryModule)
                {
                    dep = module as DependencyTrackingTelemetryModule;
                    dep.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("cijo.com");
                    dep.DisableDiagnosticSourceInstrumentation = true;
                    dep.DisableRuntimeInstrumentation = true;
                    
                    //dep.Dispose();
                    //dep.DisableDiagnosticSourceInstrumentation = true;
                    //dep.Initialize(TelemetryConfiguration.Active);
                }
            }
            
            
           // var configuration = app.ApplicationServices.GetService<TelemetryConfiguration>();
            //var ss = configuration.TelemetryChannel as ServerTelemetryChannel;
            //ss.StorageFolder = "d:\\cijo";
            //ss.Initialize(configuration);

           // var channelDI = app.ApplicationServices.GetService<ITelemetryChannel>();
           // ((ServerTelemetryChannel)channelDI).StorageFolder = "D:\\CIJO";
            
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
