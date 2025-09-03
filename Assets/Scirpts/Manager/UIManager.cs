using UnityEngine;
using TMPro;

/// <summary>
/// update UI upon call
/// UI component manager
/// </summary>
public class UIManager : MonoBehaviour
{
    public InventoryData invData;
    public TMP_Text cookieCount;
    public ItemType cookieItem; 

    void OnEnable()
    {
        if (invData != null)
            invData.OnItemChanged += HandleItemChanged;
    }

    void Start()
    {
        if (invData != null)
            HandleItemChanged(cookieItem,invData.GetAmount(cookieItem));
    }

    void OnDisable()
    {
        if (invData != null)
            invData.OnItemChanged -= HandleItemChanged;
    }

    void HandleItemChanged(ItemType type, int value)
    {
        if (type == cookieItem)
        {
            cookieCount.text = $"Cookies: {value}";
        }
    }
}