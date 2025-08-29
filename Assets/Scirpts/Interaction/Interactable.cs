using UnityEngine;
using TMPro;
public class Interactable : MonoBehaviour
{
    public LayerMask interactableLayer;
    public Material highlightMaterial;

    [SerializeField] private Camera mainCam;
    [Header("Player & UI")]
    public Transform player;          // 玩家/主角 Transform
    public TMP_Text promptText; // 靠近时提示
    private Renderer lastRend;
    private Material lastOriginalMat;
    private InteractableInfo currentInfo;
    private Collider2D currentCol2D;
    private Dialog _promptInst;

    void Awake()
    {
        if (!mainCam) mainCam = Camera.main;
        if (promptText) promptText.gameObject.SetActive(false);
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
                }
                
                // Check distance
                if (currentInfo != null && player != null)
                {
                    Vector3 closest = currentCol2D.ClosestPoint(player.position);
                    float dist = Vector3.Distance(player.position, closest);
                    // In interaction Distance
                    if (dist <= currentInfo.interactDistance)
                    {
                        ShowPrompt(currentCol2D.transform, currentInfo);
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            DialogueManager.I.Play(hit.collider.transform, currentInfo);
                            DestroyPrompt();
                        }
                    }
                    else
                    {
                        DestroyPrompt(); 
                    }
                }
                else
                {
                    DestroyPrompt();
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
    
    void ShowPrompt(Transform target, InteractableInfo info)
    {
        if (_promptInst == null && DialogueManager.I)
        {
            string tip = info.promptLine;
            _promptInst = DialogueManager.I.ShowPrompt(target, tip);
        }
    }
    void DestroyPrompt()
    {
        if (_promptInst)
        {
            Destroy(_promptInst.gameObject); 
            _promptInst = null;
        }
    }
}
