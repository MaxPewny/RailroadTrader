using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gizmo_SupplyStoreColliders 
{
    [DrawGizmo(GizmoType.NonSelected)]
    static void DrawAreaOfEffectZone(GizmoShop store, GizmoType gizmoType)
    {
        BoxCollider col = store.gameObject.GetComponentInChildren<BoxCollider>();
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(col.center, col.size);
        Gizmos.color = Color.magenta;
        //Gizmos.DrawWireSphere(store.transform.position, store.maxR);
    }

    //[DrawGizmo(GizmoType.Selected)]
    //static void DrawSelectedAreaOfEffectZone(ExpandingAOE zone, GizmoType gizmoType)
    //{
    //    SphereCollider col = zone.gameObject.GetComponent<SphereCollider>();
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(zone.transform.position, col.radius);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(zone.transform.position, zone.maxR);
    //}
}