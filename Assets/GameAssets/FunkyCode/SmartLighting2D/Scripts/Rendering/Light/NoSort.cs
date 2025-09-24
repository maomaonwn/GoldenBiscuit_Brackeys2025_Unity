using GameAssets.FunkyCode.SmartLighting2D.Components.LightCollider;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Components.LightTilemap2D.Types;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Light.Mask;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.SuperTilemapEditor.Components;
using UnityEngine;
using Collider = GameAssets.FunkyCode.SmartLighting2D.Scripts.SuperTilemapEditor.Rendering.Light.Shadow.Collider;
using Grid = GameAssets.FunkyCode.SmartLighting2D.Scripts.SuperTilemapEditor.Rendering.Light.Shadow.Grid;
using Mesh = GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Light.Mask.Mesh;
using Shape = GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Light.ShadowEngine.Extensions.Shape;
using TilemapCollider = GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Light.ShadowEngine.Extensions.TilemapCollider;
using UnityTilemap = GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Light.ShadowEngine.Extensions.UnityTilemap;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Light
{
    public static class NoSort
    {
        public static void Draw(Pass pass)
        {
            if (pass.drawShadows)
            {
                Shadows.Draw(pass);
            }

            if (pass.drawMask)
            {
                Masks.Draw(pass);
            }
        }

        public static class Shadows
        {
            public static void Draw(Pass pass)
            {
                switch(ShadowEngine.ShadowEngine.drawMode)
                {
                    case ShadowEngine.ShadowEngine.DRAW_MODE_SPRITEPROJECTION:

                        DrawCollider(pass);

                    break;

                     case ShadowEngine.ShadowEngine.DRAW_MODE_FAST:

                        ShadowEngine.ShadowEngine.GetMaterial().SetPass(0);

                        GL.Begin(GL.QUADS);

                        GL.Color(new Color(1, 0, 1, 1));

                        DrawCollider(pass);
                        DrawTilemapCollider(pass);
                
                        GL.End();

                    break;

                    default:

                        ShadowEngine.ShadowEngine.GetMaterial().SetPass(0);

                        GL.Begin(GL.TRIANGLES);

                        GL.Color(new Color(1, 0, 1, 1));

                        DrawCollider(pass);
                        DrawTilemapCollider(pass);
                
                        GL.End();

                    break;
                }
            }

            private static void DrawCollider(Pass pass)
            {
                int colliderCount = pass.layerShadowList.Count;

                if (colliderCount < 1)
                {
                    return;
                }

                for(int id = 0; id < colliderCount; id++)
                {
                    LightCollider2D collider = pass.layerShadowList[id];

                    if (collider.ShadowDisabled())
                    {
                        continue;
                    }

                    switch(ShadowEngine.ShadowEngine.drawMode)
                    {
                        case ShadowEngine.ShadowEngine.DRAW_MODE_SPRITEPROJECTION:

                            ShadowEngine.ShadowEngine.spriteProjection = collider.mainShape.spriteShape.GetOriginalSprite();

                            SpriteRenderer spriteRenderer = collider.mainShape.spriteShape.GetSpriteRenderer();
                            ShadowEngine.ShadowEngine.flipX = spriteRenderer.flipX;
                            ShadowEngine.ShadowEngine.flipY = spriteRenderer.flipY; 
                            
                        break;  
                    }

                    Shape.Draw(pass.light, collider);
                }
            }

            private static void DrawTilemapCollider(Pass pass)
            {
                for(int id = 0; id < pass.tilemapShadowList.Count; id++)
                {
                    LightTilemapCollider2D tilemap = pass.tilemapShadowList[id];

                    bool shadowsDisabled = tilemap.ShadowsDisabled();
                    
                    if (shadowsDisabled)
                    {
                        continue;
                    }

                    if (!tilemap.InLight(pass.light))
                    {
                        continue;
                    }

                    switch(tilemap.mapType)
                    {
                        case MapType.UnityRectangle:
                        case MapType.UnityIsometric:
                        case MapType.UnityHexagon:

                            Base baseTilemap = tilemap.GetCurrentTilemap();

                            switch(baseTilemap.shadowType)
                            {
                                case ShadowType.SpritePhysicsShape:
                                case ShadowType.Grid:

                                    UnityTilemap.Draw(pass.light, tilemap);

                                break;

                                case ShadowType.CompositeCollider:

                                    TilemapCollider.Rectangle.Draw(pass.light, tilemap);

                                break;
                            }
                            
                        break;

                        case MapType.SuperTilemapEditor:

                            switch(tilemap.superTilemapEditor.shadowTypeSTE)
                            {
                                case TilemapCollider2D.ShadowType.Grid:
                                case TilemapCollider2D.ShadowType.TileCollider:

                                        Grid.Draw(pass.light, tilemap);

                                break;
                                    
                                case TilemapCollider2D.ShadowType.Collider:

                                    Collider.Draw(pass.light, tilemap);

                                break;
                                }
                            
                        break;
                    }
                }
            }
        }

        private static class Masks
        {
           static public void Draw(Pass pass)
           {
                UnityEngine.Material maskMaterial = pass.materialMask;
                maskMaterial.mainTexture = null;
                maskMaterial.SetPass(0);

                GL.Begin(GL.TRIANGLES);

                    DrawCollider(pass);

                    DrawTilemapCollider(pass);

                GL.End();

                DrawMesh(pass);

                DrawSprite(pass);

                DrawTilemapSprite(pass);
            }

            private static void DrawCollider(Pass pass)
            {
                int colliderCount = pass.layerMaskList.Count;

                if (colliderCount < 1)
                {
                    return;
                }

                for(int id = 0; id < colliderCount; id++)
                {
                    LightCollider2D collider = pass.layerMaskList[id];

                    switch(collider.mainShape.maskType)
                    {
                        case LightCollider2D.MaskType.SpritePhysicsShape:
                        case LightCollider2D.MaskType.CompositeCollider2D:
                        case LightCollider2D.MaskType.Collider2D:
                        case LightCollider2D.MaskType.Collider3D:

                            Mask.Shape.Mask(pass.light, collider, pass.layer);

                        break;
                    }
                }
            }

            private static void DrawMesh(Pass pass)
            {
                int colliderCount = pass.layerMaskList.Count;

                if (colliderCount < 1)
                {
                    return;
                }

                for(int id = 0; id < colliderCount; id++)
                {
                    LightCollider2D collider = pass.layerMaskList[id];

                    switch(collider.mainShape.maskType)
                    {
                        case LightCollider2D.MaskType.MeshRenderer:

                            Mesh.Mask(pass.light, collider, pass.materialMask, pass.layer);

                        break;

                        case LightCollider2D.MaskType.BumpedMeshRenderer:

                            UnityEngine.Material material = collider.bumpMapMode.SelectMaterial(pass.materialNormalMap_PixelToLight, pass.materialNormalMap_ObjectToLight);
                            Mesh.MaskNormalMap(pass.light, collider, material, pass.layer);

                        break;

                        case LightCollider2D.MaskType.SkinnedMeshRenderer:

                            SkinnedMesh.Mask(pass.light, collider, pass.materialMask, pass.layer);

                        break;
                    }
                }
            }

            private static void DrawSprite(Pass pass)
            {
                int colliderCount = pass.layerMaskList.Count;

                if (colliderCount < 1)
                {
                    return;
                }

                SpriteRenderer2D.currentTexture = null;

                for(int id = 0; id < colliderCount; id++)
                {
                    LightCollider2D collider = pass.layerMaskList[id];

                    if (collider.mainShape.maskType != LightCollider2D.MaskType.Sprite)
                    {
                        continue;
                    }

                    SpriteRenderer2D.Mask(pass.light, collider, pass.materialMask, pass.layer);
                }

                if (SpriteRenderer2D.currentTexture != null)
                {
                    GL.End();

                    pass.materialMask.mainTexture = null;

                    SpriteRenderer2D.currentTexture = null;
                }

                // pptimize bumped sprites batching? (use SpriteRenderer2D.currentTexture?)

                for(int id = 0; id < colliderCount; id++)
                {
                    LightCollider2D collider = pass.layerMaskList[id];

                    if (collider.mainShape.maskType != LightCollider2D.MaskType.BumpedSprite)
                    {
                        continue;
                    }

                    UnityEngine.Material material = collider.bumpMapMode.SelectMaterial(pass.materialNormalMap_PixelToLight, pass.materialNormalMap_ObjectToLight);
                
                    SpriteRenderer2D.MaskBumped(pass.light, collider, material, pass.layer);
                }
            }

            private static void DrawTilemapCollider(Pass pass)
            {
                for(int id = 0; id < pass.tilemapMaskList.Count; id++)
                {
                    LightTilemapCollider2D tilemap = pass.tilemapMaskList[id];

                    if (tilemap.maskLayer != pass.layerID)
                    {
                        continue;
                    }

                    if (tilemap.MasksDisabled())
                    {
                        continue;
                    }
                    
                    if (!tilemap.InLight(pass.light))
                    {
                        continue;
                    }

                    // Tilemap In Range
                    switch(tilemap.mapType)
                    {
                        case MapType.UnityRectangle:
                        case MapType.UnityIsometric:
                        case MapType.UnityHexagon:

                            Base baseTilemap = tilemap.GetCurrentTilemap();

                            switch(baseTilemap.maskType)
                            {
                                case MaskType.Grid:
                                case MaskType.SpritePhysicsShape:

                                    Mask.UnityTilemap.MaskShape(pass.light, tilemap, pass.layer);

                                break;
                            }

                        break;

                        case MapType.SuperTilemapEditor:

                            SuperTilemapEditor.Rendering.Light.Mask.Grid.Draw(pass.light, tilemap);

                        break;
                    }
                }
            }

            private static void DrawTilemapSprite(Pass pass)
            {
                for(int id = 0; id < pass.tilemapMaskList.Count; id++)
                {
                    LightTilemapCollider2D tilemap = pass.tilemapMaskList[id];

                    if (tilemap.maskLayer != pass.layerID)
                    {
                        continue;
                    }

                    if (tilemap.MasksDisabled())
                    {
                        continue;
                    }

                    if (!tilemap.InLight(pass.light))
                    {
                        continue;
                    }

                    // Tilemap In Range

                        switch(tilemap.mapType)
                        {
                            case MapType.UnityRectangle:
                            case MapType.UnityIsometric:
                            case MapType.UnityHexagon:

                                Base baseTilemap = tilemap.GetCurrentTilemap();
                        
                            switch(baseTilemap.maskType)
                            {
                                case MaskType.Sprite:
                                    
                                    Mask.UnityTilemap.Sprite(pass.light, tilemap, pass.materialMask, pass.layer);
                                
                                break;

                                case MaskType.BumpedSprite:

                                    UnityEngine.Material material = tilemap.bumpMapMode.SelectMaterial(pass.materialNormalMap_PixelToLight, pass.materialNormalMap_ObjectToLight);
                        
                                    Mask.UnityTilemap.BumpedSprite(pass.light, tilemap, material, pass.layer);

                                break;
                            }
                            
                        break;

                        case MapType.SuperTilemapEditor:

                            switch(tilemap.superTilemapEditor.maskTypeSTE)
                            {
                                case SuperTilemapEditor.Components.TilemapCollider.MaskType.Sprite:

                                    SuperTilemapEditor.Rendering.Light.Mask.SpriteRenderer2D.Sprite(pass.light, tilemap, pass.materialMask);
                                
                                break;
                                
                                case SuperTilemapEditor.Components.TilemapCollider.MaskType.BumpedSprite:

                                    UnityEngine.Material material = tilemap.bumpMapMode.SelectMaterial(pass.materialNormalMap_PixelToLight, pass.materialNormalMap_ObjectToLight);
                        
                                    SuperTilemapEditor.Rendering.Light.Mask.SpriteRenderer2D.BumpedSprite(pass.light, tilemap, material);
                            
                                break;
                            }
    
                        break;
                    }                   
                }
            }
        }
    }
}