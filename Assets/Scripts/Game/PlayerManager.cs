using Base.Input;
using Base.Input.Interfaces;
using Base.Input.Samples;
using Base.Player.Interfaces;
using Base.Sound;
using Base.Utility;
using UnityEngine;

namespace Game
{
    public class PlayerManager : ExtendedCustomMonoBehaviour, IDestructible
    {
        [Header("Main")]
        [SerializeField] private Transform maskView;

        private bool _control;
        private bool _followCursor;

        private IInputManager _inputManager;

        public int Id => id;

        private void Update()
        {
            CheckForInput();
        }

        protected override void Init()
        {
            base.Init();

            SetId(myGO.GetHashCode());

            _inputManager = new InputManager(new SampleBindings());
            _inputManager.AddActionToBindingKeyDown("shoot", FollowCursor);
            _inputManager.AddActionToBindingKeyUp("shoot", NotFollowCursor);
        }

        private void CheckForInput()
        {
            if (!MenuManager.Instance) return;
            if (MenuManager.Instance.IsMenuActive()) return;
            if (MenuManager.Instance.IsCursorOverGameUi()) return;

            if (_control)
            {
                _inputManager.CheckForInput();

                if (_followCursor)
                {
                    Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    maskView.position = new Vector3(newPosition.x, newPosition.y, maskView.position.z);
                }
            }
        }

        private void FollowCursor()
        {
            _followCursor = true;
        }

        private void NotFollowCursor()
        {
            _followCursor = false;
        }

        public void StartControl()
        {
            _control = true;
        }

        public void StopControl()
        {
            _control = false;
        }

        public void Damage(int value)
        {
            UserManager.Instance?.ReduceHealth(value);

            SoundManager.Instance?.PlaySoundByIndex(5, myTransform.position);
        }
    }
}

