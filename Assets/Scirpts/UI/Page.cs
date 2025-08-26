using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource),typeof(CanvasGroup))]
[DisallowMultipleComponent]
public class Page : MonoBehaviour
{
    //组件
    private AudioSource audioSource;
    private RectTransform rect;
    private CanvasGroup canvasGroup;

    public float animationSpeed = 1f;
    public bool b_ExitOnNewPagePush = false;
    
    private Coroutine animationCoroutine;
    private Coroutine audioCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        //音频源初始设置
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0;
    }

    public void PageEnter(bool _b_PlayAudio)
    {
        
    }

    #region 音频播放方法

    private void SlideIn(bool _b_PlayAudio)
    {
        //判断协程是否为空
        if(animationCoroutine != null)
            StopCoroutine(animationCoroutine);
        
        
    }

    #endregion
}
