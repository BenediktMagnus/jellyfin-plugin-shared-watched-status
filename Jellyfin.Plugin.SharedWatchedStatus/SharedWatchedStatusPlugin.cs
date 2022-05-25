using System;
using System.Collections.Generic;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;

namespace Jellyfin.Plugin.SharedWatchedStatus
{
    /// <summary>
    /// The main plugin.
    /// </summary>
    public class SharedWatchedStatusPlugin: BasePlugin<Configuration.PluginConfiguration>, IHasWebPages
    {
        /// <inheritdoc/>
        public override string Name => "Shared Watched Status";

        /// <inheritdoc/>
        public override Guid Id => Guid.Parse("af7e610f-ed05-4efd-bd69-c2269cf701f2");

        public static SharedWatchedStatusPlugin? Instance { get; private set; }

        /// <summary>
        /// Initialises a new instance for the plugin system.
        /// </summary>
        public SharedWatchedStatusPlugin (
            IApplicationPaths applicationPaths,
            IXmlSerializer xmlSerialiser
        ): base(applicationPaths, xmlSerialiser)
        {
            Instance = this;
        }

        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "sharedwatchedstatus",
                    EmbeddedResourcePath = GetType().Namespace + ".Web.configuration.html",
                },
                new PluginPageInfo
                {
                    Name = "sharedwatchedstatusjs",
                    EmbeddedResourcePath = GetType().Namespace + ".Web.configuration.js"
                }
            };
        }
    }
}
