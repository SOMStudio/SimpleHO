using System;
using UnityEngine;
using UnityEngine.Events;

namespace Base.Utility
{
    public sealed class OnTriggerManager : MonoBehaviour
    {
        public ColliderEvent onTriggerEnterEvent;
        public ColliderEvent onTriggerExitEvent;

        private void OnTriggerEnter(Collider other)
        {
            onTriggerEnterEvent?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            onTriggerExitEvent?.Invoke(other);
        }

        [Serializable]
        public class ColliderEvent : UnityEvent<Collider>
        {
        }
    }
}
