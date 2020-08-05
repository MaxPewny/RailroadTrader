using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject IngameUI;
    public GameObject StartMenuUI;
    public GameObject PauseMenu;

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
        OnObjectClicked.OnPassengerTrackClicked += CloseMenusExceptPTracks;
        //OnObjectClicked.OnShopClicked

        BuildMenu.OnBuildModeActivated += CloseAllMenus;

        StartMenu.OnShowStartMenu += ShowStartMenu;
        StartMenu.OnShowIngameUI += ShowIngameUI;
        TimeController.OnGamePaused += ShowPauseMenu;
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
    private void CloseMenusExceptPTracks(PassengerTrack track)
    {
        StationManagementMenu.SetActive(false);
        ShopInfoMenu.SetActive(false);
        CargoOrderMenu.SetActive(false);
        ShopBuildMenu.SetActive(false);
        TrackBuildMenu.SetActive(false);
    }

    private void ShowStartMenu(bool show)
    {
        StartMenuUI.SetActive(show);
    }

    private void ShowIngameUI(bool show)
    {
        IngameUI.SetActive(show);
    }
    private void ShowPauseMenu(bool show)
    {
        if (show)
            CloseAllMenus();

        PauseMenu.SetActive(show);
    }
}
