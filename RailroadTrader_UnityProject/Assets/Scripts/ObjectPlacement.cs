using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [ReadOnly, SerializeField]
    private bool _buildModeActivated = false;

    private List<Vector2Int> _blockedTiles = new List<Vector2Int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectMouse();
    }

    protected virtual void SetBuildAreas()
    {

    }

    protected virtual void DetectMouse()
    {
        if (Input.GetMouseButtonDown(0) && _buildModeActivated)
        {
            if (GridManager.Instance.m_HoveredGridTile != null && !GridManager.Instance.m_HoveredGridTile.IsTileOccupied())
            {
                foreach (GridTile tile in GridManager.Instance.Neighbors(GridManager.Instance.m_HoveredGridTile, true, _blockedTiles))
                {
                    if (tile.IsTileOccupied()) 
                    {
                        return;
                    }
                }
                PlaceObject();
            }
        }
    }

    public virtual void ActivateBuildmode(GameObject pPrefab) 
    {
        if(_buildModeActivated)
        {
            DeactivateBuildmode();
        }

        m_ObjectPrefab = pPrefab;
        _buildModeActivated = true;
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
        _buildModeActivated = false;
        m_HighlightCursor.SetActive(true);
        _blockedTiles.Clear();
        
    }

    protected virtual void PlaceObject() 
    {
        GameObject instance = Instantiate(m_ObjectPrefab, GridManager.Instance.m_HoveredGridTile.transform.position, m_ObjectPrefab.transform.rotation, m_ObjectParent.transform);
        instance.GetComponent<GridObject>().m_GridPosition = GridManager.Instance.m_HoveredGridTile.m_GridPosition;
        DeactivateBuildmode();
    }
}
