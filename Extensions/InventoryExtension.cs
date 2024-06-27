using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatoriaModule.Extensions
{
    public static class InventoryExtension
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
            public ItemCreatoria() { }
        }
        public static void ClearInventory(this PlayerInventory inventory)
        {
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(inventory.player);
            player.Player.equipment.ReceiveUpdateState(0, 0, new byte[0]);
            player.Player.equipment.ReceiveUpdateState(1, 0, new byte[0]);
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

        public static bool RemoveItem(this PlayerInventory inventory, ushort id, int count = 1)
        {
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(inventory.player);
            var list = GetItems(inventory).Where(p => p.ItemJar.interactableItem.asset.id == id).ToList();
            if (!list.IsEmpty() || list.Count() >= count)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    var item = list[i];
                    player.Player.inventory.removeItem(item.Page, player.Player.inventory.getIndex(item.Page, item.ItemJar.x, item.ItemJar.y));
                }
                return true;
            }
            return false;
        }
    }
}
