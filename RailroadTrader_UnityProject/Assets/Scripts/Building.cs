using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject m_Model;

    public bool m_IsActive { get; protected set; }

    public List<Vector2Int> m_BlockedTilesXZ;

    protected GridObject _gridObject;

    protected virtual void Initialize() 
    {
        _gridObject = GetComponent<GridObject>();

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
