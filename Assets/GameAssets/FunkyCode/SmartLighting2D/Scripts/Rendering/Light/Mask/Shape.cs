using GameAssets.FunkyCode.SmartLighting2D.Components.LightCollider;
using GameAssets.FunkyCode.SmartLighting2D.Components.Lightmap;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Universal.Objects;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Settings;
using UnityEngine;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Light.Mask
{
    public class Shape
    {
        public static void Mask(Light2D light, LightCollider2D id, LayerSetting layerSetting)
        {
            if (!id.InLight(light))
            {
                return;
            }

            var shape = id.mainShape;
            var meshObjects = shape.GetMeshes();
            if (meshObjects == null)
            {
                return;
            }
                        
            Vector2 position = shape.transform2D.Position - light.transform2D.position;
            Vector2 pivotPosition = shape.GetPivotPoint() - light.transform2D.position;

            GLExtended.color = LayerSettingColor.Get(pivotPosition, layerSetting, id.maskLit, 1, id.maskLitCustom);
            GLExtended.DrawMeshPass(meshObjects, position, shape.transform.lossyScale, shape.transform2D.Rotation);
        }
    }
}