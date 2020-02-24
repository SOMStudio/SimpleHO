using Base;
using Base.Sound;
using Base.Utility;
using Game.SaveSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class MenuManager : BaseMenuManager
    {
        [Header("Level List")]
        [SerializeField] protected bool useLevelListWindow = false;

        private bool _cursorIsOverGameUi = false;
        
        [System.NonSerialized] public static MenuManager Instance;

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
            InitMenu();
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClickEscapeEvent();
            }
        }

        private void InitMenu()
        {
            
        }

        #region OverrideEvents
        protected override void ActivateWindowEvent()
        {
            base.ActivateWindowEvent();
            
            DisActivateGameInterface();
        }

        protected override void ChangeWindowEvent(int number)
        {
            base.ChangeWindowEvent(number);
            
            SoundManager.Instance?.PlaySoundByIndex(0, Vector3.zero);
        }

        protected override void DisActivateWindowEvent()
        {
            base.DisActivateWindowEvent();

            if (!IsMenuActive())
                ActivateGameInterface();
        }

        protected override void ActivateConsoleWEvent()
        {
            base.ActivateConsoleWEvent();
            
            DisActivateGameInterface();
        }

        protected override void ChangeConsoleWEvent(int number)
        {
            base.ChangeConsoleWEvent(number);
            
            SoundManager.Instance?.PlaySoundByIndex(0, Vector3.zero);
        }

        protected override void DisActivateConsoleWEvent()
        {
            base.DisActivateConsoleWEvent();
            
            if (!IsMenuActive())
                ActivateGameInterface();
        }

        public override void ConsoleWinMessage_ButtonOk()
        {
            base.ConsoleWinMessage_ButtonOk();
            
            SoundManager.Instance?.PlaySoundByIndex(1, Vector3.zero);
        }

        public override void ConsoleWinYesNo_ButtonNo()
        {
            base.ConsoleWinYesNo_ButtonNo();
            
            SoundManager.Instance?.PlaySoundByIndex(1, Vector3.zero);
        }

        public override void ConsoleWinYesNo_ButtonYes()
        {
            base.ConsoleWinYesNo_ButtonYes();
            
            SoundManager.Instance?.PlaySoundByIndex(1, Vector3.zero);
        }
        
        protected override void ActivateGameWEvent()
        {
            base.ActivateGameWEvent();
            
        }

        protected override void DisActivateGameWEvent()
        {
            base.DisActivateGameWEvent();

        }
        
        protected override void ExitGame ()
        {
            SoundManager.Instance?.PlaySoundByIndex(1, Vector3.zero);
            
            UserManager.Instance?.SavePrivateDataPlayer();

            base.ExitGame ();
        }
        #endregion

        //help function
        public bool IsMenuActive()
        {
            return WindowActive >= 0 || ConsoleWindowActive >= 0;
        }

        public bool IsCursorOverGameUi()
        {
            return _cursorIsOverGameUi;
        }

        public void SetOverUiState(bool value)
        {
            _cursorIsOverGameUi = value;
        }
        
        private void ClickEscapeEvent() {
            if (consoleWindowActive == -1) {
                if (windowActive == -1) {
                    ExitGameConsoleWindow_Button ();
                }
            }
        }

        // Console windows
        public void ShowMessageConsoleWindowOk(string value, UnityAction actionClick = null)
        {
            ConsoleWinMessage_SetTxt(value);
            if (actionClick != null)
            {
                ConsoleWinMessage_SetOkAction(actionClick);
            }

            ActivateConsoleWindow(8);
        }

        public void ShowMessageConsoleWindowYesNo(string value, UnityAction actionYes)
        {
            ConsoleWinYesNo_SetTxt (value);
            ConsoleWinYesNo_SetYesAction (actionYes);
            
            ActivateConsoleWindow (9);
        }

        public void ShowMessageConsoleWindowYesNo(string value, UnityAction actionYes, UnityAction actionNo)
        {
            ConsoleWinYesNo_SetTxt(value);
            ConsoleWinYesNo_SetYesAction(actionYes);
            ConsoleWinYesNo_SetNoAction(actionNo);

            ActivateConsoleWindow(9);
        }

        //button action
        public void RunLevel_Button()
        {
            if (useLevelListWindow)
            {
                ActivateWindow(2);
            }
            else
            {
                DisActivateWindow();

                GameController.Instance?.RunLevel();

                SoundManager.Instance?.PlaySoundByIndex(1, Vector3.zero);
            }
        }

        public void StopLevel_Button()
        {
            GameController.Instance?.StopLevel();
            
            ActivateWindow(0);
            
            SoundManager.Instance?.PlaySoundByIndex(1, Vector3.zero);
        }

        public void MainMenu_Button()
        {
            GameController.Instance?.PauseLevel();

            ShowMessageConsoleWindowYesNo("You want exit from game?", StopActiveLevel, ContinueActiveLevel);
            
            SoundManager.Instance?.PlaySoundByIndex(1, Vector3.zero);
        }

        private void StopActiveLevel()
        {
            if (GameController.Instance) {
                GameController.Instance.StopLevel();

                if (GameController.Instance.MenuAndLevelsDivided)
                    GameController.Instance?.StartScene("Menu");
            }
        }

        private void ContinueActiveLevel()
        {
            if (GameController.Instance)
            {
                int activeLevel = GameController.Instance.GameData.Data.ActiveLevel;

                GameController.Instance.RunLevel(activeLevel);
            }
        }

        public void ExitGameConsoleWindow_Button() {
            ShowMessageConsoleWindowYesNo ("Do you want to Exit?", ExitGame);
        }
    }
}