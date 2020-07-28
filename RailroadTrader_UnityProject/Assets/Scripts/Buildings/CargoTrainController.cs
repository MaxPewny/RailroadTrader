using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoTrainController : MonoBehaviour
{
    [SerializeField]
    private float timeTilArrival = 45.0f;
    [SerializeField]
    private float passedTime = 0.0f;
    [SerializeField]
    private List<CargoTrack> allTracks = new List<CargoTrack>();

    public bool inStation { get; protected set; }

    public static event System.Action OnCargoReset = delegate { };
    public static event System.Action<HashSet<Cargo>> OnOrderArrived = delegate { };
    public HashSet<Cargo> OrderedCargo { get; private set; }

    //public HashSet<Cargo> StoredCargo { get; private set; }

    void Start()
    {
        BuildingManager.OnCargoTrackCountChange += UpdateTrackList;
    }

    void Update()
    {
        if (!inStation)
            Timer();
    }

    private void UpdateTrackList(List<CargoTrack> tracks)
    {
        allTracks = tracks;
    }

    //Called via button in the cargo ui
    public void ConfirmOrder()
    {
        foreach(CargoTrack track in allTracks)
        {
            StartCoroutine( track.TrainLeaves());
        }
        OnCargoReset();
        inStation = false;
    }

    private void Timer()
    {
        passedTime += Time.deltaTime;
        if (passedTime >= timeTilArrival)
        {
            foreach (CargoTrack track in allTracks)
            {
                StartCoroutine(track.TrainArrived());
                OnOrderArrived(OrderedCargo);
                OrderedCargo.Clear();
            }
            passedTime = 0.0f;
        }
    }
}
