using System.Collections;
using UnityEngine;

namespace Scirpts.StateMachine
{
    public class EntityFX : MonoBehaviour
    {
        private SpriteRenderer sr;

        [Header("Flash FX")] 
        public float flashDuration = .15f;
        [SerializeField]private Material hitMat;     //受伤时材质
        [SerializeField]private Material originalMat;   //原材质

        private void Start()
        {
            sr = GetComponentInChildren<SpriteRenderer>();
            //赋值原材质
            originalMat = sr.material;
        }

        /// <summary>
        /// 受伤时材质闪烁特效
        /// </summary>
        /// <returns></returns>
        private IEnumerator FlashFX()
        {
            sr.material = hitMat;
            yield return new WaitForSeconds(flashDuration);

            sr.material = originalMat;
        }
        
        /// <summary>
        /// 红色闪烁特效
        /// </summary>
        private void RedColorBlink()
        {
            if(sr.color != Color.white)
                sr.color = Color.white;
            else
                sr.color = Color.red;
        }
        /// <summary>
        /// 取消红色闪烁特效
        /// </summary>
        private void CancelRedBlink()
        {
            CancelInvoke();
            sr.color = Color.white;
        }
        
        /// <summary>
        /// 多色闪烁协程
        /// </summary>
        /// <param name="colors"></param>
        /// <param name="duration"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public IEnumerator MultiColorBlink(Color[] colors, float duration = .1f, int times = 5)
        {
            for (int i = 0; i < times; i++)
            {
                foreach (var color in colors)
                {
                    sr.color = color;
                    yield return new WaitForSeconds(duration);
                }
            }

            //恢复原颜色
            sr.color = Color.white;
        }

        public void StartMultiColorBlink()
        {
            Color[] colors = new Color[]
            {
                Color.red,
                Color.magenta,
                Color.cyan,
                Color.red,
                Color.magenta,
                Color.cyan,
            };

            StartCoroutine(MultiColorBlink(colors, .1f, 3));
        }
    }
}