using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject towerPrefab;
    private GameObject towerPreview;
    private Camera mainCamera;

    [Header("Tower DATA")]
    [SerializeField] private int towerCost;
    [SerializeField] private TextMeshProUGUI towerCostText;

    private void Awake()
    {
        mainCamera = Camera.main;
        towerCostText.text = "$" + towerCost.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.instance.GetMoney() < towerCost) return;

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

        int ignoreLayer = LayerMask.NameToLayer("Turret");
        int layerMask = ~(1 << ignoreLayer);

        Ray ray = mainCamera.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
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
                    Turret turret = towerPreview.GetComponent<Turret>();
                    if (turret != null)
                    {
                        zone.AddTurret(turret);
                        turret.isPlaced = true;
                    }
                    PowerUp powerUp = towerPreview.GetComponent<PowerUp>();
                    if (powerUp != null)
                    {
                        if (zone.turrets.Count <= 0)
                        {
                            Destroy(towerPreview);
                            return;
                        }
                        powerUp.AssignZone(zone);
                        powerUp.ApplyPowerupForeachTurret();
                    }
                    zone.AddStackNum();
                    GameManager.instance.RemoveMoney(towerCost);
                    towerPreview = null;
                }
                else
                {
                    // Altrimenti usa il centro dei bounds del collider
                    towerPreview.transform.position = hit.collider.bounds.center;
                }
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
