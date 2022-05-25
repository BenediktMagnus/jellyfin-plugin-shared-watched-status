using System;
using System.Collections.Generic;
using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.SharedWatchedStatus.Configuration
{

    public class PluginConfiguration: BasePluginConfiguration
    {
        public ICollection<UserConnection> UserConnections { get; }

        public PluginConfiguration()
        {
            UserConnections = new List<UserConnection>();
        }

        public UserConnection? GetByUserId (Guid userId)
        {
            foreach (var userConnection in UserConnections)
            {
                if (userConnection.FromUserId == userId)
                {
                    return userConnection;
                }
            }

            return null;
        }
    }
}
