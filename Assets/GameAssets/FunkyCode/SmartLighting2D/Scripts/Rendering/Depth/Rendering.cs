using GameAssets.FunkyCode.SmartLighting2D.Components.DayLightCollider;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Depth.Shadow;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Settings;
using UnityEngine;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Depth
{
	public static class Rendering
	{
		public static void Draw(Pass pass)
		{
			Shadow.Shadow.Begin(); // quads

			DrawCollider(pass);

			Shadow.Shadow.End();

			DrawColliderFill(pass); // triangles

			DrawSprite(pass);
			
		}

		public static void DrawSprite(Pass pass)
		{
			SpriteRendererShadow.Begin(pass.offset);

			for(int i = 0; i < pass.colliderCount; i++)
			{
				DayLightCollider2D id = pass.colliderList[i];

				if (id.shadowLayer != pass.layerId)
				{
					continue;
				}

				switch(id.mainShape.shadowType)
				{
					case DayLightCollider2D.ShadowType.SpriteOffset:

						SpriteRendererShadow.DrawOffset(id);

					break;
				}
			}

			if (SpriteRendererShadow.currentTexture != null)
			{
				SpriteRendererShadow.End();
			}
		}

		public static void DrawCollider(Pass pass)
		{
			for(int i = 0; i < pass.colliderCount; i++)
			{
				DayLightCollider2D id = pass.colliderList[i];
				
				if (id.shadowLayer != pass.layerId)
				{
					continue;
				}

				switch(id.mainShape.shadowType)
				{
					case DayLightCollider2D.ShadowType.SpritePhysicsShape:
					case DayLightCollider2D.ShadowType.Collider2D:

						Shadow.Shadow.Draw(id, pass.offset);  

					break;
				}             
			}
		}

		public static void DrawColliderFill(Pass pass)
		{
			Lighting2D.Materials.shadow.GetDepthDayShadow().SetPass(0);
			GL.Begin(GL.TRIANGLES);

			for(int i = 0; i < pass.colliderCount; i++)
			{
				DayLightCollider2D id = pass.colliderList[i];
				
				if (id.shadowLayer != pass.layerId)
				{
					continue;
				}

				switch(id.mainShape.shadowType)
				{
					case DayLightCollider2D.ShadowType.FillCollider2D:
					case DayLightCollider2D.ShadowType.FillSpritePhysicsShape:

						Shadow.Shadow.DrawFill(id, pass.offset); 

					break;
				}             
			}

			GL.End();
		}
	}
}