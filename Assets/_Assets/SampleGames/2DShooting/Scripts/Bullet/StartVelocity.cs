using UnityEngine;

namespace Samples.Shooting2D
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class StartVelocity : MonoBehaviour
	{
		[SerializeField] private Space m_space = default;
		[SerializeField] private Vector3 m_velocity = Vector3.zero;

		private void Start()
        {
			Vector3 velocity;
            switch (m_space)
            {
				default:
				case Space.World:
					velocity = m_velocity;
					break;
				case Space.Self:
					velocity = transform.worldToLocalMatrix * m_velocity;
					break;
            }
			GetComponent<Rigidbody2D>().velocity = velocity;
		}
	}
}