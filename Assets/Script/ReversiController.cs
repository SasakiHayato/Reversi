using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversiController : MonoBehaviour
{
    private enum StageStatus
    {
        Is,
        None,
    }

    [SerializeField] GameObject m_stagePrefab = null;
    [SerializeField] GameObject m_piecePrefab = null;

    private GameObject[,] m_cellArray = new GameObject[m_height, m_wide];

    [SerializeField] Material m_default = null;
    [SerializeField] Material m_select = null;
    
    private const int m_height = 8;
    private const int m_wide = 8;

    private int m_selectX = 0;
    private int m_selectY = 0;

    StageStatus[,] m_stage = new StageStatus[m_height, m_wide];

    PieceScript m_pieceSc = new PieceScript();

    private int m_count = 0;
    
    void Start()
    {  
        CreateStage();
        StartSetPiece();
    }

    void Update()
    {
        Move();
        SelectCellCheck();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetPiece();
        }
    }

    private void ChengePiece(int x, int y, PieceStatus status)
    {
        PieceStatus piece = new PieceStatus();
        if (status == PieceStatus.White)
        {
            piece = PieceStatus.Brack;
        }
        else
        {
            piece = PieceStatus.White;
        }

        RightCheck(x, y, piece);
    }
    
    private void RightCheck(int x, int y, PieceStatus status)
    {

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

    private void SetPiece()
    {
        PieceStatus piece = new PieceStatus();
        if (m_stage[m_selectX, m_selectY] == StageStatus.Is) { Debug.Log("Is"); return; }
        
        m_pieceSc.SetStatusToField(m_selectX, m_selectY, m_pieceSc.NowStatus(m_count, piece));
        CreatePiece(m_selectX, m_selectY);
        
        ChengePiece(m_selectX, m_selectY, m_pieceSc.NowStatus(m_count, piece));
        m_count++;
    }

    private void SelectCellCheck()
    {
        for (int i = 0; i < m_height; i++)
        {
            for (int j = 0; j < m_wide; j++)
            {
                Renderer renderer = m_cellArray[i, j].GetComponent<Renderer>();
                renderer.material = (m_selectX == i && m_selectY == j ? m_select : m_default);
            }
        }
    }

    private void CreateStage()
    {
        for (int i = 0; i < m_height; i++)
        {
            for (int j = 0; j < m_wide; j++)
            {
                GameObject cell = Instantiate(m_stagePrefab, new Vector2(i - m_height / 2, j - m_wide / 2), Quaternion.identity);
                m_cellArray[i, j] = cell;
                m_stage[i, j] = StageStatus.None;
            }
        }
    }

    private void StartSetPiece()
    {
        if (m_stage[4, 4] == StageStatus.None)
        {
            m_pieceSc.SetStatusToField(4, 4, PieceStatus.White);
            CreatePiece(4, 4);

        }
        if (m_stage[3, 3] == StageStatus.None)
        {
            m_pieceSc.SetStatusToField(3, 3, PieceStatus.White);
            CreatePiece(3, 3);
        }
        if (m_stage[4, 3] == StageStatus.None)
        {
            m_pieceSc.SetStatusToField(4, 3, PieceStatus.Brack);
            CreatePiece(4, 3);
        }
        if (m_stage[3, 4] == StageStatus.None)
        {
            m_pieceSc.SetStatusToField(3, 4, PieceStatus.Brack);
            CreatePiece(3, 4);
        }
    }

    private void CreatePiece(int x, int y)
    {
        Vector2 vector = new Vector2(x - m_height / 2, y - m_wide / 2);

        var a = Instantiate(m_piecePrefab, vector, Quaternion.identity);
        m_pieceSc.SetPiece(a, x, y);
        m_stage[x, y] = StageStatus.Is;
    }
}
