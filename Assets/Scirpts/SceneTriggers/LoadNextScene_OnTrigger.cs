using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Scirpts.SceneTriggers
{
    public class LoadNextScene_OnTrigger : MonoBehaviour
    {
        [Header("触发后延迟切换时间（s）")]
        public float delay = 0f;
        [FormerlySerializedAs("tag")] [Header("允许触发者")] 
        public string tagName = "Player";

        private void OnTriggerEnter2D(Collider2D other)
        {
            //目标触发方法
            if (other.CompareTag(tagName))
            {
                if (delay <= 0f)
                    LoadNextScene();
                else
                    Invoke(nameof(LoadNextScene), delay);
            }
        }

        private void LoadNextScene()
        {
            // 获取当前场景索引并加1
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // 如果超出总场景数，可回到0或不做处理
            if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
                nextSceneIndex = 0;

            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}