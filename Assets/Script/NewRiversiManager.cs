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

    [SerializeField] UiManager m_ui;

    int m_selectX = 4;
    int m_selectY = 4;

    int m_seveInt;

    bool m_check = false;

    void Start()
    {
        CreateField();
        FirstSetCell();
        m_ui.GetText(m_nowCellState.ToString());

        CountCell();
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
    void CountCell()
    {
        int brackCount = 0;
        int whiteCount = 0;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (m_fields[x, y].Status == NewFieldCellClass.FieldState.None) continue;
                if (m_cells[x, y].Status == NewCellClass.CellState.Brack)
                {
                    brackCount++;
                }
                else if (m_cells[x, y].Status == NewCellClass.CellState.White)
                {
                    whiteCount++;
                }
            }
        }

        if (brackCount == 0 || whiteCount == 0)
        {
            
        }

        m_ui.GetBrackCount(brackCount);
        m_ui.GetWhiteCount(whiteCount);
    }
    bool SetCellCheck()
    {
        bool result = true;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                int count = 0;
                for (int targetX  = x - 1; targetX  <= x + 1; targetX++)
                {
                    for (int targetY = y - 1; targetY <= y + 1; targetY++)
                    {
                        if (targetX < 0 || targetX >= 8) continue;
                        if (targetY < 0 || targetY >= 8) continue;
                        if (targetX == x && targetY == y) continue;
                        if (m_fields[x, y].Status == NewFieldCellClass.FieldState.Is) continue;
                        if (m_fields[targetX, targetY].Status == NewFieldCellClass.FieldState.None)
                        {
                            count++;
                            continue;
                        }

                        if (m_nowCellState == m_cells[targetX, targetY].Status)
                        {
                            count++;
                            
                            if(count == 8)
                            {
                                Debug.Log("置けるところなし");
                            }
                        }
                        else
                        {
                            result = false;
                        }
                        Debug.Log(count);
                    }
                }
            }
        }

        return result;
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
            bool set = CheckCell(m_selectX, m_selectY);
            if (set) SetCellAndChengePlayer(m_selectX, m_selectY);
            CountCell();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && m_fields[m_selectX, m_selectY].Status != NewFieldCellClass.FieldState.None)
        {
            m_ui.SetMsgImage("Already put");
        }
    }
    
    void SetCellAndChengePlayer(int x, int y)
    {
        m_fields[x, y].Status = NewFieldCellClass.FieldState.Is;
        GameObject set = Instantiate(m_cell.gameObject, new Vector2(x - 8 / 2, y - 8 / 2), Quaternion.identity);
        m_cells[x, y] = set.GetComponent<NewCellClass>();
        m_cells[x, y].Status = m_nowCellState;
        m_cells[x, y].SetCell(m_nowCellState, set);
        bool result = SetCellCheck();
        Debug.Log(result);
        if (m_nowCellState == NewCellClass.CellState.Brack) m_nowCellState = NewCellClass.CellState.White;
        else m_nowCellState = NewCellClass.CellState.Brack;

        m_ui.GetText(m_nowCellState.ToString());
    }

    bool CheckCell(int x, int y)
    {
        bool result = false;
        int noneCount = 0;

        Right(x, y, ref noneCount);
        Left(x, y, ref noneCount);
        Up(x, y, ref noneCount);
        Down(x, y, ref noneCount);
        UpperRight(x, y, ref noneCount);
        UpperLeft(x, y, ref noneCount);
        LowerRight(x, y, ref noneCount);
        LowerLeft(x, y, ref noneCount);
        
        if (noneCount >= 1 && !m_check) m_ui.SetMsgImage("Can't put");
        else result = true;
       
        m_check = false;
        m_seveInt = 0;

        return result;
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
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[right, y].Status)
        {
            return;
        }
        else
        {
            m_seveInt = count;
            Right(right, y, ref count);
            if (m_seveInt == count)
            {
                m_cells[right, y].Chenge(right, y, m_cells, m_nowCellState);
                m_check = true;
            }
            else
            {
                count++;
            }
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
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[left, y].Status)
        {
            return;
        }
        else
        {
            m_seveInt = count;
            Left(left, y, ref count);
            if (m_seveInt == count)
            {
                m_cells[left, y].Chenge(left, y, m_cells, m_nowCellState);
                m_check = true;
            }
            else
            {
                count++;
            }
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
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[x, up].Status)
        {
            return;
        }
        else
        {
            m_seveInt = count;
            Up(x, up, ref count);
            if (m_seveInt == count)
            {
                m_cells[x, up].Chenge(x, up, m_cells, m_nowCellState);
                m_check = true;
            }
            else
            {
                count++;
            }
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
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[x, down].Status)
        {
            
            return;
        }
        else
        {
            m_seveInt = count;
            Down(x, down, ref count);
            if (m_seveInt == count)
            {
                m_cells[x, down].Chenge(x, down, m_cells, m_nowCellState);
                m_check = true;
            }
            else
            {
                count++;
            }
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
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[right, up].Status)
        {
            return;
        }
        else
        {
            m_seveInt = count;
            UpperRight(right, up, ref count);
            if (m_seveInt == count)
            {
                m_cells[right, up].Chenge(right, up, m_cells, m_nowCellState);
                m_check = true;
            }
            else
            {
                count++;
            }
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
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[left, up].Status)
        {
            return;
        }
        else
        {
            m_seveInt = count;
            UpperLeft(left, up, ref count);
            if (m_seveInt == count)
            {
                m_cells[left, up].Chenge(left, up, m_cells, m_nowCellState);
                m_check = true;
            }
            else
            {
                count++;
            }
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
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[right, down].Status)
        {
            return;
        }
        else
        {
            m_seveInt = count;
            LowerRight(right, down, ref count);
            if (m_seveInt == count)
            {
                m_cells[right, down].Chenge(right, down, m_cells, m_nowCellState);
                m_check = true;
            }
            else
            {
                count++;
            }
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
            count++;
            return;
        }
        else if (m_nowCellState == m_cells[left, down].Status)
        {
            return;
        }
        else
        {
            m_seveInt = count;
            LowerLeft(left, down, ref count);
            if (m_seveInt == count)
            {
                m_cells[left, down].Chenge(left, down, m_cells, m_nowCellState);
                m_check = true;
            }
            else
            {
                count++;
            }
        }
    }
}
