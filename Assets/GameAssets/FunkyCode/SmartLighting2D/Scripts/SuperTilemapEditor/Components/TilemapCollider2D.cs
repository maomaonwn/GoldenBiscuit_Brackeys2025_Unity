using GameAssets.FunkyCode.SmartLighting2D.Scripts.Components.LightTilemap2D.Types;

#if (SUPER_TILEMAP_EDITOR)

    namespace FunkyCode.SuperTilemapEditorSupport
    {
        [System.Serializable]
        public class TilemapCollider2D : TilemapCollider
        {
        }
    }

#else

    namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.SuperTilemapEditor.Components
    {
        [System.Serializable]
        public class TilemapCollider2D : TilemapCollider
        {
            
        }

        public class TilemapCollider : Base
        {
            public enum ShadowType {None, Grid, TileCollider, Collider};
            public enum MaskType {None, Grid, Sprite, BumpedSprite};

            public ShadowType shadowTypeSTE = ShadowType.Grid;
            public MaskType maskTypeSTE = MaskType.Sprite;

            public bool eventsInit;
        }
    }

#endif