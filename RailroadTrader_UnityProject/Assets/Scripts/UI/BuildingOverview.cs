using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingOverview : MonoBehaviour
{
    [SerializeField]
    private Text BuildingCount;

    public void WriteTotalBuildingCount(int amount)
    {
        BuildingCount.text = amount.ToString();
    }
}
