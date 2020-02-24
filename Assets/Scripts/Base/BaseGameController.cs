using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base
{
	[AddComponentMenu("Base/GameController")]
	
	public class BaseGameController : MonoBehaviour
	{
		[Header("Base")]
		[SerializeField] protected GameObject explosionPrefab;

		[Header("Level")]
		[SerializeField] protected bool menuAndLevelsDivided = false;
		[SerializeField] protected BaseLevelManager levelManager;
		
		private bool _paused;

		private Camera _mainCamera;

		public bool MenuAndLevelsDivided => menuAndLevelsDivided;

		public Camera MainCamera
		{
			get
			{
				if (_mainCamera == null) _mainCamera = Camera.main;
				return _mainCamera;
			}
		}
		
		public void Explode(Vector3 aPosition)
		{
			if (explosionPrefab)
			{
				Instantiate(explosionPrefab, aPosition, Quaternion.identity);
			}
		}

		public GameObject GetPlayer(int value = 0)
		{
			return levelManager?.GetPlayer(value);
		}

		public GameObject GetEnemy(int value = 0)
		{
			return levelManager?.GetEnemy(value);
		}
		
		public virtual void PlayerDestroyed()
		{

		}

		public virtual void EnemyDestroyed()
		{

		}

		public virtual void BossDestroyed()
		{

		}

		public virtual void StartScene(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}

		public virtual void RestartScene()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		public void SetLevelManager(BaseLevelManager value)
		{
			levelManager = value;
		}

		public virtual void RunLevel(int number = 0)
		{
			if (menuAndLevelsDivided)
				StartScene("Level" + number);
			else
				levelManager?.RunLevel(number);
		}

		public virtual void PauseLevel()
		{
			levelManager?.PauseLevel();
		}
		
		public virtual void StopLevel()
		{
			levelManager?.StopLevel();
		}

		public bool Paused
		{
			get { return _paused; }
			set
			{
				_paused = value;

				if (_paused)
				{
					Time.timeScale = 0f;
				}
				else
				{
					Time.timeScale = 1f;
				}
			}
		}
	}
}
