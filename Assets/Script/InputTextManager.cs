using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTextManager : MonoBehaviour
{
    [SerializeField] UiManager m_ui;
    [SerializeField] Text m_setName;
    [SerializeField] Text m_sumple;

    bool m_active = true;
    bool m_inputBool = true;

    int m_count = 0;

    void Start() => m_setName.text = "";
    
    void Update()
    {
        if (!m_inputBool) return;

        if (Input.GetKeyDown(KeyCode.Q)) Set("q");
        if (Input.GetKeyDown(KeyCode.W)) Set("w");
        if (Input.GetKeyDown(KeyCode.E)) Set("e");
        if (Input.GetKeyDown(KeyCode.R)) Set("r");
        if (Input.GetKeyDown(KeyCode.T)) Set("t");
        if (Input.GetKeyDown(KeyCode.Y)) Set("y");
        if (Input.GetKeyDown(KeyCode.U)) Set("u");
        if (Input.GetKeyDown(KeyCode.I)) Set("i");
        if (Input.GetKeyDown(KeyCode.O)) Set("o");
        if (Input.GetKeyDown(KeyCode.P)) Set("p");
        if (Input.GetKeyDown(KeyCode.A)) Set("a");
        if (Input.GetKeyDown(KeyCode.S)) Set("s");
        if (Input.GetKeyDown(KeyCode.D)) Set("d");
        if (Input.GetKeyDown(KeyCode.F)) Set("f");
        if (Input.GetKeyDown(KeyCode.G)) Set("g");
        if (Input.GetKeyDown(KeyCode.H)) Set("h");
        if (Input.GetKeyDown(KeyCode.J)) Set("j");
        if (Input.GetKeyDown(KeyCode.K)) Set("k");
        if (Input.GetKeyDown(KeyCode.L)) Set("l");
        if (Input.GetKeyDown(KeyCode.Z)) Set("z");
        if (Input.GetKeyDown(KeyCode.X)) Set("x");
        if (Input.GetKeyDown(KeyCode.C)) Set("c");
        if (Input.GetKeyDown(KeyCode.V)) Set("v");
        if (Input.GetKeyDown(KeyCode.B)) Set("b");
        if (Input.GetKeyDown(KeyCode.N)) Set("n");
        if (Input.GetKeyDown(KeyCode.M)) Set("m");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_count++;
            if (m_setName.text.Length == 0) m_setName.text = "Guest";

            m_ui.SetName(m_setName.text);
            m_setName.text = "";
            if (!m_active)
            {
                m_active = true;
                m_sumple.enabled = m_active;
            }

            if (m_count == 2)
            {
                m_sumple.text = "Game Start";
                m_inputBool = false;
            }
        }
    }

    void Set(string set)
    {
        if (m_active)
        {
            m_active = false;
            m_sumple.enabled = m_active;
        }
        
        m_setName.text += set;
    }
}
