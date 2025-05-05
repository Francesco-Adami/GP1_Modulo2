using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacementZone : MonoBehaviour
{
    public int stackNum = 0;

    public List<Turret> turrets = new List<Turret>();

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
        //    {
        //        // print("Hit: " + hit.collider.name);
        //        TowerPlacementZone towerDragHandler = hit.collider.GetComponent<TowerPlacementZone>();

        //        if (towerDragHandler != null)
        //        {
        //            print("Hit: " + hit.collider.name);
        //            StartMovingTurret(turrets[^1]);
        //        }
        //    }

        //    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
        //    {
        //        TowerPlacementZone towerDragHandler = hit.collider.GetComponent<TowerPlacementZone>();
        //        if (towerDragHandler != null)
        //        {
        //            print("Hit: " + hit.collider.name);
        //            StartMovingTurret(turrets[^1]);
        //        }
        //    }
        //}
    }

    private void StartMovingTurret(Turret turret)
    {
        turret.isPlaced = false;
        turret.gameObject.SetActive(false);


    }

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
    public void AddTurret(Turret turret) { turrets.Add(turret); }
    public void RemoveTurret(Turret turret) { turrets.Remove(turret); }

}
