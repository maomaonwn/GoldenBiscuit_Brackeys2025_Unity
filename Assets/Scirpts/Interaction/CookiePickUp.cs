using UnityEngine;

public class CookiePickup : MonoBehaviour
{
    public int amount = 1;

    public bool Collect(PlayerInventory inv)
    {
        inv.AddCookies(amount); 
        Destroy(gameObject);
        return true;
    }
}