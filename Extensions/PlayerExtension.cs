using Rocket.Core;
using Rocket.Unturned.Player;
using SDG.NetTransport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatoriaModule.Extensions
{
    public static class PlayerExtension
    {
        public static ITransportConnection TransportConnection(this UnturnedPlayer player) =>
            player.Player.channel.GetOwnerTransportConnection();
        public static bool HaveRocketGroup(this UnturnedPlayer player, string id) => R.Permissions.GetGroups(player, true).Exists(p => p.Id == id);
    }
}
