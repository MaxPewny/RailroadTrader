using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoTrainController : MonoBehaviour
{
    public GenericValues values;

    [SerializeField]
    private float timeTilArrival = 45.0f;
    [SerializeField]
    private float passedTime = 0.0f;
    [SerializeField]
    private List<CargoTrack> allTracks = new List<CargoTrack>();
    [SerializeField]
    private int cargoAmountPerTrack = 15;

    public int MaxCargoCapacity { get { return CurCargoCapacity(); } }

    public bool inStation { get; protected set; }

    public static event System.Action OnCargoReset = delegate { };
    public static event System.Action<Dictionary<Resource,int>> OnOrderArrived = delegate { };
    public Dictionary<Resource, int> OrderedCargo { get; private set; }

    //public HashSet<Cargo> StoredCargo { get; private set; }

    private void Awake()
    {
        timeTilArrival = values.CargoTimeTilArrival;
        cargoAmountPerTrack = values.CargoAmountPerTrack;
    }

    void Start()
    {
        OrderedCargo = new Dictionary<Resource, int>();
        BuildingManager.OnCargoTrackCountChange += UpdateTrackList;
        inStation = true;
    }

    void Update()
    {
        if (!inStation)
            Timer();
    }

    private int CurCargoCapacity()
    {
        return cargoAmountPerTrack * allTracks.Count;
    }
    
    private void UpdateTrackList(List<CargoTrack> tracks)
    {
        allTracks = tracks;
        if (inStation)
        {
            foreach (CargoTrack track in allTracks)
            {
                StartCoroutine(track.TrainArrived());
            }
        }
    }

    private void SendCargoTrains()
    {
        inStation = false;
        foreach(CargoTrack track in allTracks)
        {
            StartCoroutine( track.TrainLeaves());
        }
    }

    public void SaveOrder(int foodAmount, int drinkAmount, int cargoAmount)
    {
        if (!inStation)
            return;

        OrderedCargo.Add(Resource.FOOD, foodAmount);
        OrderedCargo.Add(Resource.BEVERAGE, drinkAmount);
        OrderedCargo.Add(Resource.CARGO, cargoAmount);
        OnCargoReset();
        SendCargoTrains();
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
                inStation = true;
            }
            passedTime = 0.0f;
        }
    }
}
