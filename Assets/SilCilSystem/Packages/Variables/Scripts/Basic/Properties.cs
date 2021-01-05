using System;
using UnityEngine;

namespace SilCilSystem.Variables.Generic
{
    // 2019ではVaraible<T>がシリアライズできないので使用不可.
    [Obsolete, Serializable]
    public abstract class Property<T>
    {
        [SerializeField] private T m_value = default;
        [SerializeField] private Variable<T> m_variable = default;

        public Property(T value) => Value = value;

        public T Value
        {
            get => (m_variable != null) ? m_variable.Value : m_value;
            set
            {
                m_value = value;
                m_variable?.SetValue(value);
            }
        }

        public Variable<T> Variable
        {
            get => m_variable;
            set
            {
                m_value = value;
                m_variable = value;
            }
        }

        public void SetValue(T value) => Value = value;
        public static implicit operator T(Property<T> property) => property.Value;
    }

    [Obsolete, Serializable]
    public abstract class ReadonlyProperty<T>
    {
        [SerializeField] private T m_value = default;
        [SerializeField] private ReadonlyVariable<T> m_variable = default;

        public ReadonlyProperty(T value) => m_value = value;

        public T Value => (m_variable != null) ? m_variable.Value : m_value;

        public ReadonlyVariable<T> Variable
        {
            get => m_variable;
            set
            {
                m_value = value;
                m_variable = value;
            }
        }

        public static implicit operator T(ReadonlyProperty<T> property) => property.Value;
    }

    [Serializable]
    public abstract class Property<TType, TVariable> where TVariable : Variable<TType>
    {
        [SerializeField] private TType m_value = default;
        [SerializeField] private TVariable m_variable = default;

        public Property(TType value) => Value = value;

        public TType Value
        {
            get => (m_variable != null) ? m_variable.Value : m_value;
            set
            {
                m_value = value;
                m_variable?.SetValue(value);
            }
        }

        public TVariable Variable
        {
            get => m_variable;
            set
            {
                m_value = value;
                m_variable = value;
            }
        }

        public void SetValue(TType value) => Value = value;
        public static implicit operator TType(Property<TType, TVariable> property) => property.Value;
    }
    
    [Serializable]
    public abstract class ReadonlyProperty<TType, TReadonly> where TReadonly : ReadonlyVariable<TType>
    {
        [SerializeField] private TType m_value = default;
        [SerializeField] private TReadonly m_variable = default;

        public ReadonlyProperty(TType value) => m_value = value;

        public TType Value => (m_variable != null) ? m_variable.Value : m_value;

        public TReadonly Variable
        {
            get => m_variable;
            set
            {
                m_value = value;
                m_variable = value;
            }
        }

        public static implicit operator TType(ReadonlyProperty<TType, TReadonly> property) => property.Value;
    }
}