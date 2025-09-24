using GameAssets.FunkyCode.SmartLighting2D.Scripts.Components.LightTilemap2D.Types;

#if (SUPER_TILEMAP_EDITOR)

    namespace FunkyCode.SuperTilemapEditorSupport
    {
        public class TilemapRoom2D : TilemapCollider
        {
            // public enum MaskType {None, Grid, Sprite};
            // public MaskType maskType = MaskType.Sprite;
            // No Enums?
        }
    }

#else

    namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.SuperTilemapEditor.Components
    {
        public class TilemapRoom2D : Base
        {
            public override void Initialize() {}
        }
    }

#endif