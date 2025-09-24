using GameAssets.FunkyCode.SmartLighting2D.Scripts.Settings;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Settings.Presets;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Day
{	
	public static class Main
	{
		static Pass pass = new Pass();

		public static void Draw(UnityEngine.Camera camera, LightmapPreset lightmapPreset)
		{
			if (!IsDrawing(camera, lightmapPreset))
			{
				return;
			}

			LightmapLayer[] layerSettings = lightmapPreset.dayLayers.Get();
		
			for(int i = 0; i < layerSettings.Length; i++)
			{
				LightmapLayer dayLayer = layerSettings[i];

				LayerSorting sorting = dayLayer.sorting;

				if (!pass.Setup(dayLayer, camera))
				{
					continue;
				}

				if (sorting == LayerSorting.None)
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

		public static bool IsDrawing(UnityEngine.Camera camera, LightmapPreset lightmapPreset)
		{
			if (Lighting2D.DayLightingSettings.ShadowColor.a == 0) // <=
			{
				return(false);
			}

			if (lightmapPreset == null)
			{
				return(false);
			}

			LightmapLayer[] layerSettings = lightmapPreset.dayLayers.Get();

			if (layerSettings.Length < 1)
			{
				return(false);
			}

			return(true);
		}
	}
}