using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Supplystore", menuName = "Scriptables/SupplystoreValues" )]
public class SupplystoreValues : ScriptableObject
{
    [Tooltip("How much building this costs")]
    public int BuildCost;
    [Tooltip("How much this building costs per hour")]
    public int UpkeepCost = 25;
    [Tooltip("Seconds til 1 NPC leaves the shop")]
    public float SecondsSpendByNPC = 30.0f;
    [Tooltip("Resources this building uses and their amounts")]
    public List<BuildingRessource> Resources;
    [Tooltip("Resources this building uses and their amounts")]
    public List<VisitorStats> VisitorStats;
}
