using GameAssets.FunkyCode.SmartLighting2D.Scripts.Settings.Presets;
using UnityEngine;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Depth
{
	public static class Main
	{
		static Pass pass = new Pass();

		public static void Draw(UnityEngine.Camera camera, LightmapPreset lightmapPreset)
		{
			LightmapLayer[] layerSettings = lightmapPreset.dayLayers.Get();
	
			if (layerSettings == null)
			{
				return;
			}

			if (layerSettings.Length < 1)
			{
				return;
			}

			for(int i = 0; i < layerSettings.Length; i++)
			{
				LightmapLayer dayLayer = layerSettings[i];

				if (!pass.Setup(dayLayer, camera))
				{
					continue;
				}

				Rendering.Draw(pass);
			}
		}

		public static Color ClearColor(LightmapPreset lightmapPreset)
		{
		   	float depthFloat = ((float)lightmapPreset.depth + 100) / 255;

		   	Color color = new Color(depthFloat, 0, 0, 0);

			return(color);
		}
	}
}