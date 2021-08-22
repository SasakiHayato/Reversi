using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiversiManager : MonoBehaviour
{
    const int m_wide = 8;
    const int m_height = 8;

    [SerializeField] PlayerClass m_player;

    [SerializeField] FieldCellClass m_fieldCell;
    FieldCellClass[,] m_fieldCells = new FieldCellClass[m_wide, m_height];

    [SerializeField] CellClass m_cell;
    CellClass[,] m_cells = new CellClass[m_wide, m_height];

    void Start()
    {
        m_player.SetWide(m_wide);
        m_player.SetHeight(m_height);
        CreateField();
    }

    void Update()
    {
        TargetField(m_player.SetX(), m_player.SetY());
    }

    void CreateField()
    {
        for (int x = 0; x < m_wide; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                Vector2 setVec = new Vector2(x - m_wide / 2, y - m_height / 2);
                GameObject cell = Instantiate(m_fieldCell.gameObject, setVec, Quaternion.identity);
                cell.name = $"Cell :{x} :{y}";
                m_fieldCells[x, y] = cell.GetComponent<FieldCellClass>();
                m_fieldCells[x, y].SetStatus(FieldStatus.Close);

                if (x == 3 && y == 3 || x == 4 && y == 4 || x == 3 && y == 4 || x == 4 && y == 3)
                {
                    StartSetCell(x, y, setVec);
                }
            }
        }
    }

    void StartSetCell(int x, int y, Vector2 setVec)
    {
        GameObject cell = Instantiate(m_cell.gameObject, setVec, Quaternion.identity);
        m_cells[x, y] = cell.GetComponent<CellClass>();
        if (x == 3 && y == 3) { m_cells[x, y].SetCellStatus(CellStatus.White); }
        if (x == 4 && y == 4) { m_cells[x, y].SetCellStatus(CellStatus.White); }
        if (x == 3 && y == 4) { m_cells[x, y].SetCellStatus(CellStatus.Brack); }
        if (x == 4 && y == 3) { m_cells[x, y].SetCellStatus(CellStatus.Brack); }
    }

    void TargetField(int selectX, int selectY)
    {
        for (int x = 0; x < m_wide; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                Material target = m_fieldCells[x, y].TargetFieldColor();
                Material others = m_fieldCells[x, y].OthersFieldColor();

                Renderer renderer = m_fieldCells[x, y].GetComponent<Renderer>();
                renderer.material = (selectX == x && selectY == y ? target : others);
            }
        }
    }
}
