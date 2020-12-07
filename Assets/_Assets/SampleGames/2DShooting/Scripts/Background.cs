using UnityEngine;

namespace Samples.Shooting2D
{
	public class Background : MonoBehaviour
	{
		[SerializeField] private float m_speed = 0.1f;
		[SerializeField] private string m_textureName = "_BaseMap";
		private Renderer m_renderer;

        private void Start()
        {
			m_renderer = GetComponent<Renderer>();
        }

        void Update()
		{
			float y = Mathf.Repeat(Time.time * m_speed, 1);
			Vector2 offset = new Vector2(0, y);
			m_renderer.sharedMaterial.SetTextureOffset(m_textureName, offset);
		}
	}
}