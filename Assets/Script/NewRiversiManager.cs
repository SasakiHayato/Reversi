using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRiversiManager : MonoBehaviour
{
    [SerializeField] NewFieldCellClass m_field;
    NewFieldCellClass[,] m_fields = new NewFieldCellClass[8, 8];

    [SerializeField] NewCellClass m_cell;
    NewCellClass[,] m_cells = new NewCellClass[8, 8];

    NewCellClass.CellState m_nowCellState = NewCellClass.CellState.Brack;
    NewCellClass.CellState m_enemyCellState = NewCellClass.CellState.White;

    int m_selectX;
    int m_selectY;

    void Start()
    {
        CreateField();
        FirstSetCell();
    }

    void CreateField()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject field = Instantiate(m_field.gameObject, new Vector2(x - 8 / 2, y - 8 / 2), Quaternion.identity);
                field.name = $"X :{x} Y :{y}";
                m_fields[x, y] = field.GetComponent<NewFieldCellClass>();
                m_fields[x, y].Status = NewFieldCellClass.FieldState.None;
            }
        }
    }
    void FirstSetCell()
    {
        if (m_fields[3, 3])
        {
            GameObject cell = Instantiate(m_cell.gameObject, new Vector2(3 - 8 / 2, 3 - 8 / 2), Quaternion.identity);
            cell.transform.Rotate(0, 180, 0, Space.World);
            m_cells[3, 3] = cell.GetComponent<NewCellClass>();
            m_cells[3, 3].Status = NewCellClass.CellState.Brack;
            m_fields[3, 3].Status = NewFieldCellClass.FieldState.Is;
        }
        if (m_fields[4, 4])
        {
            GameObject cell = Instantiate(m_cell.gameObject, new Vector2(4 - 8 / 2, 4 - 8 / 2), Quaternion.identity);
            cell.transform.Rotate(0, 180, 0, Space.World);
            m_cells[4, 4] = cell.GetComponent<NewCellClass>();
            m_cells[4, 4].Status = NewCellClass.CellState.Brack;
            m_fields[4, 4].Status = NewFieldCellClass.FieldState.Is;
        }
        if (m_fields[3, 4])
        {
            GameObject cell = Instantiate(m_cell.gameObject, new Vector2(3 - 8 / 2, 4 - 8 / 2), Quaternion.identity);
            m_cells[3, 4] = cell.GetComponent<NewCellClass>();
            m_cells[3, 4].Status = NewCellClass.CellState.White;
            m_fields[3, 4].Status = NewFieldCellClass.FieldState.Is;
        }
        if (m_fields[4, 3])
        {
            GameObject cell = Instantiate(m_cell.gameObject, new Vector2(4 - 8 / 2, 3 - 8 / 2), Quaternion.identity);
            m_cells[4, 3] = cell.GetComponent<NewCellClass>();
            m_cells[4, 3].Status = NewCellClass.CellState.White;
            m_fields[4, 3].Status = NewFieldCellClass.FieldState.Is;
        }
    }

    void TargetField(int selectX, int selectY)
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Material target = m_fields[x, y].TargetFieldColor();
                Material others = m_fields[x, y].OthersFieldColor();

                Renderer renderer = m_fields[x, y].GetComponent<Renderer>();
                renderer.material = (selectX == x && selectY == y ? target : others);
            }
        }
    }

    void Update()
    {
        TargetField(m_selectX, m_selectY);

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_selectX++;
            if (m_selectX >= 8)
            {
                m_selectX--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_selectX--;
            if (m_selectX < 0)
            {
                m_selectX++;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_selectY++;
            if (m_selectY >= 8)
            {
                m_selectY--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_selectY--;
            if (m_selectY < 0)
            {
                m_selectY++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && m_fields[m_selectX, m_selectY].Status == NewFieldCellClass.FieldState.None)
        {
            CheckCell(m_selectX, m_selectY);
        }
    }

    void CheckCell(int x, int y)
    {
        int noneCount = 0;

        Right(x, y, ref noneCount);
        Left(x, y, ref noneCount);
        Up(x, y, ref noneCount);
        Down(x, y, ref noneCount);

        UpperRight(x, y, ref noneCount);
        UpperLeft(x, y, ref noneCount);
        LowerRight(x, y, ref noneCount);
        LowerLeft(x, y, ref noneCount);

        if (noneCount == 8)
        {
            Debug.Log("どこもなし");
        }
        else
        {
            Debug.Log("おける");
        }
    }

    void Right(int right, int y,ref int count)
    {
        right++;
        if (right >= 8)
        {
            count++;
            return;
        }
        if (m_fields[right, y].Status == NewFieldCellClass.FieldState.None)
        {
            Debug.Log("右何もなし");
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[right, y].Status)
        {
            Debug.Log("同じ");
            return;
        }
        else
        {
            Right(right, y, ref count);
            m_cells[right, y].Chenge(right, y, m_cells);
        }
    }
    void Left(int left, int y, ref int count)
    {
        left--;
        if (left < 0)
        {
            count++;
            return;
        }
        if (m_fields[left, y].Status == NewFieldCellClass.FieldState.None)
        {
            Debug.Log("左何もなし");
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[left, y].Status)
        {
            Debug.Log("同じ");
            return;
        }
        else
        {
            Left(left, y, ref count);
            m_cells[left, y].Chenge(left, y, m_cells);
        }
    }
    void Up(int x, int up, ref int count)
    {
        up++;
        if (up >= 8)
        {
            count++;
            return;
        }
        if (m_fields[x, up].Status == NewFieldCellClass.FieldState.None)
        {
            Debug.Log("上何もなし");
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[x, up].Status)
        {
            Debug.Log("同じ");
            return;
        }
        else
        {
            Up(x, up, ref count);
            m_cells[x, up].Chenge(x, up, m_cells);
        }
    }
    void Down(int x, int down, ref int count)
    {
        down--;
        if (down < 0)
        {
            count++;
            return;
        }
        if (m_fields[x, down].Status == NewFieldCellClass.FieldState.None)
        {
            Debug.Log("下何もなし");
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[x, down].Status)
        {
            Debug.Log("同じ");
            return;
        }
        else
        {
            Down(x, down, ref count);
            m_cells[x, down].Chenge(x, down, m_cells);
        }
    }

    void UpperRight(int right, int up, ref int count)
    {
        right++;
        up++;
        if (right >= 8 || up >= 8)
        {
            count++;
            return;
        }
        if (m_fields[right, up].Status == NewFieldCellClass.FieldState.None)
        {
            Debug.Log("右上何もなし");
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[right, up].Status)
        {
            Debug.Log("同じ");
            return;
        }
        else
        {
            UpperRight(right, up, ref count);
            m_cells[right, up].Chenge(right, up, m_cells);
        }
    }
    void UpperLeft(int left, int up, ref int count)
    {
        left--;
        up++;
        if (left < 0 || up >= 8)
        {
            count++;
            return;
        }
        if (m_fields[left, up].Status == NewFieldCellClass.FieldState.None)
        {
            Debug.Log("左上何もなし");
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[left, up].Status)
        {
            Debug.Log("同じ");
            return;
        }
        else
        {
            UpperLeft(left, up, ref count);
            m_cells[left, up].Chenge(left, up, m_cells);
        }
    }
    void LowerRight(int right, int down, ref int count)
    {
        right++;
        down--;
        if (right >= 8 || down < 0)
        {
            count++;
            return;
        }
        if (m_fields[right, down].Status == NewFieldCellClass.FieldState.None)
        {
            Debug.Log("右下何もなし");
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[right, down].Status)
        {
            Debug.Log("同じ");
            return;
        }
        else
        {
            LowerRight(right, down, ref count);
            m_cells[right, down].Chenge(right, down, m_cells);
        }
    }
    void LowerLeft(int left, int down, ref int count)
    {
        left--;
        down--;
        if (left < 0 || down < 0)
        {
            count++;
            return;
        }
        if (m_fields[left, down].Status == NewFieldCellClass.FieldState.None)
        {
            Debug.Log("左下何もなし");
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[left, down].Status)
        {
            Debug.Log("同じ");
            return;
        }
        else
        {
            LowerLeft(left, down, ref count);
            m_cells[left, down].Chenge(left, down, m_cells);
        }
    }
}
