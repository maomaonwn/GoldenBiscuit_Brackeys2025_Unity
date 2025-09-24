using UnityEngine;

namespace GameAssets.FunkyCode.SmartUtilities2D.Scripts.Utilities.Misc
{
	public class DestroyTimer : MonoBehaviour {
		TimerHelper timer;

		void Start () {
			timer = TimerHelper.Create();
		}
		
		void Update () {
			if (timer.GetMillisecs() > 2000) {
				Destroy(gameObject);
			}
		}
	}
}