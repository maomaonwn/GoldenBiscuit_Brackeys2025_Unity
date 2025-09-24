using UnityEngine;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Material.Extended
{
	[System.Serializable]
	public class Shadow
	{
		private Sprite penumbraSprite;
		private Sprite penumbraSprite2;

		private LightingMaterial softShadow = null;
		private LightingMaterial softShadowDefault = null;

		private LightingMaterial alphaShadow = null;
		private LightingMaterial legacyGPUShadow = null;
		private LightingMaterial legacyCPUShadow = null;

		private LightingMaterial spriteProjection = null;

		private LightingMaterial dayCPUShadow = null;
		private LightingMaterial spriteShadow = null;

		private LightingMaterial depthDayShadow = null;

		private LightingMaterial softDistanceShadow = null;

		private LightingMaterial fastShadow = null;

		public void Reset()
		{
			penumbraSprite = null;

			softShadow = null;
			softShadowDefault = null;
			fastShadow = null;

			alphaShadow = null;
			legacyGPUShadow = null;
			legacyCPUShadow = null;

			depthDayShadow = null;
		
			dayCPUShadow = null;
			spriteProjection = null;

			spriteShadow = null;

			softDistanceShadow = null;
		}

		public void Initialize()
		{
			GetPenumbraSprite();
			GetPenumbraSprite2();

			GetSoftShadow();
			GetSoftDistanceShadow();

			GetLegacyGPUShadow();
			GetLegacyCPUShadow();

			GetDayCPUShadow();
			GetSpriteShadow();
		}

		public UnityEngine.Material GetDepthDayShadow()
		{
			if (depthDayShadow == null || depthDayShadow.Get() == null)
			{
				depthDayShadow = LightingMaterial.Load("Light2D/Internal/Depth/DayShadow");
			}

			return(depthDayShadow.Get());
		}

		public UnityEngine.Material GetAlphaShadow()
		{
			if (alphaShadow == null || alphaShadow.Get() == null)
			{
				alphaShadow = LightingMaterial.Load("Light2D/Internal/AlphaShadow");

				alphaShadow.SetTexture("textures/white");
			}

			return(alphaShadow.Get());
		}
	
		public UnityEngine.Material GetSoftShadow()
		{
			if (softShadow == null || softShadow.Get() == null)
			{
				softShadow = LightingMaterial.Load("Light2D/Internal/Shadow/SoftShadow");
			}

			return(softShadow.Get());
		}

		public UnityEngine.Material GetSoftShadowDefault()
		{
			if (softShadowDefault == null || softShadowDefault.Get() == null)
			{
				softShadowDefault = LightingMaterial.Load("Light2D/Internal/Shadow/SoftDefault");
			}

			return(softShadowDefault.Get());
		}

		public UnityEngine.Material GetFastShadow()
		{
			if (fastShadow == null || fastShadow.Get() == null)
			{
				fastShadow = LightingMaterial.Load("Light2D/Internal/Shadow/Fast");
			}

			return(fastShadow.Get());
		}


		public UnityEngine.Material GetLegacyGPUShadow()
		{
			if (legacyGPUShadow == null || legacyGPUShadow.Get() == null)
			{
				legacyGPUShadow = LightingMaterial.Load("Light2D/Internal/Shadow/LegacyGPU");

				if (legacyGPUShadow.Get() != null)
				{
					legacyGPUShadow.Get().mainTexture = GetPenumbraSprite().texture;
				}
			}

			return(legacyGPUShadow.Get());
		}

		public UnityEngine.Material GetSoftDistanceShadow()
		{
			if (softDistanceShadow == null || softDistanceShadow.Get() == null)
			{
				softDistanceShadow = LightingMaterial.Load("Light2D/Internal/Shadow/SoftDistance");

				if (softDistanceShadow.Get() != null)
				{
					softDistanceShadow.Get().mainTexture = GetPenumbraSprite2().texture;
				}
			}

			return(softDistanceShadow.Get());
		}

		public UnityEngine.Material GetLegacyCPUShadow()
		{
			if (legacyCPUShadow == null || legacyCPUShadow.Get() == null)
			{
				legacyCPUShadow = LightingMaterial.Load("Light2D/Internal/Shadow/LegacyCPU");

				if (legacyCPUShadow.Get() != null)
				{
					legacyCPUShadow.Get().mainTexture = GetPenumbraSprite().texture;
				}
			}

			return(legacyCPUShadow.Get());
		}

		public Sprite GetPenumbraSprite()
		{
			if (penumbraSprite == null)
			{
				penumbraSprite = Resources.Load<Sprite>("textures/penumbra"); 
			}

			return(penumbraSprite);
		}

		
		public Sprite GetPenumbraSprite2()
		{
			if (penumbraSprite2 == null)
			{
				penumbraSprite2 = Resources.Load<Sprite>("textures/penumbra2"); 
			}

			return(penumbraSprite2);
		}

		public UnityEngine.Material GetDayCPUShadow()
		{
			if (dayCPUShadow == null || dayCPUShadow.Get() == null)
			{
				dayCPUShadow = LightingMaterial.Load("Light2D/Internal/Day/SoftShadow");
			}

			return(dayCPUShadow.Get());
		}

		public UnityEngine.Material GetSpriteShadow()
		{
			if (spriteShadow == null || spriteShadow.Get() == null)
			{
				spriteShadow = LightingMaterial.Load("Light2D/Internal/SpriteShadow");
			}

			return(spriteShadow.Get());
		}

		public UnityEngine.Material GetSpriteProjectionMaterial()
		{
			if (spriteProjection == null || spriteProjection.Get() == null)
			{
				spriteProjection = LightingMaterial.Load("Light2D/Internal/SpriteProjection");
			}
			
			return(spriteProjection.Get());
		}

	}
}