using UnityEngine;

namespace Samples.Shooting2D
{
    public class LifeTime : MonoBehaviour
    {
        [SerializeField] private float m_lifeTime = 10f;
        
        private void Start()
        {
            Destroy(gameObject, m_lifeTime);
        }
    }
}