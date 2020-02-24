using UnityEngine;
using UnityEngine.UI;

namespace Base.UI.StopWatch
{
    public class UiStopWatchManager : MonoBehaviour
    {
        [SerializeField] private Text textTime;

        public void UpdateTime(string value)
        {
            textTime.text = value;
        }
    }
}
