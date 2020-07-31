using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlaceMarkers : MonoBehaviour
{
    public GameObject ShopMarkers;
    public GameObject TrackMarkers;

    private void Start()
    {
        ObjectPlacement.OnBuildModeEnded += DeaktivateMarkers;
        OnObjectClicked.OnAnyTileClicked += DeaktivateMarkers;
        OnObjectClicked.OnCargoTrackClicked += DeaktivateMarkers;
    }

    private void DeaktivateMarkers()
    {
        SetTrackMakerVisibility(false);
        SetShopMakerVisibility(false);
    }

    public void SetTrackMakerVisibility(bool visible)
    {
        TrackMarkers.SetActive(visible);
    }

    public void SetShopMakerVisibility(bool visible)
    {
        ShopMarkers.SetActive(visible);
    }
}
