using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    int m_selectX = 0;
    int m_selectY = 0;

    static int m_wide;
    static int m_height;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_selectX++;
            if (m_selectX >= m_wide)
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
            if (m_selectY >= m_height)
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
    }

    public int SetX() { return m_selectX; }
    public int SetY() { return m_selectY; }

    public int SetWide(int wide) { return m_wide = wide; }
    public int SetHeight(int height) { return m_height = height; }
}
