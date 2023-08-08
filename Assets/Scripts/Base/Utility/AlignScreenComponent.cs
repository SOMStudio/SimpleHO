using System;
using UnityEngine;

namespace Base.Utility
{
	[ExecuteInEditMode]
	public class AlignScreenComponent : MonoBehaviour
	{
		[Header("Main")]
		public AlignScreenPosition align = AlignScreenPosition.Left;
		public Vector3 shiftPosition = Vector3.zero;

		private Camera _mainCamera;
		private Transform _myTransform;

		[ExecuteInEditMode]
		private void Start()
		{
			if (!_mainCamera)
			{
				_mainCamera = Camera.main;
			}

			if (!_myTransform)
			{
				_myTransform = transform;
			}

			UpdatePosition();
		}

		private void UpdatePosition()
		{
			Vector3 alignVector = Vector3.zero;

			float positionCameraZ = _mainCamera.transform.position.z;

			if ((align & AlignScreenPosition.Left) == AlignScreenPosition.Left)
			{
				alignVector +=
					new Vector3(_mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, positionCameraZ)).x, 0, 0);
			}
			else if ((align & AlignScreenPosition.Right) == AlignScreenPosition.Right)
			{
				alignVector += new Vector3(_mainCamera.ScreenToWorldPoint(new Vector3(0, 0, positionCameraZ)).x, 0, 0);
			}

			if ((align & AlignScreenPosition.Up) == AlignScreenPosition.Up)
			{
				alignVector += new Vector3(0, _mainCamera.ScreenToWorldPoint(new Vector3(0, 0, positionCameraZ)).y, 0);
			}
			else if ((align & AlignScreenPosition.Down) == AlignScreenPosition.Down)
			{
				alignVector += new Vector3(0,
					_mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, positionCameraZ)).y, 0);
			}

			_myTransform.position = alignVector + shiftPosition;
		}
	}

	[Flags]
	public enum AlignScreenPosition : byte
	{
		Left = 1,
		Right = 2,
		Up = 4,
		Down = 16,
	}
}