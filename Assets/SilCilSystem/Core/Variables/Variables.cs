using System;
using System.Diagnostics;
using SilCilSystem.Variables.Base;
using UnityEngine;
using UnityEngine.SceneManagement;
<<<<<<< Updated upstream
=======
using Debug = UnityEngine.Debug;
>>>>>>> Stashed changes

namespace SilCilSystem.Variables
{
    public abstract class GameEvent : VariableAsset
    {
        public abstract IDisposable Subscribe(Action action);
        public abstract void Publish();
    }

    public abstract class GameEventListener : VariableAsset
    {
        public abstract IDisposable Subscribe(Action action);
    }
}

namespace SilCilSystem.Variables.Generic
{
    public abstract class Variable<T> : VariableAsset
    {
        public abstract T Value { get; set; }
        public void SetValue(T value) => Value = value;
        public static implicit operator T(Variable<T> variable) => variable.Value;

        #region Restore
<<<<<<< Updated upstream
        protected T m_initialValue = default;
        
        public virtual void Restore()
        {
            Value = m_initialValue;
=======
        [SerializeReference, HideInInspector]
        private T initialValue;

        [SerializeField, HideInInspector] private bool isInitialized;

        public virtual void Restore()
        {
            Value = initialValue;
>>>>>>> Stashed changes
        }

        protected virtual void OnEnable()
        {
<<<<<<< Updated upstream
            m_initialValue = Value;

=======
            if(m_restoreValue && isInitialized) Restore();
            #if UNITY_EDITOR
                initialValue = Value;
                isInitialized = true;
            #endif
   
>>>>>>> Stashed changes
            // 重複登録を防ぐために解除してから登録.
            SceneChangedDispatcher.UnRegister(OnSceneChanged, ExecutionOrder);
            SceneChangedDispatcher.Register(OnSceneChanged, ExecutionOrder);
            
            // エディタ専用.
            RegisterPlayModeChanged();
        }

        protected virtual void OnDisable()
        {
            SceneChangedDispatcher.UnRegister(OnSceneChanged, ExecutionOrder);
        }

        // エディタ上で描画順を後にしたいのでHideInInspector.
        [SerializeField, Tooltip("trueの時、シーン切り替え時に値をリセットします"), HideInInspector] internal bool m_restoreOnSceneChanged = false;
        private const int ExecutionOrder = DisposeOnSceneChangedExtensios.ExecutionOrder + 1;

        private void OnSceneChanged(Scene arg0, Scene arg1)
        {
            if (m_restoreOnSceneChanged) Restore();
        }

<<<<<<< Updated upstream
        public void RestoreOnGameObejctDestroyed(GameObject gameObject)
=======
        public void RestoreOnGameObjectDestroyed(GameObject gameObject)
>>>>>>> Stashed changes
        {
            DelegateDispose.Create(Restore).DisposeOnDestroy(gameObject);
        }

<<<<<<< Updated upstream
#if UNITY_EDITOR
        // エディタ上で描画順を後にしたいのでHideInInspector.
        [SerializeField, Tooltip("エディタ専用: trueの時、プレイモード終了後に値をリセットします"), HideInInspector] internal bool m_restoreValue = false;
#endif
        
=======
        // エディタ上で描画順を後にしたいのでHideInInspector.
        [SerializeField, Tooltip("エディタ専用: trueの時、プレイモード終了後に値をリセットします"), HideInInspector] internal bool m_restoreValue = false;

>>>>>>> Stashed changes
        [Conditional("UNITY_EDITOR")]
        private void RegisterPlayModeChanged()
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode == false) return;

            UnityEditor.EditorApplication.playModeStateChanged += _ => 
            {
                if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode == false && m_restoreValue)
                {
<<<<<<< Updated upstream
=======
                    isInitialized = false;
>>>>>>> Stashed changes
                    Restore();
                }
            };
#endif
        }
#endregion
    }

    public abstract class ReadonlyVariable<T> : VariableAsset
    {
        public abstract T Value { get; }
        public static implicit operator T(ReadonlyVariable<T> variable) => variable.Value;
    }
    
    public abstract class GameEvent<T> : GameEvent
    {
        public abstract IDisposable Subscribe(Action<T> action);
        public abstract void Publish(T value);

        public override void Publish() => Publish(default);
        public override IDisposable Subscribe(Action action) => Subscribe(_ => action?.Invoke());
    }
    
    public abstract class GameEventListener<T> : GameEventListener
    {
        public abstract IDisposable Subscribe(Action<T> action);

        public override IDisposable Subscribe(Action action) => Subscribe(_ => action?.Invoke());
    }
}
