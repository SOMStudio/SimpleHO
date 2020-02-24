using UnityEngine;

namespace Base.Utility
{
    public class ParticleManager : MonoBehaviour
    {
        [Header("Main")]
        [SerializeField] private ParticleSystem particleSystem;

        public void Play()
        {
            particleSystem.Play();
        }

        public void Stop()
        {
            particleSystem.Stop();
        }
    }
}
