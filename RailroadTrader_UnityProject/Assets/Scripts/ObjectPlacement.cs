using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPlacement : MonoBehaviour
{
    [Header("Parent")]
    public GameObject m_ObjectParent;
    [Header("Prefab")]
    public GameObject m_ObjectPrefab;
    private GameObject _modelHolder;
    [Header("Cursor")]
    public GameObject m_HighlightCursor;
    public Material m_HighlightMat;

    private int tempBuildCost;

    [SerializeField]
    public bool BuildModeActivated { get; protected set; }
    private List<Vector2Int> _blockedTiles = new List<Vector2Int>();

    public static event System.Action<int> OnBuildingCanceled = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BuildModeActivated)
            DetectMouse();
    }

    protected virtual void SetBuildAreas()
    {

    }

    protected virtual void DetectMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GridManager.Instance.m_HoveredGridTile != null && !GridManager.Instance.m_HoveredGridTile.IsTileOccupied() && !EventSystem.current.IsPointerOverGameObject())
            {
                List<GridTile> neighbors = GridManager.Instance.Neighbors(GridManager.Instance.m_HoveredGridTile, true, _blockedTiles);

                if (neighbors.Count < _blockedTiles.Count - 1) return;

                foreach (GridTile tile in neighbors)
                {
                    if (tile.IsTileOccupied())
                    {
                        return;
                    }
                    if (tile.gameObject.GetComponent<BuildSpace>().m_StoreType != m_ObjectPrefab.GetComponent<Building>().m_StoreType) 
                    {
                        return;
                    }
                }
                PlaceObject();
            }
        }
        else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            AbortBuildMode();
        }
    }

    public virtual void ActivateBuildmode(GameObject pPrefab, int pCost) 
    {
        BuildModeActivated = true;
        m_ObjectPrefab = pPrefab;
        tempBuildCost = pCost;
        m_HighlightCursor.SetActive(false);
        _modelHolder = Instantiate(m_ObjectPrefab.GetComponent<Building>().m_Model, transform);

        for (int i = 0; i <= m_ObjectPrefab.GetComponent<Building>().BlockedXTiles; i++)
        {
            for (int j = 0; j < m_ObjectPrefab.GetComponent<Building>().BlockedZTiles; j++)
            {
                _blockedTiles.Add(new Vector2Int(i, j));
            }
        }
        //_modelHolder.GetComponent<MeshRenderer>().material = m_HighlightMat;
    }

    protected virtual void DeactivateBuildmode()
    {
        Destroy(_modelHolder);
        _blockedTiles.Clear();        
        m_HighlightCursor.SetActive(true);
        BuildModeActivated = false;
    }

    protected virtual void AbortBuildMode()
    {
        OnBuildingCanceled(tempBuildCost);
        DeactivateBuildmode();
    }

    protected virtual void PlaceObject() 
    {
        GameObject instance = Instantiate(m_ObjectPrefab, GridManager.Instance.m_HoveredGridTile.transform.position, m_ObjectPrefab.transform.rotation, m_ObjectParent.transform);
        instance.GetComponent<GridObject>().m_GridPosition = GridManager.Instance.m_HoveredGridTile.m_GridPosition;
        DeactivateBuildmode();
    }
}
