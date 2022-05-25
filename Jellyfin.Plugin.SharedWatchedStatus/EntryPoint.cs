using System;
using System.Threading.Tasks;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Plugins;
using MediaBrowser.Controller.Session;

namespace Jellyfin.Plugin.SharedWatchedStatus
{
    // TODO: Rename the class to something more appropriate.
    public class EntryPoint: IServerEntryPoint, IDisposable
    {
        private readonly ISessionManager SessionManager;
        private readonly IUserManager UserManager;
        private readonly IUserDataManager UserDataManager;

        public EntryPoint (ISessionManager sessionManager, IUserManager userManager, IUserDataManager userDataManager)
        {
            SessionManager = sessionManager;
            UserManager = userManager;
            UserDataManager = userDataManager;
        }

        public Task RunAsync ()
        {
            SessionManager.PlaybackStart += OnPlaybackUpdate;
            SessionManager.PlaybackProgress += OnPlaybackUpdate;
            SessionManager.PlaybackStopped += OnPlaybackUpdate;

            return Task.CompletedTask;
        }

        private void OnPlaybackUpdate (object? sender, PlaybackProgressEventArgs playbackProgressEventArgs)
        {
            if (playbackProgressEventArgs.Users == null
                || playbackProgressEventArgs.Users.Count == 0
                || playbackProgressEventArgs.Item == null
            ) {
                return;
            }

            foreach (var user in playbackProgressEventArgs.Users)
            {
                var userConnection = SharedWatchedStatusPlugin.Instance?.Configuration.GetByUserId(user.Id);

                if (userConnection == null)
                {
                    continue;
                }

                foreach (var toUserId in userConnection.ToUserIds)
                {
                    var toUserItemData = UserDataManager.GetUserData(toUserId, playbackProgressEventArgs.Item);

                    if (toUserItemData == null)
                    {
                        continue;
                    }

                    UserDataManager.UpdatePlayState(
                        playbackProgressEventArgs.Item,
                        toUserItemData,
                        playbackProgressEventArgs.PlaybackPositionTicks
                    );
                }
            }
        }

        public void Dispose ()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// </summary>
        /// <param name="cleanAllResources">If true, both managed and native resources will be cleaned, otherwise native only.</param>
        protected virtual void Dispose (bool cleanAllResources)
        {
            if (cleanAllResources)
            {
                SessionManager.PlaybackStart -= OnPlaybackUpdate;
                SessionManager.PlaybackProgress -= OnPlaybackUpdate;
                SessionManager.PlaybackStopped -= OnPlaybackUpdate;
            }
        }
    }
}
