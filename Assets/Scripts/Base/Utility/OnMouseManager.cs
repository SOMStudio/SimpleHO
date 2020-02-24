using UnityEngine;
using UnityEngine.Events;

namespace Base.Utility
{
    public class OnMouseManager : MonoBehaviour
    {
        public UnityEvent onMouseDownEvent;
        public UnityEvent onMouseDownExit;
        public UnityEvent onMouseUpEvent;

        private void OnMouseDown()
        {
            onMouseDownEvent?.Invoke();
        }

        private void OnMouseExit()
        {
            onMouseDownExit?.Invoke();
        }

        private void OnMouseUp()
        {
            onMouseUpEvent?.Invoke();
        }
    }
}
