using GameAssets.FunkyCode.SmartLighting2D.Scripts.Scriptable;
using UnityEngine;

namespace GameAssets.FunkyCode.Demos___SmartLighting2D.Demos___Scriptable.Demo_1___Light_Sprite
{
    [ExecuteInEditMode]
    public class LightSpriteSample : MonoBehaviour
    {
        public Sprite sprite;

        public LightSprite2D lightSprite;
    
        void Start() {
            LightSprite2D light = new LightSprite2D();

            light.SetActive(true);

            light.Sprite = sprite;

            light.Position = Vector3.zero;
            light.Scale = Vector3.one;
            light.Rotation = 0;

            lightSprite = light;
        }
    }
}