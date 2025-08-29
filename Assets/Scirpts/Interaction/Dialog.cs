using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TMP_Text text;
    public Transform followTarget;
    public Camera cam;

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
        // 跟随并朝向相机（简单的朝向）
        transform.position = followTarget.position;
        if (cam) transform.forward = (transform.position - cam.transform.position).normalized;
    }
}