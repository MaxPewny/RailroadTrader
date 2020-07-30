using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildMenu : MonoBehaviour
{
    [System.Serializable]
    public struct BuildOption 
    {
        public GameObject prefab;
        public Sprite picture;
        public Button button;
        public Image image;
        public int cost;
        public StoreType type;
    }

    [SerializeField]
    private ObjectPlacement _placement;
    [SerializeField]
    private FinanceController FC;
    [SerializeField]
    private BuildingManager BM;
    public List<BuildOption> TrackBuildOptions;
    public List<BuildOption> ShopBuildOptions;

    void Start()
    {
        foreach (BuildOption option in TrackBuildOptions)
        {
            option.button.onClick.AddListener(delegate { StartBuildMode(option.prefab, option.cost); });
            option.image.sprite = option.picture;
        }

        foreach (BuildOption option in ShopBuildOptions)
        {
            option.button.onClick.AddListener(delegate { StartBuildMode(option.prefab, option.cost, option.type); });
            option.image.sprite = option.picture;
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
