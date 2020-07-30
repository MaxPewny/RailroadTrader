using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generics", menuName = "Scriptables/GenericValues")]
public class GenericValues : ScriptableObject
{
    [Header("General values")]
    [Tooltip("How many shops the player can build")]
    public int MaxShopAmount = 10;
    [Tooltip("How much tracks  the player can build")]
    public int MaxTrackAmount = 6;
    [Tooltip("How many real time seconds 1 ingame hour lasts")]
    public int RealTimeSecsPerHour = 5;
    [Tooltip("Money amount the player starts the game with")]
    public int StartMoney = 1000;

    [Header("Cargo specific values")]
    [Tooltip("How much cargo one track can carry in t")]
    public int CargoAmountPerTrack = 15;
    [Tooltip("How long it takes for cargo to arrive in seconds")]
    public float CargoTimeTilArrival = 45.0f;

    //public int FastSpeed = 0.2f;
    //public int FastestSpeed = 0.4f;
}
