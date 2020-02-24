using Base;
using Base.SaveSystem;
using Base.SaveSystem.Interfaces;
using UnityEngine;

namespace Game
{
	public class UserManager : BaseUserManager
	{
		public static UserManager Instance { get; private set; }

		private ISaveSystem fileSaveSystem;

		private bool dataWasRead = false;
		private bool dataNeedWrite = false;

		private bool highScoreShowInLevel = false;
		
		void Awake()
		{
			if (!Instance)
			{
				Instance = this;
			}
			else
			{
				Destroy(this.gameObject);
			}

			string fileName = $"{Application.persistentDataPath}/playerData_{gamePrefsName}.dat";
			
			fileSaveSystem = new FileSaveSystem(fileName);
		}

		private void Start()
		{
			DontDestroyOnLoad(this.gameObject);

			if (GameController.Instance)
			{
				score.AddListener(CheckHighScore);
					
				health.AddListener(GameController.Instance.CheckLifePlayer);
			}
		}

		public void VisitLevel(int value)
		{
			if (dataWasRead)
			{
				if (GetLevel() < value)
				{
					SetLevel(value);

					dataNeedWrite = true;
				}

				ResetHighScoreShowFlag();
			}
			else
			{
				LoadPrivateDataPlayer();
			}
		}

		private void CheckHighScore(int value)
		{
			if (dataWasRead)
			{
				if (value > GetHighScore())
				{
					if (!highScoreShowInLevel)
					{
						highScoreShowInLevel = true;

						MenuManager.Instance?.ShowAdviceGameWindow("You improve Best Score!");
					}

					SetHighScore(GetScore(), true);

					dataNeedWrite = true;
				}
			}
			else
			{
				LoadPrivateDataPlayer();
			}
		}

		private void ResetHighScoreShowFlag()
		{
			highScoreShowInLevel = false;
		}

		/// <summary>
		/// save player data in file with encrypting, not use for Web-application (web can't write file)
		/// </summary>
		public void SavePrivateDataPlayer()
		{
			if (dataWasRead)
			{
				if (dataNeedWrite)
				{
					PlayerData data = new PlayerData();
					data.playerName = playerName;
					data.bestScore = GetHighScore();
					data.level = GetLevel();

					fileSaveSystem.Save(data);

					dataNeedWrite = false;
				}
			}
			else
			{
				LoadPrivateDataPlayer();
			}
		}

		/// <summary>
		/// restore player data from encrypting file.
		/// </summary>
		public void LoadPrivateDataPlayer()
		{
			if (!dataWasRead)
			{
				PlayerData data = new PlayerData();

				if (fileSaveSystem.Load(out data))
				{
					playerName = data.playerName;
					SetHighScore(data.bestScore);
					SetLevel(data.level);
				}
				else
				{
					GetDefaultData();
				}

				dataWasRead = true;
			}
		}
	}

	[System.Serializable]
	public class PlayerData
	{
		public string playerName;
		public int bestScore;
		public int level;
	}
}
