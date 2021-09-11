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
    public void Chenge(int x, int y, NewCellClass[,] target)
    {
        target[x, y].gameObject.transform.Rotate(0, 180, 0, Space.World);
    }
}
