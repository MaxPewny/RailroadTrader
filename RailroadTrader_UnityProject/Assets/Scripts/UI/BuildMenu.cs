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

    public BuildOptionValues[] TrackBuildOptions;
    public BuildOptionValues[] ShopBuildOptions;

    public static event System.Action OnBuildModeActivated = delegate { };

    void Start()
    {
        for (int i = 0; i < TrackBuildOptions.Length; i++)
        {
            BuildOptionValues option = TrackBuildOptions[i];
            option.Button.onClick.AddListener(delegate { StartBuildMode(option.Prefab, option.Values.BuildCost); });
            option.ImageUI.sprite = option.Preview;
        }
        for (int i = 0; i < ShopBuildOptions.Length; i++)
        {
            BuildOptionValues option = ShopBuildOptions[i];
            option.Button.onClick.AddListener(delegate { StartBuildMode(option.Prefab, option.Values.BuildCost, option.Values.StoreType); });
            option.ImageUI.sprite = option.Preview;
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

        //check if building can be paid for, but do not pay yet
        if (FC.PayBuildingCost(pCost))
        {
            _placement.ActivateBuildmode(pPrefab, pCost);
            OnBuildModeActivated();
        }
        else
        {
            print("not enough moneyz");
        }
    }
}
