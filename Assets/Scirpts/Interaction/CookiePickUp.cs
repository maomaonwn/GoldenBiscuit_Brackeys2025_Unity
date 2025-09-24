using Scirpts.StateMachine.EntityStat;
using UnityEngine;

namespace Scirpts.Interaction
{
    public class CookiePickup : MonoBehaviour
    {
        public int amount = 1;

        public int healAmount = 1;
        public bool Collect(PlayerInventory inv)
        {
            // add cookies in inventory
            inv.AddCookies(amount); 
        
            // heal player
            EntityStat playerStat = inv.GetComponent<EntityStat>();
            if(playerStat!= null)
                playerStat.Heal(healAmount);
        
            Destroy(gameObject);
            return true;
        }
    }
}