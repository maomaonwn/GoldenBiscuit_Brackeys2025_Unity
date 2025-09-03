using UnityEngine;

/// <summary>
///  Used to retrieve data from inventory and
/// provide inventory operation (add/remove) functions
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    public InventoryData data;
    public int Cookies => data? data.GetAmount(ItemType.Cookie) : 0;

    public void AddCookies(int amount)
    {
        if (!data)
        {
            Debug.LogError("No inventory data");
            return;
        }
        data.AddItem(ItemType.Cookie, amount);
        
        //Debug.Log($"Cookies: {Cookies}");
        
    }
}
