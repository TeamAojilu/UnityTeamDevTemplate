using UnityEngine;

namespace Samples.Shooting2D
{
    public class ShotBullet : MonoBehaviour
    {
		[SerializeField] private  float m_shotDelay = 1f;
		[SerializeField] private GameObject m_bulletPrefab = default;

		private Transform m_transform = default;
		private float m_timer = 10000f; // 1フレーム目で撃つために大きい値.

        private void Start()
        {
			m_transform = transform;
        }

        private void Update()
        {
			m_timer += Time.deltaTime;
			if (m_timer > m_shotDelay) 
			{
				m_timer = 0f;
				for (int i = 0; i < m_transform.childCount; i++)
				{
					Transform shotPosition = m_transform.GetChild(i);
					Shot(shotPosition);
				}
			}
        }

		private void Shot(Transform origin)
		{
			Instantiate(m_bulletPrefab, origin.position, origin.rotation);
		}
	}
}