using UnityEngine;
using SilCilSystem.Timers;

namespace Samples.Shooting2D
{
    public class ShotBullet : MonoBehaviour
    {
		[SerializeField] private float m_shotDelay = 1f;
		[SerializeField] private GameObject m_bulletPrefab = default;

		private Transform m_transform = default;

        private void Start()
        {
			m_transform = transform;
			TimeMethods.CallRepeatedly(Shot, m_shotDelay, gameObject, enabled: () => enabled);
        }

		private void Shot()
		{
			for (int i = 0; i < m_transform.childCount; i++)
			{
				Transform shotPosition = m_transform.GetChild(i);
				Instantiate(m_bulletPrefab, shotPosition.position, shotPosition.rotation);
			}
		}
	}
}