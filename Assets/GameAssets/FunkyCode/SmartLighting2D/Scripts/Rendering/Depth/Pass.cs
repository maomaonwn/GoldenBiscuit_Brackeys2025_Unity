using System.Collections.Generic;
using GameAssets.FunkyCode.SmartLighting2D.Components.DayLightCollider;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Settings.Presets;
using UnityEngine;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Depth
{
    public class Pass
    {
        public int layerId;
        public LightmapLayer layer;

        public UnityEngine.Camera camera;
        public Vector2 offset;

        public List<DayLightCollider2D> colliderList;
        public int colliderCount;

        // public bool drawShadows = false;
        // public bool drawMask = false;

        public List<DayLightTilemapCollider2D> tilemapColliderList;
        public int tilemapColliderCount;

        public bool Setup(LightmapLayer slayer, UnityEngine.Camera camera)
        {
            if (slayer.id < 0) {
                return(false);
            }

            layerId = (int)slayer.id;
            layer = slayer;

            this.camera = camera;
            offset = -camera.transform.position;

            colliderList = DayLightCollider2D.List;
            colliderCount = colliderList.Count;

            tilemapColliderList  = DayLightTilemapCollider2D.List;
            tilemapColliderCount = tilemapColliderList.Count;

            // drawShadows = slayer.type != LayerType.MaskOnly;
            // drawMask = slayer.type != LayerType.ShadowsOnly;
            
            return(true);
        }
    }
}