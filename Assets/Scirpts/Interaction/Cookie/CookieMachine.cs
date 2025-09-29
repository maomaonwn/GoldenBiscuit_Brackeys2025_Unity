using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scirpts.Interaction.Cookie
{
    public class CookieMachine : MonoBehaviour
    {
        [Header("Usage")]
        public int maxUses = 3;
        [SerializeField] private int usesLeft;
        public float useCooldown = 0.4f; 
        private float lastUseTime = -999f;

        [Header("Drop Setting")]
        public int minDrop = 0;  
        public int maxDrop = 3;  
    
        [Header("UI / Text")]
        public TextMeshProUGUI prompt;
        public string promptWhenAvailable = "Press F to use machine..({0}/{1} times left)";
        public string promptWhenDepleted  = "The machine is out of uses...";
        public string dialogOnUse         = "The Machine drops {0} cookies！({1}/{2} times left)";
        public string dialogOnEmpty       = "Nothing dropped this time :(";
    
        private Coroutine showRoutine;

        void Awake() { usesLeft = Mathf.Max(0, maxUses); }

        public bool IsDepleted => usesLeft <= 0;
        public bool CanUse =>
            usesLeft > 0 && Time.time - lastUseTime >= useCooldown;

        /// <summary>尝试使用机器：返回是否触发成功；dropped 为掉落饼干数量</summary>
        public bool TryUse(PlayerInventory.PlayerInventory inv, out int dropped)
        {
            dropped = 0;

            // used up or in cool down process
            if (usesLeft <= 0)
            {
                return false;
            }
            if (Time.time - lastUseTime < useCooldown) return false;

            lastUseTime = Time.time;
            usesLeft = Mathf.Max(0, usesLeft - 1);

            // drop cookies randomly
            dropped = Random.Range(minDrop, maxDrop + 1);

            // add to inventory
            if (inv != null && dropped > 0)
            {
                inv.AddCookies(dropped);
                // prompt
                ShowOneShot(string.Format(dialogOnUse, dropped, usesLeft, maxUses));
            }
            else if(dropped == 0)
                ShowOneShot(dialogOnEmpty);
      
            return true;
        }

        /// <summary>提供给外部用于显示的文本</summary>
        public string GetPromptText()
        {
            return usesLeft > 0
                ? string.Format(promptWhenAvailable, usesLeft, maxUses)
                : promptWhenDepleted;
        }

        /// <summary>
        /// 显示一条提示，1.2s 后自动隐藏
        /// </summary>
        public void ShowOneShot(string msg, float seconds = 1.2f)
        {
            prompt.text = msg;

            prompt.gameObject.SetActive(true);

            if (showRoutine != null)
                StopCoroutine(showRoutine);

            showRoutine = StartCoroutine(HideAfter(seconds));
        }

        private IEnumerator HideAfter(float seconds)
        {
            yield return new WaitForSecondsRealtime(seconds);
            prompt.gameObject.SetActive(false); 
            showRoutine = null;
        }
    
    }
}
