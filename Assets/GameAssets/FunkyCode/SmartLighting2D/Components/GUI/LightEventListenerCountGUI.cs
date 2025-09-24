using GameAssets.FunkyCode.SmartLighting2D.Components.Event_Handling;
using UnityEngine;

namespace GameAssets.FunkyCode.SmartLighting2D.Components.GUI
{
    [ExecuteInEditMode]
    public class LightEventListenerCountGUI : MonoBehaviour
    {
        static private Texture pointTexture;

        private LightEventListenerCount lightEventReceiver;

        private void OnEnable()
        {
            lightEventReceiver = GetComponent<LightEventListenerCount>();
        }

        void OnGUI()
        {
            if (Camera.main == null)
            {
                return;
            }
            
            Vector2 middlePoint = Camera.main.WorldToScreenPoint(transform.position);

            UnityEngine.GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            
            string display = lightEventReceiver.lights.Count.ToString();

            int size = Screen.height / 20;

            GUIStyle style = new GUIStyle();
            style.fontSize = size;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;

            int pointSize = Screen.height / 80;

            UnityEngine.GUI.Label(new Rect(middlePoint.x - 50, Screen.height - middlePoint.y - 50, 100, 100), display, style);
        }
    }
}