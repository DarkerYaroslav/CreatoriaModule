using Rocket.Core;
using Rocket.Unturned.Items;
using Rocket.Unturned.Player;
using SDG.NetTransport;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class UnturnedPlayerExtension
{
    public class ItemCreatoria
    {
        public ItemJar ItemJar;
        public byte Page;
        public ItemCreatoria(ItemJar itemJar, byte page)
        {
            ItemJar = itemJar;
            Page = page;
        }
    }
    private static ITransportConnection TransportConnection(this UnturnedPlayer player) =>
        player.Player.channel.GetOwnerTransportConnection();
    public static void SendUIText(this UnturnedPlayer player, short key, string childNameOrPath, string text) =>
        EffectManager.sendUIEffectText(key, player.TransportConnection(), true, childNameOrPath, text);
    public static void SendUIEffect(this UnturnedPlayer player, ushort id, short key) =>
        EffectManager.sendUIEffect(id, key, player.TransportConnection(), true);
    public static void SendUIEffectVisibility(this UnturnedPlayer player, short key, string childNameOrPath,
        bool visible) => EffectManager.sendUIEffectVisibility(key, player.TransportConnection(), true, childNameOrPath, visible);
    public static void SendUIClear(this UnturnedPlayer player, ushort id) =>
        EffectManager.askEffectClearByID(id, player.TransportConnection());
    public static bool HaveRocketGroup(this UnturnedPlayer player,string id) => R.Permissions.GetGroups(player,true).Exists(p => p.Id == id);
    public static void ClearInventory(this PlayerInventory inventory)
    {
        UnturnedPlayer player = UnturnedPlayer.FromPlayer(inventory.player);
        player.Player.equipment.ReceiveSlot(0, 0, new byte[0]);
        player.Player.equipment.ReceiveSlot(1, 0, new byte[0]);
        for (byte page = 0; page < PlayerInventory.PAGES; page++)
        {
            if (page == PlayerInventory.AREA)
            {
                continue;
            }
            var count = player.Inventory.getItemCount(page);
            for (byte index = 0; index < count; index++)
            {
                player.Inventory.removeItem(page, 0);
            }
        }
        player.Player.clothing.updateClothes(0, 0, new byte[0], 0, 0, new byte[0], 0, 0, new byte[0], 0, 0, new byte[0], 0, 0, new byte[0], 0, 0, new byte[0], 0, 0, new byte[0]);
    }

    public static List<ItemCreatoria> GetItems(this PlayerInventory inventory)
    {
        var list = new List<ItemCreatoria>();
        foreach (var items in UnturnedPlayer.FromPlayer(inventory.player).Player.inventory.items.ToArray().Where(x => x != null))
        {
            foreach (var item in items.items.ToArray().Where(x => x != null))
                list.Add(new ItemCreatoria(item, items.page));
        }
        return list;
    }

    public static bool RemoveItem(this PlayerInventory inventory, ushort id, int count)
    {
        UnturnedPlayer player = UnturnedPlayer.FromPlayer(inventory.player);
        var list = GetItems(inventory).Where(p => p.ItemJar.interactableItem.asset.id == id).ToList();
        if (!list.IsEmpty() || list.Count() >= count)
        {
            foreach (var item in list)
                player.Player.inventory.removeItem(item.Page, player.Player.inventory.getIndex(item.Page, item.ItemJar.x, item.ItemJar.y));
            return true;
        }
        return false;
    }
}
