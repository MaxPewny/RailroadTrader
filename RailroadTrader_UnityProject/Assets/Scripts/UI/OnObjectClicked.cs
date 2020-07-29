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

                switch (clickedBuilding.m_StoreType)
                {
                    case StoreType.PLATFORM:
                        OnCargoTrackClicked();
                        break;
                    case StoreType.SUPPLYSTORE:
                        OnShopClicked(hit.collider.GetComponentInParent<SupplyStores>());
                        break;
                    case StoreType.DINGSSTORE:
                        OnPassengerTrackClicked(hit.collider.GetComponentInParent<PassengerTrack>());
                        break;
                    default:
                        break;
                }
            }
		}
	}
}