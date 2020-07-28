using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Gizmo_SupplyStoreColliders 
{
    [DrawGizmo(GizmoType.NonSelected)]
    static void DrawNonSelectedBuildings(GizmoShop store, GizmoType gizmoType)
    {
        NavMeshObstacle col = store.gameObject.GetComponentInChildren<NavMeshObstacle>();
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(col.center+store.transform.position, col.size);
        Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(store.transform.position, store.maxR);
    }

    [DrawGizmo(GizmoType.Selected)]
    static void DrawSelectedBuildings(GizmoShop store, GizmoType gizmoType)
    {
        NavMeshObstacle col = store.gameObject.GetComponentInChildren<NavMeshObstacle>();
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(col.center + store.transform.position, col.size);
        Gizmos.color = Color.magenta;
    }

}