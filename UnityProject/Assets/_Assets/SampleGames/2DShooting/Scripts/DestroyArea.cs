using UnityEngine;

namespace Samples.Shooting2D
{
	public class DestroyArea : MonoBehaviour
	{
		private void OnTriggerExit2D(Collider2D c)
		{
			Destroy(c.gameObject);
		}
	}
}