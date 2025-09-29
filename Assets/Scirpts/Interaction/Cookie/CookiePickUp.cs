using UnityEngine;

namespace Scirpts.Interaction.Cookie
{
    public class CookiePickup : MonoBehaviour
    {
        [Header("捡到饼干的数量")]
        public int amount = 1;

        [Header("治疗量")]
        public int healAmount = 1;
        public bool Collect(PlayerInventory.PlayerInventory inv)
        {
            // add cookies in inventory
            inv.AddCookies(amount); 
            
            // // heal player
            // EntityStat playerStat = inv.GetComponent<EntityStat>();
            // if(playerStat!= null)
            //     playerStat.Heal(healAmount);
        
            Destroy(gameObject);
            return true;
        }
    }
}