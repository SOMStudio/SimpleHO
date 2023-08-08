using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
	[SerializeField] private float destroyTime;
	
	private void Start()
	{
		Destroy(gameObject, destroyTime);
	}
}
