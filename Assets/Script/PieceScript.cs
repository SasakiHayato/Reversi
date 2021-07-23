using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceStatus
{
    White,
    Brack,
}

public class PieceScript : MonoBehaviour
{
    private PieceStatus[,] m_piece = new PieceStatus[8, 8];

    public GameObject SetPiece(GameObject piecePrefab, int x, int y)
    {
        if (m_piece[x, y] == PieceStatus.White)
        {
            piecePrefab.transform.Rotate(0, 0, 0, Space.World);

        }
        if (m_piece[x, y] == PieceStatus.Brack)
        {
            piecePrefab.transform.Rotate(0, 180, 0, Space.World);
        }

        return piecePrefab;
    }

    public PieceStatus SetStatusToField(int x, int y, PieceStatus status)
    {
        m_piece[x, y] = status;
        return m_piece[x, y];
    }

    public PieceStatus NowStatus(int count, PieceStatus status)
    {
        if (0 == count % 2)
        {
            status = PieceStatus.Brack;
        }
        else
        {
            status = PieceStatus.White;
        }
        return status;
    }
}
