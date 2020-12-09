using System;
using System.Collections.Generic;
using UnityEngine;

namespace SilCilSystem.Components.Views
{
    [Serializable]
    internal class MaterialInfo
    {
        public string m_propertyName = "_Color";
        public Material[] m_materials = default;
        public Renderer[] m_renderers = default;
        public bool m_useSharedMaterial = false;
        public bool m_onlyMainMaterial = true;

        public IColorSetter GetColorSetter()
        {
            var colorSettter = new CompositeColorSetter();
            foreach (var material in GetMaterials())
            {
                colorSettter.Add(material.GetColorSetter(m_propertyName));
            }
            return colorSettter;
        }

        private IEnumerable<Material> GetMaterials()
        {
            foreach (var material in m_materials) yield return material;
            foreach (var renderer in m_renderers)
            {
                if (m_onlyMainMaterial)
                {
                    yield return (m_useSharedMaterial) ? renderer.sharedMaterial : renderer.material;
                }
                else
                {
                    foreach (var material in (m_useSharedMaterial) ? renderer.sharedMaterials : renderer.materials)
                    {
                        yield return material;
                    }
                }
            }
        }
    }
}
