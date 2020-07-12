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
                foreach (GridTile tile in GridManager.Instance.Neighbors(GridManager.Instance.m_HoveredGridTile, true, m_ObjectPrefab.GetComponent<Building>().m_BlockedTilesXZ))
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
        m_ObjectPrefab = pPrefab;
        _buildModeActivated = true;
        m_HighlightCursor.SetActive(false);
        _modelHolder = Instantiate(m_ObjectPrefab.GetComponent<Building>().m_Model, transform);
        //_modelHolder.GetComponent<MeshRenderer>().material = m_HighlightMat;
    }

    protected virtual void DeactivateBuildmode()
    {
        Destroy(_modelHolder);
        _buildModeActivated = false;
        m_HighlightCursor.SetActive(true);
        
    }

    protected virtual void PlaceObject() 
    {
        GameObject instance = Instantiate(m_ObjectPrefab, GridManager.Instance.m_HoveredGridTile.transform.position, m_ObjectPrefab.transform.rotation, m_ObjectParent.transform);
        instance.GetComponent<GridObject>().m_GridPosition = GridManager.Instance.m_HoveredGridTile.m_GridPosition;
        DeactivateBuildmode();
    }
}
