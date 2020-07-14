using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuildMenu : MonoBehaviour
{
    [System.Serializable]
    public struct m_BuildOption
    {
        public GameObject prefab;
        public Button button;
        public int cost;
    }

    [SerializeField]
    private ObjectPlacement _placement;
    private FinanceController FC;
    public List<m_BuildOption> m_BuildOptions;

    void Start()
    {
        FC = FindObjectOfType<FinanceController>();

        foreach (m_BuildOption option in m_BuildOptions)
        {
            option.button.onClick.AddListener(delegate { StartBuildMode(option.prefab, option.cost); });
        }
    }

    void Update()
    {

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
