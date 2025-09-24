using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scirpts.Interaction
{
    /// <summary>
    /// Used to store inventory data
    /// the amount of cookies
    /// </summary>
    public enum ItemType
    {
        Cookie,
        // 以后可以加更多物品类型
    }

    [Serializable]
    public class InventoryItem
    {
        public ItemType itemType;
        public int amount;
    }
    [CreateAssetMenu(fileName = "InventoryData", menuName = "Data/InventoryData")]
    public class InventoryData : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> items = new List<InventoryItem>();
        public event Action<ItemType, int> OnItemChanged;

        // Get item amount in inventory
        public int GetAmount(ItemType type)
        {
            var item = items.Find(i => i.itemType == type);
            return item != null ? item.amount : 0;
        }

        // Add item to inventory
        public void AddItem(ItemType type, int amount)
        {
            var item = items.Find(i => i.itemType == type);
            if (item == null) 
                items.Add(new InventoryItem { itemType = type, amount = amount });
            else 
                item.amount += amount;

            OnItemChanged?.Invoke(type, GetAmount(type));
        }

        // Spend item from inventory
        public bool TrySpendItem(ItemType type, int amount)
        {
            var item = items.Find(i => i.itemType == type);
            if (item == null || item.amount < amount) return false;

            item.amount -= amount;
            OnItemChanged?.Invoke(type, item.amount);
            return true;
        }
    
        // reset all items
        public void Reset()
        {
            foreach (var item in items)
            {
                item.amount = 0;
                OnItemChanged?.Invoke(item.itemType, 0);
            }
        }
    }
}