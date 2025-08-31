using UnityEngine;

public class LadderActivator : MonoBehaviour
{
    public GameObject ladder;  
    public Rigidbody2D ladderRb; // 拖拽梯子本体的刚体// 指向梯子本体
    private bool activated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;
        if (other.CompareTag("Player"))
        {
            ladderRb.bodyType = RigidbodyType2D.Dynamic;
            ladderRb.constraints = RigidbodyConstraints2D.None;

        }
        if (other.CompareTag("LadderTrigger")) 
        {
            ladderRb.angularVelocity = 0f;
            ladderRb.bodyType = RigidbodyType2D.Static;
        }
    }
}