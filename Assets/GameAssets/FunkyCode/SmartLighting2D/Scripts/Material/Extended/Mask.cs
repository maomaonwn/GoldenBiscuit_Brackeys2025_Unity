namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Material.Extended
{
	[System.Serializable]
	public class Mask
	{
		private LightingMaterial mask = null;
		private LightingMaterial maskTranslucency = null;
		private LightingMaterial dayMask = null;

		public void Reset() 
		{
			mask = null;
			dayMask = null;
			maskTranslucency = null;
		}
		
		public UnityEngine.Material GetMask()
		{
			if (mask == null || mask.Get() == null)
			{
				mask = LightingMaterial.Load("Light2D/Internal/Mask");
			}

			return(mask.Get());
		}
				
		public UnityEngine.Material GetMaskTranslucency()
		{
			if (maskTranslucency == null || maskTranslucency.Get() == null)
			{
				maskTranslucency = LightingMaterial.Load("Light2D/Internal/MaskTranslucency");
			}

			return(maskTranslucency.Get());
		}

		public UnityEngine.Material GetDayMask()
		{
			if (dayMask == null || dayMask.Get() == null)
			{
				dayMask = LightingMaterial.Load("Light2D/Internal/DayMask");
			}

			return(dayMask.Get());
		}

		public void Initialize()
		{
			GetMask();
			GetMaskTranslucency();
			GetDayMask();
		}
	}
}