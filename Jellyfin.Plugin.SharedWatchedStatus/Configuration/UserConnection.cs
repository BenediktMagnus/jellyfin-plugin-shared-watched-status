using System;
using System.Collections.Generic;

namespace Jellyfin.Plugin.SharedWatchedStatus.Configuration
{
    public class UserConnection
    {
        public Guid FromUserId { get; set; }
        public ICollection<Guid> ToUserIds { get; }

        public UserConnection (Guid fromUserId, Guid[]? toUserIds = null)
        {
            FromUserId = fromUserId;

            ToUserIds = new List<Guid>(
                toUserIds ?? Array.Empty<Guid>()
            );
        }
    }
}
