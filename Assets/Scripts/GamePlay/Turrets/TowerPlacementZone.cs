using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacementZone : MonoBehaviour
{
    public int stackNum = 0;

    // Restituisce il centro dei bounds del collider per lo snap
    public Vector3 GetSnapPosition()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            return col.bounds.center + new Vector3(0, 0.5f, 0) + Vector3.up * stackNum;
        }
        return transform.position;
    }

    public void AddStackNum() { stackNum++; }
    public int GetStackNum() { return stackNum; }


}
