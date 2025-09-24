using UnityEngine;

namespace Scirpts
{
    public class LadderFreeze : MonoBehaviour
    {
        public Rigidbody2D ladderRb; // 拖拽梯子本体的刚体

        private void OnTriggerEnter2D(Collider2D other)
        {
        
        }
    }
}