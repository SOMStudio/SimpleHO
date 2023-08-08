using System;
using System.Collections;
using UnityEngine;

namespace Base.Coroutines
{
	public class CoroutineHelper
	{
		public static void RunCoroutine(Func<IEnumerator> coroutine, bool selfDestruct = true)
		{
			GameObject runner = new GameObject("CoroutineRunner");
			CoroutineInstance coroutineInstance = runner.AddComponent<CoroutineInstance>();
			coroutineInstance.StartCoroutine(coroutineInstance.Run(coroutine, selfDestruct));
		}
	}
}