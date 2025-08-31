using System;
using System.Collections;
using UnityEngine;

namespace Scirpts
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
        /// 受伤时闪烁特效
        /// </summary>
        /// <returns></returns>
        private IEnumerator FlashFX()
        {
            sr.material = hitMat;
            yield return new WaitForSeconds(flashDuration);

            sr.material = originalMat;
        }
        
        private void RedColorBlink()
        {
            if(sr.color != Color.white)
                sr.color = Color.white;
            else
                sr.color = Color.red;
        }
        private void CancelRedBlink()
        {
            CancelInvoke();
            sr.color = Color.white;
        }
    }
}