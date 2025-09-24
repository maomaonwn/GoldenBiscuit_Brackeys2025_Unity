
using GameAssets.FunkyCode.SmartLighting2D.Components.LightCollider;
using GameAssets.FunkyCode.SmartLighting2D.Components.Lightmap;

#if (SUPER_TILEMAP_EDITOR)

    namespace FunkyCode.SuperTilemapEditorSupport.Light.Shadow
    {
        public class Collider
        {
            static public void Draw(Light2D light, LightTilemapCollider2D id)
            {
                Rendering.Light.ShadowEngine.Draw(id.superTilemapEditor.GetWorldColliders(), 0, 0, 0);
            }
        }
    }

#else 

    namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.SuperTilemapEditor.Rendering.Light.Shadow
    {
        public class Collider
        { 
            static public void Draw(Light2D light, LightTilemapCollider2D id) {}
        }
    }

#endif