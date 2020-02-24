using Base;
using Base.TimeControl;
using Game.GameData.DevScripts;
using Game.GameLogic.DevScripts;
using Game.LevelListData.DevScripts;
using UnityEngine;

namespace Game
{
    public class LevelManager : BaseLevelManager
    {   
        [Header("Player Manager")]
        [SerializeField] private PlayerManager playerManager;

        [Header("Items Manager")]
        [SerializeField] private ItemsManager itemsManager;

        [Header("Mask Manager")]
        [SerializeField] private MaskManager maskManager;
		
		[Header("Stop Watch")]
		[SerializeField] private StopWatch levelStopWatch;

        private GameDataTemplate _gameData;
        private GameLogicDataTemplate _gameLogicData;
        private LevelListDataTemplate _levelListData;

        private LevelData _levelData;

        private bool _startLevel = false;
        private bool _pauseLevel = false;

        [System.NonSerialized] public static LevelManager Instance;

        public PlayerManager PlayerManager => playerManager;
        public ItemsManager ItemsManager => itemsManager;
        public MaskManager MaskManager => maskManager;
        public StopWatch LevelStopWatch => levelStopWatch;

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
            InitLevel();
        }

        private void InitLevel()
        {
            if (GameController.Instance)
            {
                var gameController = GameController.Instance;

                gameController.SetLevelManager(this);

                _gameData = gameController.GameData;
                _gameLogicData = gameController.GameLogicData;
                _levelListData = gameController.LevelListData;

                if (gameController.MenuAndLevelsDivided)
                    RunLevel(_gameData.Data.ActiveLevel);
            }
        }
        
        #region OverrideMethods
        public override void RunLevel(int number = 0)
        {
            if (number >= 0)
            {
                if (!_startLevel)
                {
                    _startLevel = !_startLevel;

                    ActivateLevel(number);
                }
                else
                {
                    if (_pauseLevel)
                    {
                        _pauseLevel = !_pauseLevel;

                        itemsManager.StartControl();
                        playerManager.StartControl();
                        levelStopWatch.Continue();
                    }
                }
            }
        }

        public override void PauseLevel()
        {
            if (_startLevel)
            {
                if (!_pauseLevel)
                {
                    _pauseLevel = !_pauseLevel;

                    itemsManager.StopControl();
                    playerManager.StopControl();
                    levelStopWatch.Pause();
                }
            }
        }

        public override void StopLevel()
        {
            _startLevel = false;

            itemsManager.StopControl();
            playerManager.StopControl();
            levelStopWatch.Pause();
        }

        public override GameObject GetPlayer(int number = 0)
        {
            return playerManager.gameObject;
        }

        public override GameObject GetEnemy(int number = 0)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        private void ActivateLevel(int number)
        {
            _levelData = _levelListData.Data[number];
            _gameLogicData.Data.InitLevelRules(this, _levelData);

            string stRules = _levelData.GetStringTask();
            MenuManager.Instance?.ShowMessageConsoleWindowOk(stRules, StartLevel);
        }

        private void StartLevel()
        {
            itemsManager.StartControl();
            playerManager.StartControl();
            levelStopWatch.Run();
        }

        public void CompleteTask()
        {
            _gameLogicData.Data.CatchGameReward();
        }

        public void CheckTasks()
        {
            _gameLogicData.Data.CheckLevelState();
        }
    }

    public enum TypeLevel
    {
        StandartHO,
        NightHO
    }
}
