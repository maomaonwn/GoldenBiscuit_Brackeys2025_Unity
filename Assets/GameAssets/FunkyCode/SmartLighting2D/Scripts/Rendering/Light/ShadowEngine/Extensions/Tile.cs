using System.Collections.Generic;
using GameAssets.FunkyCode.SmartLighting2D.Components.LightCollider;
using GameAssets.FunkyCode.SmartLighting2D.Components.Lightmap;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Components.LightTilemap2D;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Components.LightTilemap2D.Types;
using GameAssets.FunkyCode.SmartUtilities2D.Scripts.Utilities._2.Polygon2;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Light.ShadowEngine.Extensions
{
    public class Tile
    {
        static public void Draw(Light2D light, LightTile tile, LightTilemapCollider2D tilemap)
        {
            Base tilemapCollider = tilemap.GetCurrentTilemap();

            List<Polygon2> polygons = tile.GetWorldPolygons(tilemapCollider);

            ShadowEngine.Draw(polygons, 0, 0, 0);
        }  
    }
}