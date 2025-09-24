using GameAssets.FunkyCode.SmartLighting2D.Scripts.Misc;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Universal.Objects;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Settings;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Settings.Presets;
using UnityEngine;
using Texture = GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Universal.Objects.Texture;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Lightmap
{
	public static class Main
	{
		static Pass pass = new Pass();

		public static void Draw(UnityEngine.Camera camera, LightmapPreset lightmapPreset)
		{
			if (Rendering.Day.Main.IsDrawing(camera, lightmapPreset))
			{
				DarknessColor(camera, lightmapPreset);
			}
			
			var layerSettings = lightmapPreset.lightLayers.Get();
			if (layerSettings == null)
				return;

			if (layerSettings.Length < 1)
				return;

			for(int i = 0; i < layerSettings.Length; i++)
			{
				var lightingLayer = layerSettings[i];
				if (!pass.Setup(lightingLayer, camera))
					continue;

				if (lightingLayer.sorting == LayerSorting.None)
				{
					NoSort.Draw(pass);
				}
				else
				{
					pass.SortObjects();
					Sorted.Draw(pass);
				}
			}
		}

		private static void DarknessColor(UnityEngine.Camera camera, LightmapPreset lightmapPreset)
		{
			Color color = lightmapPreset.darknessColor;
			if (color.a > 0)
			{
				UnityEngine.Material material = Lighting2D.Materials.GetAlphaColor(); // use dedicated shader?
				material.mainTexture = null;
				
				GLExtended.color = color;

				float cameraRotation = -LightingPosition.GetCameraRotation(camera);
				Vector2 size = LightingRender2D.GetSize(camera);

				Texture.Quad.Draw(material, Vector2.zero, size, cameraRotation, 0);
			}
		}

		public static Color ClearColor(UnityEngine.Camera camera, LightmapPreset lightmapPreset)
		{
			if (Rendering.Day.Main.IsDrawing(camera, lightmapPreset))
			{
				return Color.white;
			}

			Color color = lightmapPreset.darknessColor;
			float alpha = color.a;

			if (alpha > 0)
			{
				Color returnColor = Color.white;

				returnColor.r = alpha * color.r + (1 - alpha) * returnColor.r;
				returnColor.g = alpha * color.g + (1 - alpha) * returnColor.g;
				returnColor.b = alpha * color.b + (1 - alpha) * returnColor.b;
				
				return returnColor;
			}
			else
			{
				return Color.white;
			}
		}		
	}
}