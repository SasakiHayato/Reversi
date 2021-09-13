using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Text m_playerText;
    [SerializeField] Text m_brackCount;
    [SerializeField] Text m_whiteCount;

    public void GetText(string set) => m_playerText.text = $"Player:{set}";
    public void GetBrackCount(int set) => m_brackCount.text = $"Brack:{set}";
    public void GetWhiteCount(int set) => m_whiteCount.text = $"White:{set}";
}
