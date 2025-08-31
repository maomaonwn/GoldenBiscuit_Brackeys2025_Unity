using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Scirpts.UI
{
    public class TextTypewriterTMP : MonoBehaviour
    {
        public TMP_Text tmpText;
        [Header("显示文本")]
        public string content;
        [Header("单个字母间隔时间(s)")] 
        public float interval = .05f;

        private void Start()
        {
            TypewriterStylePlay(content);
        }

        public void TypewriterStylePlay(string _content)
        {
            tmpText.maxVisibleCharacters = 0;
            tmpText.text = _content;
            DOTween.To(() => tmpText.maxVisibleCharacters, x => tmpText.maxVisibleCharacters = x, content.Length, content.Length * interval);
        }
    }
}
