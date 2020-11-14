using UnityEngine;
using SilCilSystem.Variables;

[CreateAssetMenu(menuName = "Variables/Custom/Vector3")]
public class VariableVector3 : Variable<Vector3>
{
    [SerializeField] private Vector3 m_value = default; 

    public override Vector3 Value
    {
        get => m_value;
        set => m_value = value;
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(VariableVector3))]
public class VariableVector3Editor : SilCilSystem.Editors.VariableEditor<VariableVector3, ReadonlyVector3> { }
#endif