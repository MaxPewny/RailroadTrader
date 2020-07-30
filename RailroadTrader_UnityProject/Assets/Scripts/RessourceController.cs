using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RessourceController : MonoBehaviour
{
    public Dictionary<Resource, int> _ressourcesInShops { get; private set; }
    public Dictionary<Resource, int> _ressourcesInTracks { get; private set; }
    public Dictionary<Passanger, int> _passangers { get; private set; }

    public static object mutex = new object();

    public static event System.Action OnRefillStores = delegate { };
    public static event System.Action<Resource, int> OnShopStockChange = delegate { };

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
            _ressourcesInTracks.Add(ressource, 0);
        }

        foreach (Passanger passanger in (Passanger[])Enum.GetValues(typeof(Passanger)))
        {
            _passangers.Add(passanger, 0);
        }

        foreach (Resource ressource in (Resource[])Enum.GetValues(typeof(Resource)))
        {
            _ressourcesInShops.Add(ressource, 0);
        }
    }


    void Start()
    {
        CargoTrainController.OnCargoReset += ResetCargoInTracks;
        CargoTrainController.OnOrderArrived += SetCargoInTracksTo;
    }

    private void ResetCargoInTracks()
    {
        lock (mutex)
        {
            _ressourcesInTracks.Clear();
            foreach (Resource ressource in (Resource[])Enum.GetValues(typeof(Resource)))
            {
                _ressourcesInTracks.Add(ressource, 0);
            }
        }
    }

    private void SetCargoInTracksTo(Dictionary<Resource, int> newAmount)
    {
        lock (mutex)
        {
            foreach(KeyValuePair<Resource,int> pair in newAmount)
            {
                _ressourcesInTracks[pair.Key] = pair.Value;
            }
            OnRefillStores();
        }
    }

    public void AddPassangersCount(Passanger pType, int pAmount)
    {
        _passangers[pType] += pAmount;       
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

    /// <summary>
    /// Returns pAmount if _ressourcesInTracks count is equal or higher, else returns the available amount
    /// </summary>
    /// <param name="pType"></param>
    /// <param name="pAmount"></param>
    /// <returns></returns>
    public int TakeRessourceFromCargo(Resource pType, int pAmount = 1)
    {
        lock (mutex)
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
    }

    public void SetResourceToTrack(Resource pType, int pAmount)
    {
        _ressourcesInTracks[pType] = pAmount;
    }

    public void SubtractFromShopRessis(Resource pType, int pAmount)
    {
        lock (mutex)
        {
            _ressourcesInShops[pType] -= pAmount;
            OnShopStockChange(pType, _ressourcesInShops[pType]);
        }
    }

    public void AddToShopRessis(Resource pType, int pAmount)
    {
        lock (mutex)
        {
            _ressourcesInShops[pType] += pAmount;
            OnShopStockChange(pType, _ressourcesInShops[pType]);
        }
    }
}
