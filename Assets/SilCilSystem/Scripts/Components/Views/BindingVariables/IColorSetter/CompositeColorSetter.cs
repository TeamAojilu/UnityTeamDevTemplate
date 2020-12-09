using System.Collections.Generic;
using UnityEngine;

namespace SilCilSystem.Components.Views
{
    public class CompositeColorSetter : IColorSetter
    {
        private List<IColorSetter> m_colorSetters = new List<IColorSetter>();

        public void Add(IColorSetter item) => m_colorSetters.Add(item);
        public void Remove(IColorSetter item) => m_colorSetters.Remove(item);

        public void SetColor(Color color)
        {
            foreach(var item in m_colorSetters)
            {
                item?.SetColor(color);
            }
        }
    }
}