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

    public void GetText(string set) => m_playerText.text = $"Player:{set}";
    public void GetBrackCount(int set) => m_brackCount.text = $"Brack:{set}";
    public void GetWhiteCount(int set) => m_whiteCount.text = $"White:{set}";
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
}
