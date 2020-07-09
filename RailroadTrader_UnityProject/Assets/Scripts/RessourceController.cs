using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourceController : MonoBehaviour
{
    public HashSet<CargoTrack> curCargoTracks { get; private set; }
    public Dictionary<Resource, int> _ressourcesInShops { get; private set; }
    public Dictionary<Resource, int> _ressourcesInTracks { get; private set; }
    public Dictionary<Passanger, int> _passangers { get; private set; }


    public Text debug_CurrencyDisplay;
    public float m_Currency { get; protected set;}


    void Awake()
    {
        InitiliazeDictionaries();
    }

    private void InitiliazeDictionaries()
    {
        _ressourcesInTracks = new Dictionary<Resource, int>();
        _ressourcesInShops = new Dictionary<Resource, int>();
        _passangers = new Dictionary<Passanger, int>();

        foreach (Resource ressource in (Resource[])Enum.GetValues(typeof(Resource)))
        {
            _ressourcesInShops.Add(ressource, 50);
        }
    }

    //Add method to refill all shops if they arent all full once new cargo arrives

    void Update()
    {
        debug_CurrencyDisplay.text = "€: " + m_Currency.ToString();
    }

    public void AddResourceToTrack(Resource pType, int pAmount) 
    {
        _ressourcesInShops[pType] += pAmount;
    }

    public void AddPassangersCount(Passanger pType, int pAmount)
    {
        _passangers[pType] += pAmount;       
    }

    public void AddCurrency(float pAmount) 
    {
        m_Currency += pAmount;
    }

    public int CurVisitors()
    {
        int amount = 0;
        foreach(KeyValuePair<Passanger, int> guest in _passangers)
        {
            amount += guest.Value;
        }
        return amount;
    }

    public int TakeRessourceFromCargo(Resource pType, int pAmount = 1)
    {
        if (_ressourcesInTracks[pType] >= pAmount)
        {
            _ressourcesInTracks[pType] -= pAmount;
            return pAmount;
        }
        else if (_ressourcesInTracks[pType] < pAmount && _ressourcesInTracks[pType] > 0)
        {
            int amount = _ressourcesInTracks[pType];
            _ressourcesInTracks[pType] = 0;
            return amount;
        }
        else
            return 0;
    }      

    public void SubtractFromShopRessis(Resource pType, int pAmount)
    {
        _ressourcesInShops[pType] -= pAmount;
    }
}
