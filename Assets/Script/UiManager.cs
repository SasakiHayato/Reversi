using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Text m_playerText;
    [SerializeField] Text m_brackCount;
    [SerializeField] Text m_whiteCount;
    [SerializeField] GameObject m_CannotPutImage;
    [SerializeField] GameObject m_resultImage;
    [SerializeField] GameObject m_titleImage;
    [SerializeField] GameObject m_button;

    string[] m_player = new string[2];
    int m_setNum = 0;

    public void GetText(string set, int id) => m_playerText.text = $"{set}\n{m_player[id]}";
    public void GetBrackCount(int set) => m_brackCount.text = $"Brack:{set}";
    public void GetWhiteCount(int set) => m_whiteCount.text = $"White:{set}";

    public void SetUiForStart()
    {
        m_button.SetActive(false);
        m_titleImage.SetActive(false);
    }
    public void SetMsgImage(string set)
    {
        m_CannotPutImage.SetActive(true);
        Text setText = m_CannotPutImage.transform.GetChild(0).gameObject.GetComponent<Text>();
        setText.text = set;
        StartCoroutine(ImageActiveForFalse(m_CannotPutImage, 1));
    }
    IEnumerator ImageActiveForFalse(GameObject setObject, float time)
    {
        yield return new WaitForSeconds(time);
        setObject.SetActive(false);
    }

    public void SetResultImage(string set)
    {
        m_resultImage.SetActive(true);
        Text setText = m_resultImage.transform.GetChild(0).gameObject.GetComponent<Text>();
        setText.text = $"Winner\n\n{set}";

        GameManager.getInstance().SetIsPlay(false);
    }
    public void SetName(string set)
    {
        m_player[m_setNum] = set;
        m_setNum++;
    }
}
