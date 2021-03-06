﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
public class Building : MonoBehaviour
{
    [SerializeField]
    private List<Vector2Int> m_BlockedTilesXZ;

    protected FinanceController FC;
    protected RessourceController RC;

    public StoreType m_StoreType;
    public BuildingType m_Type;
    public GameObject m_Model;
    //public int m_BuildCost;
    public int UpkeepCost = 25;
    public Transform NPCEnterPoint;
    public Vector3 m_NpcMovePointOffset { get; protected set; }
    public int m_NpcAmount;
    public int totalGuestCount;
    public int BlockedXTiles;
    public int BlockedZTiles;
    //public int curSatisfation { get; protected set; }
    public Vector3 m_NpcMovePoint { get; protected set; }
    public bool m_IsActive { get; protected set; }
    protected GridObject _gridObject;

    public static event Action<Building> OnInitialize = delegate { };

    private void Awake()
    {
        //WriteBlockedVectors(BlockedXTiles, BlockedZTiles);
        WriteGDValues();
    }

    protected virtual void Start()
    {
        Initialize();
    }

    protected virtual void Update()
    { }

    protected virtual void WriteGDValues()
    {
        print("writing from scriptable objects");
    }

    protected virtual void Initialize()
    {
        _gridObject = GetComponent<GridObject>();
        m_NpcMovePoint = NPCEnterPoint.position;
        FC = FindObjectOfType<FinanceController>();
        RC = FindObjectOfType<RessourceController>();
        OnInitialize(this);
        m_BlockedTilesXZ = BlockedTiles();
        SetTileStatus();
    }

    public virtual List<Vector2Int> BlockedTiles()
    {
        List<Vector2Int> blockedTiles = new List<Vector2Int>();

        for (int x = 0; x < BlockedXTiles; x++)
        {
            for(int z = 0; z < BlockedZTiles; z++)
            {
                blockedTiles.Add(new Vector2Int(x, z));
            }
        }
        return blockedTiles;
    }

    //protected virtual void WriteBlockedVectors(int x, int z)
    //{
    //    for (int i = 0; i <= x; i++)
    //    {
    //        for (int j = 0; j < z; j++)
    //        {
    //            m_BlockedTilesXZ.Add(new Vector2Int(i, j));
    //        }
    //    }
    //}

    protected virtual void SetTileStatus(bool blocked = true)
    {
        if (blocked)
        {
            foreach (GridTile tile in GridManager.Instance.Neighbors(_gridObject.m_CurrentGridTile, true, BlockedTiles()))
            {
                tile.AddOccupyingGridObject(_gridObject);
            }
        }
        else
        {
            foreach (GridTile tile in GridManager.Instance.Neighbors(_gridObject.m_CurrentGridTile, true, BlockedTiles()))
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

    public virtual int CurGuestCountOf(Passanger pType)
    {
        return 0;
    }

    public virtual int CurTotalGuests()
    {
        return 0;
    }
}
