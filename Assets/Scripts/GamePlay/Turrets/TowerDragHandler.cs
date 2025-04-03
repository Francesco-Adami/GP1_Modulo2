using UnityEngine;
using UnityEngine.EventSystems;

public class TowerDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject towerPrefab;
    private GameObject towerPreview;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (towerPrefab != null)
        {
            towerPreview = Instantiate(towerPrefab);
            // Potresti disabilitare i collider della preview per evitare interazioni indesiderate durante il drag.
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (towerPreview == null)
            return;

        // Usa un piano orizzontale a y = 0 per determinare la posizione nel mondo
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = mainCamera.ScreenPointToRay(eventData.position);
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 worldPos = ray.GetPoint(distance);
            towerPreview.transform.position = worldPos;
        }
    }

    // Quando termina il drag, verifica se la posizione è valida e "snappa" la torretta al centro della zona di placement
    public void OnEndDrag(PointerEventData eventData)
    {
        if (towerPreview == null)
            return;

        Ray ray = mainCamera.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Controlla se l'oggetto colpito è una zona di placement (con tag "TowerPlacement")
            if (hit.collider.CompareTag("TowerPlacement"))
            {
                // Prova a ottenere lo script TowerPlacementZone
                TowerPlacementZone zone = hit.collider.GetComponent<TowerPlacementZone>();

                if (zone != null)
                {
                    // se ha già 3 torrette non si aggiungono allo stack
                    if (zone.GetStackNum() >= 3)
                    {
                        Destroy(towerPreview);
                        return;
                    }

                    // Snappa la torretta al centro della zona
                    towerPreview.transform.position = zone.GetSnapPosition();
                    zone.AddStackNum();
                }
                else
                {
                    // Altrimenti usa il centro dei bounds del collider
                    towerPreview.transform.position = hit.collider.bounds.center;
                }
                // (Opzionale) Qui puoi attivare altri componenti o trasferire l'oggetto a un manager.
            }
            else
            {
                // Se il drop non è valido, distruggi la preview
                Destroy(towerPreview);
            }
        }
        else
        {
            Destroy(towerPreview);
        }
    }
}
