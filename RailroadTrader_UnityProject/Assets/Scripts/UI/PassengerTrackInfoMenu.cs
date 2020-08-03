using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerTrackInfoMenu : MonoBehaviour
{
    public Text TrainName;
    public Text DepartureTime;
    public Text ArrivalTime;
    public bool timersRunning { get; private set; }
    public PassengerTrack curTrack;


    private void Start()
    {
        OnObjectClicked.OnPassengerTrackClicked += GetClickedTrackInfo;
    }

    public void Update()
    {
        if (timersRunning)
        {
            //TODO update the texts
        }
    }

    public void GetClickedTrackInfo(PassengerTrack curTrack)
    {
        this.curTrack = curTrack;
        timersRunning = true;
    }
}
