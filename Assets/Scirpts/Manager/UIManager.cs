using Scirpts.EntityStat;
using UnityEngine;
using TMPro;

/// <summary>
/// update UI upon call
/// UI component manager
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Inventory UI")]
    public InventoryData invData;
    public TMP_Text cookieCount;
    public ItemType cookieItem; 

    [Header("Health UI")]
    public EntityStat playerStat;
    public TMP_Text healthText;
    void OnEnable()
    {
        if (invData != null)
            invData.OnItemChanged += HandleItemChanged;
        if (playerStat != null)
            playerStat.OnHealthChanged += HandleHealthChanged;
    }

    void Start()
    {
        if (invData != null)
            HandleItemChanged(cookieItem,invData.GetAmount(cookieItem));
        
        if(playerStat!=null)
            HandleHealthChanged(playerStat.currentHealth, playerStat.maxHealth);
            
    }

    void OnDisable()
    {
        if (invData != null)
            invData.OnItemChanged -= HandleItemChanged;
        
        if(playerStat!=null)
            playerStat.OnHealthChanged -= HandleHealthChanged;
    }

    void HandleItemChanged(ItemType type, int value)
    {
        if (type == cookieItem)
        {
            cookieCount.text = $"Cookies: {value}";
        }
    }
    
    void HandleHealthChanged(int currentH, int maxH)
    {
        healthText.text = $"Health: {currentH}/{maxH}";
    }
}