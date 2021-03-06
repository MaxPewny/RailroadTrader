﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GuestController : MonoBehaviour
{
    private VisitorStats[] guestCount;
    [SerializeField]
    private BuildingManager BM;
    [SerializeField]
    private NpcMovement NM;
    [SerializeField]
    private PassengerStats[] pStats = new PassengerStats[3];

    public static event System.Action<int> OnCurVisiterCountChange = delegate { };
    public static event System.Action<int> OnNewVisitorsArrived = delegate { };
    public static event System.Action OnSpendingChange = delegate { };
    public static event System.Action OnVisitorLeftEntered = delegate { };

    private void Awake()
    {
        pStats[(int)Passanger.COMMUTER] =new PassengerStats(Passanger.COMMUTER);
        pStats[(int)Passanger.TOURIST] =new PassengerStats(Passanger.TOURIST);
        pStats[(int)Passanger.BUSINESS] =new PassengerStats(Passanger.BUSINESS);
    }

    private void Start()
    {
        NM.OnVisitorSpawn += UpdateVisitorAmount;
        NpcActor.OnLeftStation += SubtractLeavingVisitorFromCount;
        FinanceController.OnPassangerSpendMoney += UpdateVisitorSpending;
    }

    private void UpdateVisitorSpending(Passanger pType, int moneySpend)
    {
        pStats[(int)pType].totalSpendings += moneySpend;
        OnSpendingChange();
        //print(pStats[(int)pType].passenger.ToString() + "s spend " + pStats[(int)pType].totalSpendings.ToString() + " € in shops");
    }

    private void UpdateVisitorAmount(Passanger pType, int newVisitorCount)
    {
        pStats[(int)pType].curAmount += newVisitorCount;
        pStats[(int)pType].totalAmount += newVisitorCount;

        OnNewVisitorsArrived(newVisitorCount);
        OnCurVisiterCountChange(newVisitorCount);
        OnVisitorLeftEntered();
    }

    private void SubtractLeavingVisitorFromCount(Passanger pType)
    {
        --pStats[(int)pType].curAmount;
        OnCurVisiterCountChange(CurGuestsInStation());
        OnVisitorLeftEntered();
    }

    //private int CurGuestsOf(Passanger pType)
    //{
    //    int count = 0;
    //    foreach (Building b in BM.m_Buildings)
    //    {
    //        count += b.CurGuestCountOf(pType);
    //    }
    //    return count;
    //}

    public PassengerStats Stats(Passanger pType)
    {
        return pStats[(int)pType];
        //return pStats.SingleOrDefault(p => p.passenger == pType);
    }

    public int CurGuestsIn(StoreType sType)
    {
        int count = 0;
        foreach (Building b in BM.m_Buildings)
        {
            if (b.m_StoreType == sType)
                count += b.CurTotalGuests();
        }
        return count;
    }

    public int CurGuestsInStation()
    {
        int count = 0;
        foreach(PassengerStats pstat in pStats)
        {
            count += pstat.curAmount; 
        }
        return count;
    }
}
