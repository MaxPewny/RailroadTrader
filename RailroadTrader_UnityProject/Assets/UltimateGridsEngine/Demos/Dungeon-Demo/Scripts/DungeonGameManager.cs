﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGameManager : MonoBehaviour
{   
    public GridObject m_PlayerGridObject;
    public GridTile m_RespawnTile;

    public void RespawnPlayer() {
        
        if (m_PlayerGridObject != null && m_RespawnTile != null) {
            var movementcomponent = m_PlayerGridObject.GetComponent<GridMovement>();
            if (movementcomponent != null) {
                movementcomponent.MoveTo(m_RespawnTile, false, false);
                movementcomponent.Rotate(m_PlayerGridObject.m_InitialFacingDirection);
            }
        }

    }
}
