using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

namespace SilCilSystem.Components.Views
{
    public interface IColorSetter
    {
        void SetColor(Color color);
    }

    internal class ColorSetterSpriteRenderer : IColorSetter
    {
        private readonly SpriteRenderer m_spriteRenderer = default;
        public ColorSetterSpriteRenderer(SpriteRenderer spriteRenderer) => m_spriteRenderer = spriteRenderer;
        public void SetColor(Color color) => m_spriteRenderer.color = color;
    }

    // Image, RawImage, TextなどUI要素.
    internal class ColorSetterUIGraphic : IColorSetter
    {
        private readonly Graphic m_graphic = default;
        public ColorSetterUIGraphic(Graphic graphic) => m_graphic = graphic;
        public void SetColor(Color color) => m_graphic.color = color;
    }

    internal class ColorSetterTileMap : IColorSetter
    {
        private readonly Tilemap m_tilemap = default;
        public ColorSetterTileMap(Tilemap tilemap) => m_tilemap = tilemap;
        public void SetColor(Color color) => m_tilemap.color = color;
    }

    internal class ColorSetterMaterial : IColorSetter
    {
        private readonly Material m_material = default;
        private readonly string m_propertyName = default;

        public ColorSetterMaterial(Material material, string propertyName)
        {
            m_material = material;
            m_propertyName = propertyName;
        }

        public void SetColor(Color color) => m_material.SetColor(m_propertyName, color);
    }

    internal class ColorSetterTextMesh : IColorSetter
    {
        private readonly TextMesh m_textMesh;
        public ColorSetterTextMesh(TextMesh textMesh) => m_textMesh = textMesh;
        public void SetColor(Color color) => m_textMesh.color = color;
    }

    internal class ColorSetterTextMeshPro : IColorSetter
    {
        private readonly TextMeshPro m_textMesh;
        public ColorSetterTextMeshPro(TextMeshPro textMesh) => m_textMesh = textMesh;
        public void SetColor(Color color) => m_textMesh.color = color;
    }

    public static class ColorSetterExtensions
    {
        /// <summary>
        /// Sprite, TileMap, UI.Graphic, TextMesh, TextMeshPro, IColorSetterを継承したComponentを取得.
        /// 【挙動】ない場合はnull
        /// </summary>
        public static IColorSetter GetColorComponent(this GameObject gameObject)
        {
            if (gameObject == null) return null;
            if (gameObject.TryGetComponent(out Graphic graphic)) return new ColorSetterUIGraphic(graphic);
            if (gameObject.TryGetComponent(out SpriteRenderer spriteRenderer)) return new ColorSetterSpriteRenderer(spriteRenderer);
            if (gameObject.TryGetComponent(out Tilemap tilemap)) return new ColorSetterTileMap(tilemap);
            if (gameObject.TryGetComponent(out TextMesh textMesh)) return new ColorSetterTextMesh(textMesh);
            if (gameObject.TryGetComponent(out TextMeshPro textMeshPro)) return new ColorSetterTextMeshPro(textMeshPro);
            if (gameObject.TryGetComponent(out IColorSetter colorSetter)) return colorSetter;

            return null;
        }

        public static IColorSetter GetColorSetter(this Material material, string propertyName = "_Color")
        {
            if (material == null) return null;
            return new ColorSetterMaterial(material, propertyName);
        }
    }
}
