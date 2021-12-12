using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Inventory/InventoryChannel")]
public class InventoryChannel : ScriptableObject
{
    public delegate void InventoryItemLootCallback(InventorySystem.InventoryItem item, int quantity);

    public InventoryItemLootCallback OnInventoryItemLoot;

    public void RaiseLootItem(InventorySystem.InventoryItem item)
    {
        OnInventoryItemLoot?.Invoke(item, 1);
    }

    public void RaiseLootItem(InventorySystem.InventoryItem item, int quantity)
    {
        OnInventoryItemLoot?.Invoke(item, quantity);
    }


    public void UseLootItem(InventorySystem.InventoryItem item)
    {
        OnInventoryItemLoot?.Invoke(item, -1);
    }
}
