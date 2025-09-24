using GameAssets.FunkyCode.SmartLighting2D.Components.Lightmap;
using UnityEngine;

namespace GameAssets.FunkyCode.SmartLighting2D.Components.Effects
{
    public class LightBlink : MonoBehaviour
    {
        public Color primaryColor = Color.white;
        public Color secondaryColor = Color.black;

        private Light2D lightingSource;
        
        void Start() {
            lightingSource = GetComponent<Light2D>();
        }

        
        void Update() {
            float time = Time.realtimeSinceStartup;
            float step = Mathf.Cos(time);
            Color color = Color.Lerp(primaryColor, secondaryColor, Mathf.Abs(step));

            lightingSource.color = color;

            lightingSource.meshMode.alpha = color.a * 0.5f;
        }
    }
}