using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCellClass : MonoBehaviour
{
    public enum CellState
    {
        Brack,
        White,
    }

    public CellState Status { get; set; }
    public void Chenge(int x, int y, NewCellClass[,] target, CellState state)
    {
        target[x, y].gameObject.transform.Rotate(0, 180, 0, Space.World);
        target[x, y].Status = state;
    }
    public void SetCell(CellState state, GameObject cell)
    {
        if (state == CellState.Brack) cell.transform.Rotate(0, 180, 0, Space.World);
    }
}
