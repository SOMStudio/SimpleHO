using System;
using UnityEngine;
using UnityEngine.Events;

namespace Base.TimeControl
{
	public class StopWatch : MonoBehaviour
	{
		[Header("Main")]
		[SerializeField] private float time = 10.0f;
		[SerializeField] private float stepUpdate = 1.0f;
		[SerializeField] private string formatTime = "MM:SS";
	
		private float _currentTime = 0.0f;
		private float _prevUpdate = 0.0f;
	
		[Header("Events")]
		public UpdateEvent eventUpdate;
		public UnityEvent eventComplete;
	
		private bool _run = false;
		private bool _endTime = true;
	
		private void Update() {
			if (_run) {
				if (!_endTime) {
					_currentTime -= Time.deltaTime;
				
					if (_prevUpdate - _currentTime >= stepUpdate) {
						_prevUpdate -= stepUpdate;

						string timeSt = TimeHelp.GetFormattedTime(_prevUpdate, formatTime);
						eventUpdate?.Invoke(timeSt);
					}
				
					if (_currentTime <= 0.0f) {
						_endTime = true;
						_run = false;
					
						eventComplete?.Invoke();
					}
				}
			}
		}

		public void Init(float initTime, float initStepUpdate = 0.0f)
		{
			time = initTime;
			_currentTime = time;
			_prevUpdate = time;

			if (initStepUpdate > 0.0f)
				stepUpdate = initStepUpdate;

			string timeSt = GetFormattedTime();
			eventUpdate?.Invoke(timeSt);
		}

		public void Run(float initTime = 0.0f, float initStepUpdate = 0.0f) {
			if (_endTime) {
				if (initTime > 0.0f) {
					time = initTime;
				
					if (initStepUpdate > 0.0f) {
						stepUpdate = initStepUpdate;
					}
				}
				
				_currentTime = time;
				_prevUpdate = time;
				_endTime = false;
			}
		
			if (!_run) {
				Continue();
			}
		}

		public void Restart(float initTime = 0.0f, float initStepUpdate = 0.0f)
		{
			_run = false;
			_endTime = true;

			Run(initTime, initStepUpdate);
		}

		public void Pause() {
			_run = false;
		}
	
		public void Continue() {
			_run = true;
		}

		public float GetTime()
		{
			return _currentTime;
		}
		
		/// <summary>
		/// GetTime in format (default MM:SS:MS)
		/// </summary>
		/// <returns>The time string</returns>
		public string GetFormattedTime()
		{
			return TimeHelp.GetFormattedTime(_currentTime, formatTime);
		}
	
		[Serializable]
		public class UpdateEvent : UnityEvent<string>{}
	}
}
