using TMPro;
using UnityEngine;

namespace Scirpts.SceneTriggers
{
    public class ActivateTMP_OnTrigger : MonoBehaviour
    {
        public TMP_Text targetText; // 拖拽你需要激活的TMP文本到这里

        private void Start()
        {
            if (targetText != null)
                targetText.gameObject.SetActive(false); // 初始隐藏
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // 这里可以加上判断，只对特定对象响应，比如Player
            if (other.CompareTag("Player"))
            {
                if (targetText != null)
                    targetText.gameObject.SetActive(true);
            }
        }
    }
}