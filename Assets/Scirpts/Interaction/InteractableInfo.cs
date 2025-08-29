using UnityEngine;

public class InteractableInfo : MonoBehaviour
{
    [Header("Interact")]
    public float interactDistance = 2.5f;
    [TextArea] public string promptLine = "Press F to interact";

    [Header("Dialog")]
    [TextArea] public string objectLine = "This line from object";
    public float objectDuration = 2f;          // 物体对白显示时长
    [TextArea] public string playerReply = "This line from player";
    public float replyDelay = 0.5f;            // 物体对白后延时
    public float playerDuration = 2f;          // 玩家对白显示时长
    public float promptDuration = 2f;          // 玩家对白显示时长
}