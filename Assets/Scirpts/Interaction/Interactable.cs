using UnityEngine;

public class Interactable : MonoBehaviour
{
    public LayerMask interactableLayer;
    public Material highlightMaterial;

    [SerializeField] private Camera mainCam;

    private Renderer lastRend;
    private Material lastOriginalMat;

    void Awake()
    {
        if (!mainCam) mainCam = Camera.main;
    }

    void Update()
    {
        // Ray detection by mouse position
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, interactableLayer);

        if (hit.collider != null)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                // new interactable 
                if (lastRend != rend)
                {
                    // reset last interactable
                    ResetLast();

                    // set new interactable
                    lastRend = rend;
                    lastOriginalMat = rend.material;
                    rend.material = highlightMaterial;
                }
                return;
            }
        }
        
        ResetLast();
    }

    void ResetLast()
    {
        if (lastRend != null)
        {
            lastRend.material = lastOriginalMat;
            lastRend = null;
            lastOriginalMat = null;
        }
    }
}