using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerTrackInfoMenu : MonoBehaviour
{
    public GameObject PTrackMenu;
    public Text TrainName;
    public Text DepartureTime;
    public Text ArrivalTime;

    [SerializeField]
    private PassengerTrack curTrack;
    private bool timersRunning { get { return PTrackMenu.activeSelf ? true : false; } }


    private void Start()
    {
        OnObjectClicked.OnPassengerTrackClicked += GetClickedTrackInfo;
        ArrivalTime.text = "0:00";
        DepartureTime.text = "0:00";
    }

    private void Update()
    {
        if (timersRunning)
        {
            DepartureTime.text = DepartureTimeText();
            ArrivalTime.text = ArrivalTimeText();
        }
    }

    private string DepartureTimeText()
    {
        if (curTrack == null)
            return "";

        string time = curTrack.CurDepartureTime().ToString().Replace('.',':');

        if (curTrack.CurDepartureTime() >= 10.00f)
        {
            return time.Length > 5 ? time.Substring(0, 5) : time;                    
        }
        else
        {
            return time.Length > 4 ? time.Substring(0, 4) : time;
        }
    }    

    private string ArrivalTimeText()
    {
        if (curTrack == null)
            return "";

        string time = curTrack.CurArrivalTime().ToString().Replace('.', ':');
        //print(time);

        if (curTrack.CurArrivalTime() >= 10.00f)
        {
            return time.Length > 5 ? time.Substring(0, 5) : time;
        }
        else
        {
            return time.Length > 4 ? time.Substring(0, 4) : time;
        }
    }

    private void GetClickedTrackInfo(PassengerTrack curTrack)
    {
        this.curTrack = curTrack;
        TrainName.text = curTrack.TrainName;
        OpenTrackMenu();
    }

    private void OpenTrackMenu()
    {
        PTrackMenu.SetActive(true);
    }
}
