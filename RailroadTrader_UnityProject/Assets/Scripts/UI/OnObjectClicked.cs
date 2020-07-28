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
	public static event System.Action<GameObject> OnShopClicked = delegate { };
	public static event System.Action<GameObject> OnPassengerTrackClicked = delegate { };

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				GameObject clickedObject = hit.collider.gameObject;

				if (clickedObject.GetComponentInChildren<CargoTrack>())
					OnCargoTrackClicked();
				else if (clickedObject.GetComponentInChildren<SupplyStores>())
					OnShopClicked(clickedObject);
				else if (clickedObject.GetComponentInChildren<PassengerTrack>())
					OnPassengerTrackClicked(clickedObject);
				else
					return;
			}
		}
	}
}