using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    [Serializable]
    public class Property<T, TVariable> where TVariable : Variable<T>
    {
        [SerializeField] private T m_value = default;
        [SerializeField] private TVariable m_variable = default;

        public Property(T value) => Value = value;

        public T Value
        {
            get => m_variable ?? m_value;
            set
            {
                m_value = value;
                m_variable?.SetValue(value);
            }
        }

        public static implicit operator T(Property<T, TVariable> property) => property.Value;
    }

    [Serializable]
    public class ReadonlyProperty<T, TVariable> where TVariable : ReadonlyVariable<T>
    {
        [SerializeField] private T m_value = default;
        [SerializeField] private TVariable m_variable = default;

        public ReadonlyProperty(T value) => m_value = value;

        public T Value => m_variable ?? m_value;
        public static implicit operator T(ReadonlyProperty<T, TVariable> property) => property.Value;
    }
}