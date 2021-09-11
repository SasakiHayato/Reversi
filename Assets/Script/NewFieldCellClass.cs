using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFieldCellClass : MonoBehaviour
{
    public enum FieldState
    {
        Is,
        None,
    }
    public FieldState Status { get; set; }

    [SerializeField] Material m_select;
    [SerializeField] Material m_default;

    public Material TargetFieldColor() => m_select;
    public Material OthersFieldColor() => m_default;
}
