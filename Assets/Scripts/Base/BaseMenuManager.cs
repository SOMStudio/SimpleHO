using Base.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Base
{
	public class BaseMenuManager : MonoBehaviour
	{
		[Header("Menu Data")]
		[SerializeField] protected int windowActive = -1;
		[SerializeField] protected int consoleWindowActive = -1;
		[SerializeField] protected bool isGameInterfaceActivated = false;
		
		[Header("Menu window list")]
		[SerializeField] private WindowOpenClose[] windowActivator;

		[Header("Blocker windows")]
		[SerializeField] private WindowOpenClose windowBlocker;
		[SerializeField] private WindowOpenClose consoleWindowBlocker;

		[Header("Console windows")]
		[SerializeField] private TMP_Text consoleWinMessageTextHead;
		[SerializeField] private TMP_Text consoleWinYesNoTextHead;

		private readonly UnityEvent consoleWinMessageActinOk = new UnityEvent();
		private readonly UnityEvent consoleWinYesNoActinYes = new UnityEvent();
		private readonly UnityEvent consoleWinYesNoActinNo = new UnityEvent();

		[Header("Game Windows")]
		[SerializeField] private WindowOpenClose gameWindowHud;
		[SerializeField] private WindowOpenClose gameWindowAdvice;
		
		[SerializeField] private TMP_Text gameWindowAdviceText;
		[SerializeField] private int _countCharForDelayOneSecond = 10;

		protected virtual void ExitGame()
		{
		#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
		}

		#region Activators
		//activators
		private void WindowActivator_Open(int number)
		{
			if (number < windowActivator.Length)
			{
				WindowOpenClose activeA = windowActivator[number];

				if (activeA)
				{
					if (!activeA.IsOpen())
					{
						activeA.Open();
					}
				}
			}
		}

		private void WindowActivator_Close(int number)
		{
			if (number < windowActivator.Length)
			{
				WindowOpenClose activeA = windowActivator[number];

				if (activeA)
				{
					if (activeA.IsOpen())
					{
						activeA.Close();
					}
				}
			}
		}

		//blocker windows activator 
		private void WindowBlocker_Open()
		{
			if (windowBlocker)
			{
				windowBlocker.Open();
			}
		}

		private void WindowBlocker_Close()
		{
			if (windowBlocker)
			{
				windowBlocker.Close();
			}
		}

		//blocker console Windows activator
		private void ConsoleWindowBlocker_Open()
		{
			if (consoleWindowBlocker)
			{
				consoleWindowBlocker.Open();
			}
		}

		private void ConsoleWindowBlocker_Close()
		{
			if (consoleWindowBlocker)
			{
				consoleWindowBlocker.Close();
			}
		}
		#endregion

		#region Events
		protected virtual void ActivateWindowEvent()
		{

		}

		protected virtual void DisActivateWindowEvent()
		{

		}

		protected virtual void ChangeWindowEvent(int number)
		{

		}

		protected virtual void ActivateConsoleWEvent()
		{

		}

		protected virtual void DisActivateConsoleWEvent()
		{

		}

		protected virtual void ChangeConsoleWEvent(int number)
		{

		}
		
		protected virtual void ActivateGameWEvent()
		{

		}

		protected virtual void DisActivateGameWEvent()
		{

		}

		protected virtual void ChangeGameWEvent(bool isActive)
		{

		}

		protected virtual void ActivateAdviceGameWEvent()
		{
			
		}
		
		protected virtual void DisActivateAdviceGameWEvent()
		{
			
		}
		#endregion

		#region Windows
		public int WindowActive
		{
			get { return windowActive; }
		}

		public void ActivateWindow(int number)
		{
			if (windowActive == number)
			{
				DisActivateWindow();
			}
			else
			{
				if (windowActive > -1)
				{
					WindowActivator_Close(windowActive);
				}

				WindowActivator_Open(number);

				if (windowActive == -1)
				{
					WindowBlocker_Open();

					ActivateWindowEvent();
				}

				windowActive = number;

				ChangeWindowEvent(number);
			}
		}

		public void DisActivateWindow()
		{
			if (windowActive > -1)
			{
				WindowActivator_Close(windowActive);

				windowActive = -1;
				
				DisActivateWindowEvent();
			}

			WindowBlocker_Close();
		}
		#endregion

		#region ConsoleWindows
		public int ConsoleWindowActive
		{
			get { return consoleWindowActive; }
		}

		public void ActivateConsoleWindow(int number)
		{
			if (consoleWindowActive == number)
			{
				DisActivateConsoleWindow();
			}
			else
			{
				if (consoleWindowActive > -1)
				{
					WindowActivator_Close(consoleWindowActive);
				}

				WindowActivator_Open(number);

				if (consoleWindowActive == -1)
				{
					ConsoleWindowBlocker_Open();

					ActivateConsoleWEvent();
				}

				consoleWindowActive = number;

				ChangeConsoleWEvent(number);
			}
		}

		public void DisActivateConsoleWindow()
		{
			if (consoleWindowActive > -1)
			{
				WindowActivator_Close(consoleWindowActive);
				
				consoleWindowActive = -1;
				
				DisActivateConsoleWEvent();
			}
			
			ConsoleWindowBlocker_Close();
		}
		
		//console Message
		protected void ConsoleWinMessage_SetTxt(string val)
		{
			consoleWinMessageTextHead.text = TextHelp.SpecTextChar(val);
		}
		
		protected void ConsoleWinMessage_SetOkAction(UnityAction val)
		{
			consoleWinMessageActinOk.AddListener(val);
		}

		private void ConsoleWinMessage_ClearOkAction()
		{
			consoleWinMessageActinOk.RemoveAllListeners();
		}

		public virtual void ConsoleWinMessage_ButtonOk()
		{
			consoleWinMessageActinOk?.Invoke();
			
			DisActivateConsoleWindow();
			
			ConsoleWinMessage_ClearOkAction();
		}

		//console YesNo
		protected void ConsoleWinYesNo_SetTxt(string val)
		{
			consoleWinYesNoTextHead.text = TextHelp.SpecTextChar(val);
		}

		protected void ConsoleWinYesNo_SetYesAction(UnityAction val)
		{
			consoleWinYesNoActinYes.AddListener(val);
		}

		protected void ConsoleWinYesNo_SetNoAction(UnityAction val)
		{
			consoleWinYesNoActinNo.AddListener(val);
		}

		private void ConsoleWinYesNo_ClearYesNoAction()
		{
			consoleWinYesNoActinYes.RemoveAllListeners();
			consoleWinYesNoActinNo.RemoveAllListeners();
		}

		public virtual void ConsoleWinYesNo_ButtonYes()
		{
			consoleWinYesNoActinYes?.Invoke();

			DisActivateConsoleWindow();

			ConsoleWinYesNo_ClearYesNoAction();
		}

		public virtual void ConsoleWinYesNo_ButtonNo()
		{
			consoleWinYesNoActinNo?.Invoke();

			DisActivateConsoleWindow();

			ConsoleWinYesNo_ClearYesNoAction();
		}
		#endregion

		#region GameWindows
		public void ActivateGameInterface()
		{
			if (!isGameInterfaceActivated)
			{
				gameWindowHud.Open();

				ActivateGameWEvent();

				isGameInterfaceActivated = true;
				
				ChangeGameWEvent(isGameInterfaceActivated);
			}
		}

		public void DisActivateGameInterface()
		{
			if (isGameInterfaceActivated)
			{
				gameWindowHud.Close();

				isGameInterfaceActivated = false;
				
				ChangeGameWEvent(isGameInterfaceActivated);
				
				DisActivateGameWEvent();
			}
		}
		
		//game window Advice
		private void CloseAdviceGameWindow()
		{
			gameWindowAdvice.Close();

			DisActivateAdviceGameWEvent();
		}
		
		public void ShowAdviceGameWindow(string value)
		{
			gameWindowAdviceText.text = TextHelp.SpecTextChar(value);
            
			gameWindowAdvice.Open();

			if (IsInvoking(nameof(CloseAdviceGameWindow)))
			{
				CancelInvoke();
			}

			int delayTime = value.Length / _countCharForDelayOneSecond;
			delayTime = delayTime > 0 ? delayTime : 1;
            
			Invoke(nameof(CloseAdviceGameWindow), delayTime);

			ActivateAdviceGameWEvent();
		}
		#endregion
	}
}
