using UnityEngine;

namespace Scirpts.SceneTriggers
{
    public class EnemyNumberCheck_OnTrigger : MonoBehaviour
    {
        [Header("是否所有敌人已清除")]
        public bool b_AllEnemiesCleared = false;

        [Header("敌人 Tag")]
        public string enemyTag = "Enemy";

        private void OnTriggerStay2D(Collider2D other)
        {
            // 可选：只在玩家进入 Trigger 时检测
            if (!other.CompareTag("Player")) return;

            // 查找场景中所有 Enemy
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

            b_AllEnemiesCleared = enemies.Length == 0;
        }
    }
}
