namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Material.Extended
{

	[System.Serializable]
	public class BumpMask {

		private LightingMaterial normalPixelToLightSprite = null;
		private LightingMaterial normalObjectToLightSprite = null;

		private LightingMaterial bumpedDaySprite = null;

	
		public void Reset() {
			normalObjectToLightSprite = null;
			normalPixelToLightSprite = null;
			bumpedDaySprite = null;
		}

		public void Initialize() {
			GetNormalMapSpritePixelToLight();
			GetNormalMapSpriteObjectToLight();
	
			GetBumpedDaySprite();
		}

				
		public UnityEngine.Material GetNormalMapSpritePixelToLight() {
			if (normalPixelToLightSprite == null || normalPixelToLightSprite.Get() == null) {
				normalPixelToLightSprite = LightingMaterial.Load("Light2D/Internal/BumpMap/PixelToLight");
			}
			return(normalPixelToLightSprite.Get());
		}

		public UnityEngine.Material GetNormalMapSpriteObjectToLight() {
			if (normalObjectToLightSprite== null || normalObjectToLightSprite.Get() == null) {
				normalObjectToLightSprite = LightingMaterial.Load("Light2D/Internal/BumpMap/ObjectToLight");
			}
			return(normalObjectToLightSprite.Get());
		}

		public UnityEngine.Material GetBumpedDaySprite() {
			if (bumpedDaySprite == null || bumpedDaySprite.Get() == null) {
				bumpedDaySprite = LightingMaterial.Load("Light2D/Internal/BumpMap/Day");
			}
			return(bumpedDaySprite.Get());
		}

	}
}