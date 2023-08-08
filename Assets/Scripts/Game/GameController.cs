using Base;
using Game.GameData.DevScripts;
using Game.GameLogic.DevScripts;
using Game.LevelListData.DevScripts;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class GameController : BaseGameController
    {
        [Header("Data")]
        [SerializeField] private GameDataTemplate gameData;
        [SerializeField] private GameLogicDataTemplate gameLogicData;
        [SerializeField] private LevelListDataTemplate levelListData;

        public GameDataTemplate GameData => gameData;
        public GameLogicDataTemplate GameLogicData => gameLogicData;
        public LevelListDataTemplate LevelListData => levelListData;

        [System.NonSerialized] public static GameController Instance;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            InitGame();
        }

        private void InitGame()
        {
            DontDestroyOnLoad(this.gameObject);

            gameLogicData.Data.InitGameRules(gameData);
            GameData.Data.ActiveLevel = -1;
        }

        #region OverrideMethods
        public override void PlayerDestroyed()
        {
            base.PlayerDestroyed();

            StopLevel();
        }

        public override void EnemyDestroyed()
        {
            base.EnemyDestroyed();


        }

        public override void BossDestroyed()
        {
            base.BossDestroyed();

            StopLevel();
        }

        public override void RunLevel(int number = 0)
        {
            if (GameData.Data.ActiveLevel == number)
            {
                levelManager?.RunLevel();
            }
            else
            {
                string nameScene = LevelListData.Data[number].NameScene;
                StartScene(nameScene);

                GameData.Data.ActiveLevel = number;

                UserManager.Instance?.VisitLevel(number);
            }
        }

        public override void StopLevel()
        {
            base.StopLevel();

            GameData.Data.ActiveLevel = -1;
        }
        #endregion

        public void CheckLifePlayer(int life)
        {
            if (life == 0)
            {
                UnityAction action;
                if (menuAndLevelsDivided)
                    action = () => StartScene("Menu");
                else
                    action = () => MenuManager.Instance?.ActivateWindow(0);

                MenuManager.Instance?.ShowMessageConsoleWindowOk("Game over!", action);

                PlayerDestroyed();
            }
        }

        public void CheckLocalTask(int percentage)
        {
            if (percentage == 100)
            {
                MenuManager.Instance?.ShowAdviceGameWindow("You so cool!");

                EnemyDestroyed();
            }
        }

        public void CheckGlobalTask(int percentage)
        {
            if (percentage == 100)
            {
                Invoke(nameof(WinMessage), gameData.Data.DelayForWinMessage);

                BossDestroyed();
            }
        }

        private void WinMessage()
        {
            UnityAction action;
            if (menuAndLevelsDivided)
                action = () => StartScene("Menu");
            else
                action = () => MenuManager.Instance?.ActivateWindow(0);

            MenuManager.Instance?.ShowMessageConsoleWindowOk("You win!", action);
        }

        public void PauseGame()
        {
            Paused = !Paused;
        }
    }
}
