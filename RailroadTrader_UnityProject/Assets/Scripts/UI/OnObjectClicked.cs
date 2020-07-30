using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Opens a menu when the right object in scene was clicked e.g. track or shop menus
/// </summary>
public class OnObjectClicked : MonoBehaviour
{
	public static event System.Action OnCargoTrackClicked = delegate { };
	public static event System.Action<SupplyStores> OnShopClicked = delegate { };
	public static event System.Action<PassengerTrack> OnPassengerTrackClicked = delegate { };

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{         
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				Building clickedBuilding = hit.collider.GetComponentInParent<Building>();
				if (clickedBuilding == null)
					return;

                print("clicked on object " + clickedBuilding.gameObject.name);

                if (clickedBuilding.m_StoreType == StoreType.SUPPLYSTORE)
                {
                    OnShopClicked(hit.collider.GetComponentInParent<SupplyStores>());
                    return;
                }
                
                switch (clickedBuilding.m_Type)
                {                   
                    case BuildingType.BOOKSTORE:
                        return;
                    case BuildingType.FLOWERSTORE:
                        return;
                    case BuildingType.TRAVELINFO:
                        return;
                    case BuildingType.TRAVELAGENCY:
                        return;
                    case BuildingType.HAIRSALON:
                        return;
                    case BuildingType.BANK:
                        return;
                    case BuildingType.PASSENGERTRAIN:
                        OnPassengerTrackClicked(hit.collider.GetComponentInParent<PassengerTrack>());
                        return;
                    case BuildingType.CARGOTRAIN:
                        OnCargoTrackClicked();
                        return;
                    default:
                        Debug.LogError("building type not found: " + clickedBuilding.m_Type.ToString());
                        return;
                }
            }
        }
    }
}