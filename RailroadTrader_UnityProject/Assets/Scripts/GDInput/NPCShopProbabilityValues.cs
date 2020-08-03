using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NPCVisitProbabilities", menuName = "Scriptables/NPCShopVisitProbabilities")]
public class NPCShopProbabilityValues : ScriptableObject
{ 
    [Header("Probabilities for all NPC types that they visit a certain store")]
    [Tooltip("Probability commuter visits a store")]
    public MovementProbabilities CommuterVisitValues = new MovementProbabilities(Passanger.COMMUTER);
    [Tooltip("Probability tourist visits a store")]
    public MovementProbabilities TouristVisitValues = new MovementProbabilities(Passanger.TOURIST);
    [Tooltip("Probability bussiness visits a store")]
    public MovementProbabilities BusinessVisitValues = new MovementProbabilities(Passanger.BUSINESS);
}
