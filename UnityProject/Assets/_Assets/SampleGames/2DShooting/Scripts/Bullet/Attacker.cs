using UnityEngine;

namespace Samples.Shooting2D
{
    public interface IDamageReceiver
    {
        /// <summary>同じGroupの場合、ダメージを与えない</summary>
        string Group { get; }
        /// <summary>ダメージ処理を行う</summary>
        void ApplyDamage(int power);
    }

    public class Attacker : MonoBehaviour
    {
		[SerializeField] private string m_group = "Player";
		[SerializeField] private int m_power = 1;
		[SerializeField] private bool m_destroyOnHit = true;

		private void OnTriggerEnter2D(Collider2D c)
		{
			if (!c.TryGetComponent(out IDamageReceiver receiver)) return;
			if (receiver.Group == m_group) return;

			receiver.ApplyDamage(m_power);
			if (m_destroyOnHit) Destroy(gameObject);
		}
	}
}