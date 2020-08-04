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
    public static event System.Action OnBuildModeEnded = delegate { };

    void Awake()
    {
        DeactivateBuildmode();
    }

    void Update()
    {
        if (BuildModeActivated)
            DetectMouse();
    }

    protected virtual void DetectMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GridManager.Instance.m_HoveredGridTile != null && !GridManager.Instance.m_HoveredGridTile.IsTileOccupied() && !EventSystem.current.IsPointerOverGameObject())
            {
                List<GridTile> neighbors = GridManager.Instance.Neighbors(GridManager.Instance.m_HoveredGridTile, true, _blockedTiles);
                HighlightNeighbours(neighbors, Color.green);

                if (neighbors.Count < _blockedTiles.Count - 1)
                {
                    HighlightNeighbours(neighbors, Color.red);
                    return;
                }

                foreach (GridTile tile in neighbors)
                {
                    if (tile.IsTileOccupied())
                    {
                        HighlightNeighbours(neighbors, Color.red);
                        return;
                    }
                    if (tile.gameObject.GetComponent<BuildSpace>().m_StoreType != m_ObjectPrefab.GetComponent<Building>().m_StoreType)
                    {
                        HighlightNeighbours(neighbors, Color.red);
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

    private void HighlightNeighbours(List<GridTile> neighbors, Color color)
    {
        foreach(GridTile tile in neighbors)
        {
            tile.GetComponentInChildren<SpriteRenderer>().color = color;
        }
    }

    public virtual void ActivateBuildmode(GameObject pPrefab, int pCost) 
    {
        GameManager.Instance.BuildModeActive = BuildModeActivated = true;
        _blockedTiles = m_ObjectPrefab.GetComponent<Building>().BlockedTiles();
        m_ObjectPrefab = pPrefab;
        tempBuildCost = pCost;
        //Cursor.visible = false;
        m_HighlightCursor.SetActive(true);
        _modelHolder = Instantiate(m_ObjectPrefab.GetComponent<Building>().m_Model, transform);

        //for (int i = 0; i <= m_ObjectPrefab.GetComponent<Building>().BlockedXTiles; i++)
        //{
        //    for (int j = 0; j < m_ObjectPrefab.GetComponent<Building>().BlockedZTiles; j++)
        //    {
        //        _blockedTiles.Add(new Vector2Int(i, j));
        //    }
        //}
        //_modelHolder.GetComponent<MeshRenderer>().material = m_HighlightMat;
    }

    protected virtual void DeactivateBuildmode()
    {
        Destroy(_modelHolder);
        _blockedTiles.Clear();        
        m_HighlightCursor.SetActive(false);
        //Cursor.visible = true;
        OnBuildModeEnded();
        GameManager.Instance.BuildModeActive = BuildModeActivated = false;
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
