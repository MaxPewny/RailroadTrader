using UnityEngine;

[CreateAssetMenu(fileName = "BuildCost", menuName = "Scriptables/BuildCostValues")]
public class BuildCostValues : ScriptableObject
{
    [Tooltip("Type of the building")]
    public StoreType StoreType;
    [Tooltip("Name of the building")]
    public BuildingType Name;
    [Tooltip("How much building this costs")]
    public int BuildCost;
}
