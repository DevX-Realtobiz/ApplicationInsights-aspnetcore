namespace Microsoft.ApplicationInsights.AspNetCore
{
    using System;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// A generic factory for telemetry processors of a given type.
    /// </summary>
    internal class TelemetryModuleConfigurator<T> : ITelemetryModuleConfigurator<T>
    {
        private Action<T> configure;
        /// <summary>
        /// Constructs an instance of the factory.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="telemetryProcessorType">The type of telemetry processor to create.</param>
        public TelemetryModuleConfigurator(Action<T> configure)
        {
            this.configure = configure;
        }

        public TelemetryModuleConfigurator()
        {            
        }

        /// <summary>
        /// Creates an instance of the telemetry processor, passing the
        /// next <see cref="ITelemetryProcessor"/> in the call chain to
        /// its constructor.
        /// </summary>
        public void Configure(T module)
        {
            if (this.configure!=null)
            { this.configure(module);
            }
        }
    }
}
