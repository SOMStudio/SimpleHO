using Base.Sound;
using Base.Utility;
using DG.Tweening;
using PathCreation;
using UnityEngine;

namespace Game
{
    public class ItemsManager : MonoBehaviour
    {
        [Header("Main")]
        [SerializeField] private ItemManager[] items;
        [SerializeField] private ParticleManager[] particles;
        [SerializeField] private PathCreator[] pathes;

        private LevelManager levelManager;

        private bool _control;

        private void Start()
        {
            if (!levelManager)
                levelManager = LevelManager.Instance;
        }

        public void InitItems()
        {
            foreach (var item in items)
            {
                HideItem(item);
            }
        }

        public void TakeItem(ItemManager value)
        {
            int index = GetIndexItem(value);
            float timeMove = 1.0f;

            if (GameController.Instance)
                timeMove = GameController.Instance.GameData.Data.DelayForWinMessage;

            particles[index].gameObject
                .DOMovePath(pathes[index], timeMove)
                .SetEase(Ease.Linear)
                .OnStart(() => StartMoveItem(index))
                .OnComplete(() => CompleteMoveItem(index));

            levelManager.CompleteTask();
            levelManager.CheckTasks();
        }

        public bool ItemsTaked()
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (!items[i].IsItemTake)
                {
                    return false;
                }
            }

            return true;
        }

        private int GetIndexItem(ItemManager value)
        {
            int index = -1;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == value)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        private void StartMoveItem(int index)
        {
            particles[index].Play();

            SoundManager.Instance?.PlaySoundByIndex(3, Vector3.zero);
        }

        private void CompleteMoveItem(int index)
        {
            ShowItem(items[index]);

            particles[index].Stop();

            SoundManager.Instance?.PlaySoundByIndex(4, Vector3.zero);
        }

        private void HideItem(ItemManager value)
        {
            value.HideItemObject();

            value.ShowClickArea();
        }

        private void ShowItem(ItemManager value)
        {
            value.HideItemShadow();
            value.HideClickArea();

            value.ShowItemObject();
        }

        public void SetNameItem(int index, string setName)
        {
            items[index].SetItemText(setName);
        }

        public bool IsControl()
        {
            return _control;
        }

        public void StartControl()
        {
            _control = true;
        }

        public void StopControl()
        {
            _control = false;
        }
    }
}
