using GameAssets.FunkyCode.SmartLighting2D.Components.LightCollider;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Components.LightTilemap2D;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Components.LightTilemap2D.Types;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Universal.Objects;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Settings;
using GameAssets.FunkyCode.SmartUtilities2D.Scripts.Utilities;
using UnityEngine;
using Sprite = GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Universal.Objects.Sprite;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Light.Mask
{
    public class Tile {
		public static VirtualSpriteRenderer virtualSpriteRenderer = new VirtualSpriteRenderer();

       	static public void MaskSprite(LightTile tile, LayerSetting layerSetting, UnityEngine.Material material, LightTilemapCollider2D tilemap, float lightSizeSquared) {
			virtualSpriteRenderer.sprite = tile.GetSprite();

			if (virtualSpriteRenderer.sprite == null) {
				return;
			}

			Base tilemapBase = tilemap.GetCurrentTilemap();

			Vector2 tilePosition = tile.GetWorldPosition(tilemapBase) - ShadowEngine.ShadowEngine.light.transform2D.position;

			GLExtended.color = LayerSettingColor.Get(tilePosition, layerSetting, MaskLit.Lit, 1, 1); // 1?

			material.mainTexture = virtualSpriteRenderer.sprite.texture;

			Vector2 scale = tile.worldScale * tile.scale;

			GLExtended.color = Color.white;

			tilePosition.x += ShadowEngine.ShadowEngine.drawOffset.x;
			tilePosition.y += ShadowEngine.ShadowEngine.drawOffset.y;

			material.SetPass(0);

			Sprite.Pass.Draw(tile.spriteMeshObject, virtualSpriteRenderer, tilePosition, scale, tile.worldRotation);
			
			material.mainTexture = null;
		}
    }
}