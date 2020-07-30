using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassengerTrack", menuName = "Scriptables/PassengerTrackValues")]
public class PassengerTrackValues : ScriptableObject
{
    //public int BuildCost;
    [Tooltip("How much this building costs per hour")]
    public int UpkeepCost = 25;
    [Tooltip("Type of NPC this track spawns")]
    public Passanger PassangerType;
    [Tooltip("Time between departure and arrival in seconds")]
    public float TimeTilArrival = 45.0f;
}
