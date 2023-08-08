using Base.Input.Interfaces;
using UnityEngine;

namespace Base.Input.InputImplementation
{
	public class RadialMouseInputHandler : IMouseInputHandler
	{
		public Vector2 GetRawPosition()
		{
			return UnityEngine.Input.mousePosition;
		}

		public Vector2 GetInput(Vector2 relativePosition)
		{
			Vector2 mousePos = GetRawPosition();
			Vector2 relativeMousePos = mousePos - relativePosition;
			float angle = Mathf.Atan2(relativeMousePos.y, relativeMousePos.x) * Mathf.Rad2Deg * -1;
			Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);
			return rot.eulerAngles;
		}
	}
}
