using UnityEngine;

public class TowerPlacementZone : MonoBehaviour
{
    // Restituisce il centro dei bounds del collider per lo snap
    public Vector3 GetSnapPosition()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            return col.bounds.center;
        }
        return transform.position;
    }
}
