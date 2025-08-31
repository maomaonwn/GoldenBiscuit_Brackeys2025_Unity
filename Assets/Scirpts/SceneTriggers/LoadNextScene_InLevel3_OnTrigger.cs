using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scirpts.SceneTriggers
{
    public class LoadNextScene_InLevel3_OnTrigger : MonoBehaviour
    {
        [Header("切换时的特效")] public GameObject fxPrefab;
        private bool b_PlayerInside = false;

        public EnemyNumberCheck_OnTrigger enemyNumberCheck;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                b_PlayerInside = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                b_PlayerInside = false;
        }

        private void Update()
        {
            //持续检测条件
            if (b_PlayerInside && enemyNumberCheck.b_AllEnemiesCleared && Input.GetKeyDown(KeyCode.F))  
            {
                //加载特效
                if (fxPrefab != null)
                    Instantiate(fxPrefab, transform.position, Quaternion.identity);

                //加载下一个场景
                int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
                if (nextIndex < SceneManager.sceneCountInBuildSettings)
                    SceneManager.LoadScene(nextIndex);
                else
                    Debug.Log("没有下一个场景了！");
            }
        }
    }
}