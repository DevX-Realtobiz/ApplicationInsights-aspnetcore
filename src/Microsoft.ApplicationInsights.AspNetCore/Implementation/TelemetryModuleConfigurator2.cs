namespace Microsoft.ApplicationInsights.AspNetCore
{
    using System;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// A generic factory for telemetry processors of a given type.
    /// </summary>
    internal class TelemetryModuleConfigurator2 : ITelemetryModuleConfigurator2
    {
        private Action<Object> configure;        

        /// <summary>
        /// Constructs an instance of the factory.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="telemetryProcessorType">The type of telemetry processor to create.</param>
        public TelemetryModuleConfigurator2(Action<Object> configure)
        {
            this.configure = configure;            
        }

        /// <summary>
        /// Creates an instance of the telemetry processor, passing the
        /// next <see cref="ITelemetryProcessor"/> in the call chain to
        /// its constructor.
        /// </summary>
        public void Configure(Object module)
        {
            if (this.configure!=null)
            { this.configure(module);
            }
        }
    }
}
