using System;
using Scirpts.Manager;
using Scirpts.StateMachine.EntityStat;
using UnityEngine;

namespace Scirpts.Interaction.PlayerInventory
{
    /// <summary>
    ///  Used to retrieve data from inventory and
    /// provide inventory operation (add/remove) functions
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {
        public InventoryData data;
        public PlayerStat playerStat;
        public int Cookies => data? data.GetAmount(ItemType.Cookie) : 0;

        private void Awake()
        {
            //从GameManager获取数据，确保跨场景数据不丢失
            if (data is null)
                data = GameManager.instance.inventoryData;
        }

        /// <summary>
        /// 往背包系统中添加饼干
        /// </summary>
        /// <param name="amount"></param>
        public void AddCookies(int amount)
        {
            data.AddItem(ItemType.Cookie, amount);
        }

        /// <summary>
        /// 使用饼干来回血
        /// </summary>
        /// <param name="_healAmount">每个饼干提供的回血量</param>
        /// <returns></returns>
        public bool UseCookie(int _healAmount)
        {
            //回血
            if (data.TrySpendItem(ItemType.Cookie, 1))
            {
                if (playerStat is not null)
                    playerStat.Heal(_healAmount);

                return true;
            }
            
#if UNITY_EDITOR
            Debug.Log("没有饼干可用！");
#endif
            return false;
        }
    }
}
