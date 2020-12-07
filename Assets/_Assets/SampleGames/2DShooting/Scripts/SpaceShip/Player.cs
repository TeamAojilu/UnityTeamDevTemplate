using UnityEngine;

namespace Samples.Shooting2D
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private float m_speed = 10f;

		private Camera m_camera = default;
		private Transform m_transform = default;

		private void Start()
		{
			m_transform = transform;
			m_camera = Camera.main;
		}

		private void Update()
		{
			float x = Input.GetAxisRaw("Horizontal");
			float y = Input.GetAxisRaw("Vertical");
			Vector2 direction = new Vector2(x, y).normalized;
			Move(direction);
		}

		private void Move(Vector2 direction)
		{
			Vector2 min = m_camera.ViewportToWorldPoint(new Vector2(0, 0));
			Vector2 max = m_camera.ViewportToWorldPoint(new Vector2(1, 1));
			Vector2 pos = m_transform.position;

			pos += direction * m_speed * Time.deltaTime;
			pos.x = Mathf.Clamp(pos.x, min.x, max.x);
			pos.y = Mathf.Clamp(pos.y, min.y, max.y);

			m_transform.position = pos;
		}
	}
}