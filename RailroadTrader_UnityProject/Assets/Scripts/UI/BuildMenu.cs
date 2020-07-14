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
        public Button button;
        public int cost;
    }

    [SerializeField]
    private ObjectPlacement _placement;
    [SerializeField]
    private FinanceController FC;
    public List<BuildOption> TrackBuildOptions;
    public List<BuildOption> ShopBuildOptions;

    void Start()
    {
        foreach (BuildOption option in TrackBuildOptions)
        {
            option.button.onClick.AddListener(delegate { StartBuildMode(option.prefab, option.cost); });
        }

        foreach (BuildOption option in ShopBuildOptions)
        {
            option.button.onClick.AddListener(delegate { StartBuildMode(option.prefab, option.cost); });
        }
    }



    protected virtual void StartBuildMode(GameObject pPrefab, int pCost)
    {
        if (FC.SubtractCurrency(pCost))
        {
            _placement.ActivateBuildmode(pPrefab);
        }    
        else
        {
            print("not enough moneyz");
        }
    }
}
