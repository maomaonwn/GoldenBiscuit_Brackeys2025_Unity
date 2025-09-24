using UnityEngine;

namespace GameAssets.FunkyCode.Demos___SmartLighting2D.Scripts
{
	public class DemoFollowMouse : MonoBehaviour {

		void Update () {
			Vector3 pos = GetMousePosition();
			pos.z = transform.position.z;

			transform.position = pos;

	
		}

		public static Vector2 GetMousePosition() {
			if (Camera.main == null) {
				return(Vector2.zero);
			}
			return(Camera.main.ScreenToWorldPoint (Input.mousePosition));
		}
	}
}
