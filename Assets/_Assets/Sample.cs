using SilCilSystem.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    [SerializeField] private VariableInt m_value = default;
    [SerializeField] private ReadonlyInt m_readonly = default;
    [SerializeField] private GameEventInt m_event = default;
    [SerializeField] private GameEventIntListener m_listener = default;

    [Header("Float")]
    [SerializeField] private ReadonlyFloat m_float = default;
}
