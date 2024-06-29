using Rocket.Core;
using Rocket.Unturned.Player;
using SDG.NetTransport;
using SDG.Unturned;

namespace CreatoriaModule.Extensions
{
    public static class PlayerExtension
    {
        /// <summary>
        /// TransportConnection(For UI and others)
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static ITransportConnection TransportConnection(this UnturnedPlayer player) =>
            player.Player.channel.GetOwnerTransportConnection();

        public static SteamPlayer SteamPlayer(this ITransportConnection transportConnection) =>
            Provider.findPlayer(transportConnection);
        /// <summary>
        /// Checking if a player has permission group
        /// </summary>
        /// <param name="player"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool HaveRocketGroup(this UnturnedPlayer player, string id) => R.Permissions.GetGroups(player, true).Exists(p => p.Id == id);
        /// <summary>
        /// Switching the resolution of the F1, F2, F3, F4, F5 buttons
        /// </summary>
        /// <param name="player"></param>
        /// <param name="status"></param>
        public static void sendFreecam(this UnturnedPlayer player, bool statusAllowed) => player.Player.look.sendFreecamAllowed(statusAllowed);
        /// <summary>
        /// Switching the resolution of the F6 button
        /// </summary>
        /// <param name="player"></param>
        /// <param name="statusAllowed"></param>
        public static void sendWorkzone(this UnturnedPlayer player, bool statusAllowed) => player.Player.look.sendWorkzoneAllowed(statusAllowed);
        /// <summary>
        /// Switching the resolution of the F7 button
        /// </summary>
        /// <param name="player"></param>
        /// <param name="statusAllowed"></param>
        public static void sendSpecStats(this UnturnedPlayer player, bool statusAllowed) => player.Player.look.sendSpecStatsAllowed(statusAllowed);
    }
}
