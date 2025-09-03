using Scirpts.Manager;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    [Header("Detect")]
    public LayerMask interactableLayer;
    [SerializeField] private Camera mainCam;

    [Header("Visual")]
    public Material highlightMaterial;

    [Header("Player")]
    public Transform player;
    
    [Header("Prefabs")]
    public Dialog promptPrefab;      // 提示用气泡
    public Transform worldRoot;
    
    private Renderer lastRend;
    private Material lastOriginalMat;
    private InteractableInfo currentInfo;
    private Collider2D currentCol2D;
    private Dialog promptInst;
    private float lastDist;             

    void Awake()
    {
        if (!mainCam) mainCam = Camera.main;
    }

    void Update()
    {
        var hit = RaycastFromMouse();
        if (hit.collider != null)
        {
            CacheHitContext(hit);

            // Highlight on Hover
            HandleHover();
            // Distance detection
            HandleProximity();
            // pickups/ dialog
            HandleInteract(hit); 
            return;
        }

        ClearTarget();
    }

    #region Information

    RaycastHit2D RaycastFromMouse()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        return Physics2D.GetRayIntersection(ray, Mathf.Infinity, interactableLayer);
    }

    void CacheHitContext(RaycastHit2D hit)
    {
        currentCol2D = hit.collider;
        
        currentInfo = currentCol2D.GetComponent<InteractableInfo>()
                      ?? currentCol2D.GetComponentInParent<InteractableInfo>()
                      ?? currentCol2D.GetComponentInChildren<InteractableInfo>();
    }
    #endregion

    #region  Highlight on Hover

    void HandleHover()
    {
        var rend = currentCol2D.GetComponent<Renderer>();
        if (!rend) return;

        // Restore last render, highlight new render
        if (lastRend != rend)
        {
            RestoreLastRenderer();
            lastRend = rend;
            lastOriginalMat = rend.material;
            if (highlightMaterial) rend.material = highlightMaterial;
        }
    }

    void RestoreLastRenderer()
    {
        if (lastRend == null) { lastOriginalMat = null; return; }
        
        if (lastOriginalMat != null)
        {
            try { lastRend.material = lastOriginalMat; }
            catch {}
        }

        lastRend = null;
        lastOriginalMat = null;
    }
    #endregion

    #region Distance detection
    void HandleProximity()
    {
        if (currentInfo == null || player == null) { HidePrompt(); return; }

        Vector3 closest = currentCol2D.ClosestPoint(player.position);
        lastDist = Vector3.Distance(player.position, closest);

        var machine = currentCol2D.GetComponentInParent<CookieMachine>()
                      ?? currentCol2D.GetComponent<CookieMachine>()
                      ?? currentCol2D.GetComponentInChildren<CookieMachine>();

        if (machine != null)
        {
            if (lastDist <= currentInfo.interactDistance)
                ShowPrompt(currentCol2D.transform, machine.GetPromptText());
            else
                HidePrompt();
            return;
        }

        // 普通交互物体
        if (lastDist <= currentInfo.interactDistance)
            ShowPrompt(currentCol2D.transform, currentInfo.promptLine);
        else
            HidePrompt();
    }

    
    #endregion
    
    #region HandleInteraction

    void HandleInteract(RaycastHit2D hit)
    {
        if (currentInfo == null || player == null) return;
        if (lastDist > currentInfo.interactDistance) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            var machine = currentCol2D.GetComponentInParent<CookieMachine>()
                          ?? currentCol2D.GetComponent<CookieMachine>()
                          ?? currentCol2D.GetComponentInChildren<CookieMachine>();

            if (machine != null)
            {
                var inv = player.GetComponent<PlayerInventory>();

                // Machine is depleted
                if (machine.IsDepleted)
                {
                    HidePrompt();
                    return;
                }

                // Machine can be used
                if (machine.TryUse(inv, out int dropped))
                {
                    HidePrompt();
                    return;
                }
                else
                {
                    // in cooldown process
                    return;   
                }
            }

            // Pick up cookies
            if (TryPickupCookie()) { HidePrompt(); return; }

            // Trigger Dialogs by press key
            var trigger = hit.collider.GetComponentInChildren<DialogTrigger>();
            if(trigger && trigger.mode == DialogTriggerMode.PressKey)
                trigger.TriggerDialogue();
        }
    }


    bool TryPickupCookie()
    {
        var pickup = currentCol2D.GetComponent<CookiePickup>()
                     ?? currentCol2D.GetComponentInParent<CookiePickup>()
                     ?? currentCol2D.GetComponentInChildren<CookiePickup>();
        if (pickup == null) return false;

        // Try to pick up cookie
        var inv = player.GetComponent<PlayerInventory>();
        if (pickup.Collect(inv))
        {
            // Play Audio?
            AudioManager.instance?.PlaySoundEffect(SoundEffectName.Player_Pickup);
            return true;
        }
        return false;
    }

    #endregion


    #region Handle Prompts
    
    Dialog PromptSetup(Transform target, string text)
    {
        var promptDialog = Instantiate(promptPrefab, worldRoot);
        promptDialog.Setup(target, text);
        return promptDialog;
    }
    
    void ShowPrompt(Transform target, string text)
    {
        if (promptInst == null)
            promptInst = PromptSetup(target, text);
        else
            promptInst.text.text = text;
    }

    void HidePrompt()
    {
        if (promptInst)
        {
            Destroy(promptInst.gameObject);
            promptInst = null;
        }
    }

    

    #endregion

    // Clean up
    void ClearTarget()
    {
        HidePrompt();
        RestoreLastRenderer();
        currentInfo = null;
        currentCol2D = null;
        lastDist = 0f;
    }


}
