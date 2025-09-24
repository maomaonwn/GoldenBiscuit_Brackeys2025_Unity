using UnityEngine;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Misc
{
	public class LightingMonoBehaviour : MonoBehaviour
	{
		public void DestroySelf()
		{
			if (Application.isPlaying)
			{
				Destroy(this.gameObject);
			}
				else
			{
				if (this && this.gameObject)
				{
					DestroyImmediate(this.gameObject);
				}
			}
		}
	}
}