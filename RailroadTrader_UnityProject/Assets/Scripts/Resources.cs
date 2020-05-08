using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
   // public Text debug_ResourceDisplay;
    public Text debug_CurrencyDisplay;


    private Dictionary<Resource, float> _resources = new Dictionary<Resource, float>();

    public float m_Currency { get; protected set;}


    // Singleton
    protected static Resources _instance = null;
    public static Resources Instance
    {
        get
        {
            if (_instance == null) { _instance = FindObjectOfType<Resources>(); }
            return _instance;
        }
        protected set { _instance = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Resource resource in (Resource[])Enum.GetValues(typeof(Resource)))
        {
            _resources.Add(resource, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        debug_CurrencyDisplay.text = "€: " + m_Currency.ToString();
    }

    public void AddResource(Resource pType, float pAmount) 
    {
        _resources[pType] += pAmount;
    }

    public void AddCurrency(float pAmount) 
    {
        m_Currency += pAmount;
    }
}
