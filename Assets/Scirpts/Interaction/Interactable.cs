using UnityEngine;
using UnityEngine.UI; 

public class Interactable : MonoBehaviour
{
    public LayerMask interactableLayer;
    public Material highlightMaterial;

    [SerializeField] private Camera mainCam;
    [Header("Player & UI")]
    public Transform player;          // 玩家/主角 Transform
    public Text promptText;           // 靠近时提示
    public Text descriptionText;      // 按 E 显示描述

    private Renderer lastRend;
    private Material lastOriginalMat;
    private InteractableInfo currentInfo;
    private Collider2D currentCol2D;

    void Awake()
    {
        if (!mainCam) mainCam = Camera.main;
        if (promptText) promptText.gameObject.SetActive(false);
        if (descriptionText) descriptionText.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, interactableLayer);

        if (hit.collider != null)
        {
            currentCol2D = hit.collider;
            currentInfo  = hit.collider.GetComponent<InteractableInfo>();
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                // Change Material color onHover
                if (lastRend != rend)
                {
                    ResetLast();
                    lastRend = rend;
                    lastOriginalMat = rend.material;
                    rend.material = highlightMaterial;
                    Debug.Log("Material Changed: " + rend.gameObject.name);

                    //currentCol2D = hit.collider;
                   // currentInfo  = hit.collider.GetComponent<InteractableInfo>();
                    if (currentInfo = null)
                        Debug.Log("InteractableInfo not found on " + hit.collider.gameObject.name);
                }
                
                // Check distance
                if (currentInfo != null && player != null)
                {
                    Vector3 closest = currentCol2D.ClosestPoint(player.position);
                    float dist = Vector3.Distance(player.position, closest);

                    Debug.Log("Distance: " + dist);
                    if (dist <= currentInfo.interactDistance)
                    {
                        if (promptText)
                        {
                            promptText.text = string.IsNullOrEmpty(currentInfo.prompt)
                                              ? "Press E to interact" : currentInfo.prompt;
                            promptText.gameObject.SetActive(true);
                        }

                        if (Input.GetKeyDown(KeyCode.F) && descriptionText)
                        {
                            descriptionText.text = currentInfo.description;
                            descriptionText.gameObject.SetActive(true);
                            CancelInvoke(nameof(HideDescription));
                            Invoke(nameof(HideDescription), 3f);// Hide after 3 sec
                        }
                    }
                    else
                    {
                        if (promptText) promptText.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (promptText) promptText.gameObject.SetActive(false);
                }

                return;
            }
        }

        ResetLast();
    }

    void ResetLast()
    {
        if (lastRend != null) lastRend.material = lastOriginalMat;
        lastRend = null;
        lastOriginalMat = null;
        currentInfo = null;
        currentCol2D = null;
        if (promptText) promptText.gameObject.SetActive(false);
    }

    void HideDescription()
    {
        if (descriptionText) descriptionText.gameObject.SetActive(false);
    }
}
