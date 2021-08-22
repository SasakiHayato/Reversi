using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FieldStatus
{
    Is,
    None,
}

public class FieldCellClass : MonoBehaviour
{
    [SerializeField] Material m_select;
    [SerializeField] Material m_default;
    FieldStatus m_status;

    public FieldStatus SetStatus(FieldStatus status) { return m_status = status; }
    public FieldStatus RetuneStatus() { return m_status; }
    public Material TargetFieldColor() { return m_select; }
    public Material OthersFieldColor() { return m_default; }
}
