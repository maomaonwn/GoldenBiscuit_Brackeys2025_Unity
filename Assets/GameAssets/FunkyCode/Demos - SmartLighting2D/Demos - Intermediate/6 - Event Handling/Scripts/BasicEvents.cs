using GameAssets.FunkyCode.SmartLighting2D.Components.LightCollider;
using GameAssets.FunkyCode.SmartLighting2D.Components.Lightmap;
using UnityEngine;

namespace GameAssets.FunkyCode.Demos___SmartLighting2D.Demos___Intermediate._6___Event_Handling.Scripts
{
    [ExecuteInEditMode]
    public class BasicEvents : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;

        void Awake() {
            LightCollider2D collider = GetComponent<LightCollider2D>();

            collider.AddEventOnEnter(OnEnter);
            collider.AddEventOnExit(OnExit);

            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void OnEnter(Light2D light) {
            spriteRenderer.color = Color.red;
        }

        void OnExit(Light2D light) {
            spriteRenderer.color = Color.green;
        }
    }
}