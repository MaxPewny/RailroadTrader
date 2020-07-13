using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [Tooltip("How many minutes in real time is 1 ingame hour")]
    public float realTimeMinPerHour = 2.0f;
    [Tooltip("paused, normal, fast, super fast")]
    public float[] ingameSpeed = new float[4];
       
    
    private int curHour;
    private int curDay;
    private int curMonth;
    private int curYear;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
