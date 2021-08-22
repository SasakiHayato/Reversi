using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellStatus
{
    White,
    Brack,

    None,
}

public class CellClass : MonoBehaviour
{
    CellStatus m_status;

    public CellStatus SetCellStatus(CellStatus status) 
    {
        ChengeCell(status);
        return m_status = status;
    }

    void ChengeCell(CellStatus status)
    {
        if (status == CellStatus.Brack) { transform.Rotate(0, 180, 0, Space.World); }
        if (status == CellStatus.White) { transform.Rotate(0, 0, 0, Space.World); }
    }
}
