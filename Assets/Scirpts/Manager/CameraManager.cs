using System.Collections;
using Scirpts.Base;
using Scirpts.Interaction;
using UnityEngine;
using DG.Tweening;

namespace Scirpts.Manager
{
    public class CameraManager : SingletonBase<CameraManager>
    {
        private Camera cam;
        public float targetCamSize = 10f;
        public float duration = 2f;

        private float startCamSize;
        private void Start()
        {
            if(cam is null)
                cam = UnityEngine.Camera.main;

            startCamSize = cam.orthographicSize;
        }

        private void Update()
        {
            if(DialogManager.Instance.b_JustEndDialog)
                CameraZoomOut();
        }

        #region 原相机的控制API
        
        /// <summary>
        /// 放大镜头视野
        /// </summary>
        /// <returns></returns>
        private void CameraZoomOut()
        {
            cam.DOOrthoSize(targetCamSize, duration).SetEase(Ease.OutBack);
        }
        
        #endregion
    }
}