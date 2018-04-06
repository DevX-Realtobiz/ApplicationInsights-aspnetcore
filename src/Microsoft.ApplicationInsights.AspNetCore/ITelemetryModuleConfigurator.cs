namespace Microsoft.ApplicationInsights.AspNetCore
{
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.Extensibility;
    using System;

    /// <summary>
    /// Represents factory used to create <see cref="ITelemetryModule"/> with dependency injection support.
    /// </summary>
    public interface ITelemetryModuleConfigurator<T>
    {
        /// <summary>
        /// Returns a <see cref="ITelemetryProcessor"/>,
        /// given the next <see cref="ITelemetryProcessor"/> in the call chain.
        /// </summary>
        void Configure(T module);
    }
}