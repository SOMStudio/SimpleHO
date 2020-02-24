using System;
using Game.GameData.DevScripts;
using Game.LevelListData.DevScripts;
using UnityEngine;
using System.Collections;
using Base.Coroutines;

namespace Game.GameLogic.DevScripts
{
    [Serializable]
    public class GameLogicData
    {
        [Header("Main")]
        [SerializeField] private GameDataTemplate gameData;
        [SerializeField] private LevelData levelData;
        [SerializeField] private LevelManager levelManager;

        public GameDataTemplate GameData
        {
            set => gameData = value;
        }

        public LevelData LevelData
        {
            set => levelData = value;
        }

        public LevelManager LevelManager
        {
            set => levelManager = value;
        }

        public void InitGameRules(GameDataTemplate gameDataSet)
        {
            GameData = gameDataSet;
        }
        
        public void InitLevelRules(LevelManager levelManager, LevelData levelDataNew = null)
        {
            LevelManager = levelManager;

            if (levelDataNew != null)
                LevelData = levelDataNew;

            CoroutineHelper.RunCoroutine(InitLevel, true);
        }

        private IEnumerator InitLevel()
        {
            yield return null;

            UserManager.Instance?.SetHealth(levelData.LifeLevel, true);
            UserManager.Instance?.SetScore(0, true);

            switch (levelData.TypeLevel)
            {
                case TypeLevel.StandartHO:
                    levelManager.MaskManager.HideMask();
                    break;
                case TypeLevel.NightHO:
                    levelManager.MaskManager.ShowMask();
                    break;
            }

            if (levelData.TimeLimit)
                levelManager.LevelStopWatch.Init(levelData.TimeForLimit);

            yield return null;

            levelManager.ItemsManager.InitItems();

            yield return null;

            int countItem = levelData.ItemsName.Length;
            for (int i = 0; i < countItem; i++)
            {
                levelManager.ItemsManager.SetNameItem(i, levelData.ItemsName[i]);
            }

            yield return null;
        }

        public void CatchGameReward()
        {
            UserManager.Instance?.AddScore(gameData.Data.BonusForItem);

            if (levelManager.ItemsManager.ItemsTaked())
            {
                levelManager.LevelStopWatch.Pause();
            } else
            {
                GameController.Instance?.CheckLocalTask(100);
            }
        }

        public void CheckLevelState()
        {
            if (levelManager.ItemsManager.ItemsTaked())
            {
                GameController.Instance?.CheckGlobalTask(100);
            }
        }
    }
}
