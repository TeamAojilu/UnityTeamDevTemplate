using SilCilSystem.Variables;
using UnityEngine;

namespace Samples.Shooting2D
{
	[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
	public class Spaceship : MonoBehaviour, IDamageReceiver
	{
		[Header("DamageReceiver")]
		[SerializeField] private string m_group = "Player";
		[SerializeField] private string m_damageTrigerName = "Damage";
		[SerializeField] private int m_hp = 1;
		[SerializeField] private int m_addScore = 100;
		[SerializeField] private VariableInt m_score = default;
		[SerializeField] private GameObject m_explosionPrefab = default;
		[SerializeField] private GameEvent m_onShipDestroyed = default;

		private Animator m_animator = default;

		public string Group => m_group;

        private void Start()
		{
			m_animator = GetComponent<Animator>();
		}

		public void Explosion()
		{
			Instantiate(m_explosionPrefab, transform.position, transform.rotation);
		}

        public void ApplyDamage(int power)
        {
			m_hp -= power;

			if (m_hp <= 0)
			{
				m_score.Value += m_addScore;
				Explosion();
				m_onShipDestroyed?.Publish();
				Destroy(gameObject);
			}
			else
			{
				m_animator.SetTrigger(m_damageTrigerName);
			}
		}
    }
}