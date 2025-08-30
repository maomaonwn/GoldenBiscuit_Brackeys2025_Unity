using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public PlayerInventory inventory;
    public TMP_Text cookieCount;

    void OnEnable()
    {
        if (inventory != null)
            // update ui on event triggered
            inventory.OnCookiesChanged += UpdateUI;
    }

    void Start()
    {
        if (inventory != null) UpdateUI(inventory.Cookies);
    }

    void OnDisable()
    {
        if (inventory != null)
            inventory.OnCookiesChanged -= UpdateUI;
    }

    void UpdateUI(int value)
    {
        cookieCount.text = $"Cookies: {value}";
    }
}
