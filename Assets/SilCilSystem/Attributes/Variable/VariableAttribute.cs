using System;
using System.Diagnostics;
using System.Linq;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Editors
{
    /// <summary>
    /// VariableAssetの派生クラスが対象.
    /// parentTypesで指定されたVariableAssetの子供として生成できるようになる.
    /// (正確にはメニュー表示される)
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Class)]
    public class VariableAttribute : Attribute
    {
        private readonly string m_suffix;
        private readonly Type[] m_parentTypes;
        
        public string MenuPath { get; private set; }
        
        public VariableAttribute(string suffix, string menuPath = null, params Type[] parentTypes)
        {
            m_suffix = suffix;
            MenuPath = (string.IsNullOrWhiteSpace(menuPath)) ? null : menuPath;
            m_parentTypes = parentTypes.Where(x => typeof(VariableAsset).IsAssignableFrom(x)).ToArray();
        }

        public bool CanBeChild(VariableAsset[] assetIncludeChildren)
        {
            foreach(var asset in assetIncludeChildren)
            {
                if (m_parentTypes.Any(x => x.IsAssignableFrom(asset.GetType()))) return true;
            }
            return false;
        }

        public string GetName(string parentName)
        {
            return $"{parentName}_{m_suffix}";
        }
    }

    /// <summary>
    /// 子供として生成された場合に呼ばれる関数を指定する.
    /// 型はvoid (VariableAsset)であること.
    /// VariableAttributeをつけていないクラスで使用しても無視される.
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Method)]
    public class OnAttachedAttribute : Attribute { }
}