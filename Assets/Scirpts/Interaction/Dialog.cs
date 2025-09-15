using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TMP_Text text;
    public Transform followTarget;
    public Camera cam;
    public Vector3 offset;

    void Awake()
    {
        if (!cam) cam = Camera.main;
    }

    public void Setup(Transform target, string content)
    {
        followTarget = target;
        if (text) text.text = content;
    }

    void LateUpdate()
    {
        if (followTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = followTarget.position;
        if (cam) transform.forward = (transform.position - cam.transform.position).normalized;
    }
}
