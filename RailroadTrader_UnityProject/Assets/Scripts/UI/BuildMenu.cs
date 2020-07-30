using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildMenu : MonoBehaviour
{
    [SerializeField]
    private ObjectPlacement _placement;
    [SerializeField]
    private FinanceController FC;
    [SerializeField]
    private BuildingManager BM;

    public List<BuildButton> TrackBuildOptions;
    public List<BuildButton> ShopBuildOptions;

    void Start()
    {
        foreach (BuildButton option in TrackBuildOptions)
        {
            option.button.onClick.AddListener(delegate { StartBuildMode(option.values.prefab, option.values.trackValues.BuildCost); });
            option.image.sprite = option.values.picture;
        }

        foreach (BuildButton option in ShopBuildOptions)
        {
            option.button.onClick.AddListener(delegate { StartBuildMode(option.values.prefab, option.values.storeValues.BuildCost, option.values.type); });
            option.image.sprite = option.values.picture;
        }
    }

    protected virtual void StartBuildMode(GameObject pPrefab, int pCost, StoreType type = StoreType.PLATFORM)
    {
        if (_placement.BuildModeActivated)
            return;

        if (type == StoreType.SUPPLYSTORE && !BM.CanBuildMoreStores())
        {
            print("cannot build more stores");
            return;
        }
        else if (type == StoreType.PLATFORM && !BM.CanBuildMoreTracks())
        {
            print("cannot build more tracks");
            return;
        }

        if (FC.PayBuildingCost(pCost))
        {
            _placement.ActivateBuildmode(pPrefab);
        }    
        else
        {
            print("not enough moneyz");
        }
    }
}
