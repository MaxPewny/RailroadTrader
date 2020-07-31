using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject StationManagementMenu;
    public GameObject CargoOrderMenu;
    public GameObject ShopInfoMenu;
    public GameObject PassengerTrackInfoMenu;
    public GameObject ShopBuildMenu;
    public GameObject TrackBuildMenu;

    private void Start()
    {
        OnObjectClicked.OnAnyTileClicked += CloseAllMenus;
        OnObjectClicked.OnCargoTrackClicked += CloseMenusExceptCargo;
        //OnObjectClicked.OnPassengerTrackClicked
        //OnObjectClicked.OnShopClicked
    }

    private void CloseAllMenus()
    {
        StationManagementMenu.SetActive(false);
        CargoOrderMenu.SetActive(false);
        ShopInfoMenu.SetActive(false);
        PassengerTrackInfoMenu.SetActive(false);
        ShopBuildMenu.SetActive(false);
        TrackBuildMenu.SetActive(false);
    }

    private void CloseMenusExceptCargo()
    {
        StationManagementMenu.SetActive(false);
        ShopInfoMenu.SetActive(false);
        PassengerTrackInfoMenu.SetActive(false);
        ShopBuildMenu.SetActive(false);
        TrackBuildMenu.SetActive(false);
    }

}
