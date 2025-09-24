using Scirpts.Interaction;
using Scirpts.StateMachine.EntityStat;
using UnityEngine;

namespace Scirpts
{
    public class Heal : MonoBehaviour
    {
        public InventoryData inv;
        public PlayerStat playerStat;
        public ItemType itemType;
        public int healAmount=20;
    
        [Header("Key Bindings")]
        public KeyCode use1Key = KeyCode.Alpha1;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(use1Key))
            {
                ConsumeAndHeal(1);
            }
        }

        void ConsumeAndHeal(int count)
        {
            if(inv.TrySpendItem(itemType, count))
            {
                playerStat.Heal(healAmount);
            }
        }
    }
}
