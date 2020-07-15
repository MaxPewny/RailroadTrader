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
    public Transform NPCEnterPoint;
    public Vector3 m_NpcMovePointOffset { get { return new Vector3(NPCEnterPoint.position.x, NPCEnterPoint.position.y, NPCEnterPoint.position.z); } }
    public int m_NpcAmount;
    public List<Vector2Int> m_BlockedTilesXZ;
    public int BlockedXTiles;
    public int BlockedZTiles;
    public Vector3 m_NpcMovePoint { get; protected set; }
    public bool m_IsActive { get; protected set; }
    protected GridObject _gridObject;

    private void Awake()
    {
        WriteBlockedVectors(BlockedXTiles, BlockedZTiles);
    }

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

        SetTileStatus();
    }


    protected virtual void WriteBlockedVectors(int x, int z)
    {
        for (int i = 0; i <= x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                m_BlockedTilesXZ.Add(new Vector2Int(i, j));
            }
        }
    }

    protected virtual void SetTileStatus(bool blocked = true)
    {
        if (blocked)
        {
            foreach (GridTile tile in GridManager.Instance.Neighbors(_gridObject.m_CurrentGridTile, true, m_BlockedTilesXZ))
            {
                tile.AddOccupyingGridObject(_gridObject);
            }
        }
        else
        {
            foreach (GridTile tile in GridManager.Instance.Neighbors(_gridObject.m_CurrentGridTile, true, m_BlockedTilesXZ))
            {
                tile.RemoveOccupyingGridObject(_gridObject);
            }
        }
    }

    protected virtual void DestroyBuilding()
    {
        SetTileStatus(false);
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
