using GameAssets.FunkyCode.SmartLighting2D.Components.LightCollider;
using GameAssets.FunkyCode.SmartLighting2D.Components.Lightmap;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Components.LightTilemap2D.Types;
using UnityEngine;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Light.ShadowEngine.Extensions
{
    public class TilemapCollider
    {
        public class Rectangle
        {
            static public void Draw(Light2D light, LightTilemapCollider2D id)
            {
                Vector2 position = -light.transform.position;

                switch(id.rectangle.shadowType)
                {
                    case ShadowType.CompositeCollider:

                        ShadowEngine.objectOffset = id.transform.position;

                        ShadowEngine.Draw(id.rectangle.compositeColliders, 0, 0, 0);

                        ShadowEngine.objectOffset = Vector2.zero;
                        
                    break;
                }
            }
        }
    }
}