using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject m_Model;

    public bool m_IsActive { get; protected set; }

    public Vector3 m_NpcMovePointOffset;
    public Vector3 m_NpcStartPointOffset;
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

        foreach (GridTile tile in GridManager.Instance.Neighbors(_gridObject.m_CurrentGridTile, true, m_BlockedTilesXZ))
        {
            tile.AddOccupyingGridObject(_gridObject);
        }

        SetNpcMovePoints();
    }

    protected virtual void DestroyBuilding()
    {

    }

    protected virtual void SetNpcMovePoints()
    {
        Debug.Log("SetTarget");
        NpcMovement.Instance.AddBuildingTargetPoint(transform.position + m_NpcMovePointOffset);
        //NpcMovement.Instance.AddSpawnPoint(transform.position + m_NpcStartPointOffset);
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
