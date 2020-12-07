using UnityEngine;
using System.Collections;
using SilCilSystem.Variables;

namespace Samples.Shooting2D
{
	public class Emitter : MonoBehaviour
	{
		[SerializeField] private ReadonlyBool m_isPlaying = default;
		[SerializeField] private GameObject[] m_wavePrefabs = default;

		private int m_currentWave;

		private IEnumerator Start()
		{
			if (m_wavePrefabs.Length == 0) yield break;
            
			while (true)
			{
				while (m_isPlaying.Value == false)
				{
					yield return new WaitForEndOfFrame();
				}

				GameObject g = Instantiate(m_wavePrefabs[m_currentWave], transform.position, Quaternion.identity);

				g.transform.parent = transform;

				while (g.transform.childCount != 0)
				{
					yield return new WaitForEndOfFrame();
				}

				Destroy(g);

				m_currentWave++;
				m_currentWave %= m_wavePrefabs.Length;
			}
		}
	}
}