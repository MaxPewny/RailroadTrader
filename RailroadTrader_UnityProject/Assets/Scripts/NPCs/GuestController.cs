using System.Collections;
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
    private List<PassengerStats> pStats = new List<PassengerStats>();

    private void Awake()
    {
        pStats.Add(new PassengerStats(Passanger.COMMUTER));
        pStats.Add(new PassengerStats(Passanger.TOURIST));
        pStats.Add(new PassengerStats(Passanger.BUSINESS));
    }

    private void Start()
    {
        NM.OnVisitorSpawn += UpdateVisitorAmount;
        NpcActor.OnLeftStation += UpdateCurVisitorCount;
        FinanceController.OnPassangerSpendMoney += UpdateVisitorSpending;
    }

    private void UpdateVisitorSpending(Passanger pType, int moneySpend)
    {
        Stats(pType).totalSpendings += moneySpend;
    }

    private void UpdateVisitorAmount(Passanger pType, int newVisitorCount)
    {
        PassengerStats stat = Stats(pType);
        stat.curAmount += newVisitorCount;
        stat.totalAmount += newVisitorCount;
    }

    private void UpdateCurVisitorCount(Passanger pType)
    {
        --Stats(pType).curAmount;
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
        return pStats.SingleOrDefault(p => p.passenger == pType);
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
