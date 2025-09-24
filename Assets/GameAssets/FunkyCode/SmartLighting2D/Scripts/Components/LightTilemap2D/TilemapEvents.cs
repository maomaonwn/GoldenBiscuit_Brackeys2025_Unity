using GameAssets.FunkyCode.SmartLighting2D.Components.LightCollider;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Components.LightTilemap2D
{
	public class TilemapEvents
	{
		private static bool initialized = false;

		public static void Initialize() {
			if (initialized) {
				return;
			}

			initialized = true;

			#if UNITY_EDITOR

				#if UNITY_2019_4_OR_NEWER
					Tilemap.tilemapTileChanged -= Events;
					Tilemap.tilemapTileChanged += Events;
				#endif

			#endif
		}
		
		#if UNITY_EDITOR
			#if UNITY_2019_4_OR_NEWER
				public static void Events(Tilemap tilemap, Tilemap.SyncTile[] s) {
					if (Application.isPlaying) {
						return;
					}

					foreach(LightTilemapCollider2D tilemap2D in LightTilemapCollider2D.List) {
						tilemap2D.Initialize();
					}
					
					SmartLighting2D.Components.Lightmap.Light2D.ForceUpdateAll();
				}
			#endif
		#endif
	}
}