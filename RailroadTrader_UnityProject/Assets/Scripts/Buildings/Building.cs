using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
public class Building : MonoBehaviour
{
    protected BuildingManager BM;
    protected FinanceController FC;
    protected RessourceController RC;

    public BuildingType m_Type; 
    public GameObject m_Model;
    public float m_BuildCost;
    public Vector3 m_NpcMovePointOffset;
    public int m_NpcAmount;
    public List<Vector2Int> m_BlockedTilesXZ;
    public Vector3 m_NpcMovePoint { get; protected set; }
    public bool m_IsActive { get; protected set; }
    protected GridObject _gridObject;

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _gridObject = GetComponent<GridObject>();
        m_NpcMovePoint = transform.position + m_NpcMovePointOffset;
        FC = FindObjectOfType<FinanceController>();
        BM = FindObjectOfType<BuildingManager>();
        RC = FindObjectOfType<RessourceController>();
        BM.m_Buildings.Add(this);
 
        foreach (GridTile tile in GridManager.Instance.Neighbors(_gridObject.m_CurrentGridTile, true, m_BlockedTilesXZ))
        {
            tile.AddOccupyingGridObject(_gridObject);
        }
    }

    protected virtual void DestroyBuilding()
    {

    }

    public virtual void Activate()
    {
        m_IsActive = true;
    }

    public virtual void Deactivate()
    {
        m_IsActive = false;
    }
}
