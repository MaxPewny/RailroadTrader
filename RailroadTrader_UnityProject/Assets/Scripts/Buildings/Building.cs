using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
public class Building : MonoBehaviour
{
    public BuildingType m_Type; 

    public GameObject m_Model;
    public bool m_IsActive { get; protected set; }

    public float m_BuildCost;

    public Vector3 m_NpcMovePointOffset;
    public Vector3 m_NpcMovePoint { get; protected set; }
    public int m_NpcAmount;

    public List<Vector2Int> m_BlockedTilesXZ;

    protected GridObject _gridObject;

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _gridObject = GetComponent<GridObject>();
        m_NpcMovePoint = transform.position + m_NpcMovePointOffset;
        BuildingManager.Instance.m_Buildings.Add(this);
 
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
