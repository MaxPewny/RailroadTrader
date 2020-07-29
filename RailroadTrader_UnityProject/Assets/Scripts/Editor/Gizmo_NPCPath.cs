using UnityEditor;
using UnityEngine;

public class Gizmo_NPCPath
{ 
    [DrawGizmo(GizmoType.NonSelected)]
    static void DrawEnemyInfo(NpcActor npc, GizmoType gizmoType)
    {
        if (!npc.hasTarget)
            return;

        Vector3 goal = new Vector3(npc._target.x, npc._target.y, npc._target.z);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(npc.transform.position, goal);
    }
}