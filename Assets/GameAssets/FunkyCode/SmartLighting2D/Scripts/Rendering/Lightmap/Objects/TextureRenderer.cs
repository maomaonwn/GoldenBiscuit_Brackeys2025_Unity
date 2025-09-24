using GameAssets.FunkyCode.SmartLighting2D.Components.Lightmap;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Universal.Objects;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Settings;
using UnityEngine;
using Texture = GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Universal.Objects.Texture;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Lightmap.Objects
{
    public class TextureRenderer
	{
		public static void Draw(LightTexture2D id, UnityEngine.Camera camera)
		{
			if (!id.InCamera(camera))
				return;

			Vector2 offset = -camera.transform.position;

			UnityEngine.Material material;

			switch(id.shaderMode)
			{
				case LightTexture2D.ShaderMode.Additive:

					material = Lighting2D.Materials.GetAdditive();
					material.mainTexture = id.texture;

					GLExtended.color = id.color;

					Texture.Quad.Draw(material, new Vector3(offset.x, offset.y) + id.transform.position, id.size, 0, 0);
					
					material.mainTexture = null;

				break;

				case LightTexture2D.ShaderMode.Multiply:

					material = Lighting2D.Materials.GetMultiplyHDR();
					material.mainTexture = id.texture;

					GLExtended.color = id.color;

					Texture.Quad.Draw(material, new Vector3(offset.x, offset.y) + id.transform.position, id.size, 0, 0);
					
					material.mainTexture = null;

				break;
			}
		}
    }
}