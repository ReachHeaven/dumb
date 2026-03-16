using Foundation;
using Foundation.Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Game.Core
{
    public class InputHandler : MonoBehaviour
    {
        [FormerlySerializedAs("_onSwipe")] [SerializeField]
        private DirectionGameEvent onSwipe;

        private Vector2 _touchStart;
        private bool _isTouching;
        private bool _isActive;

        private void Update()
        {
            if (!_isActive)
            {
                return;
            }

            Keyboard keyboard = Keyboard.current;

            if (keyboard != null)
            {
                if (keyboard.leftArrowKey.wasPressedThisFrame)
                {
                    HandleMove(Direction.Left);
                }
                else if (keyboard.rightArrowKey.wasPressedThisFrame)
                {
                    HandleMove(Direction.Right);
                }
                else if (keyboard.upArrowKey.wasPressedThisFrame)
                {
                    HandleMove(Direction.Up);
                }
                else if (keyboard.downArrowKey.wasPressedThisFrame)
                {
                    HandleMove(Direction.Down);
                }
            }

            Touchscreen touchscreen = Touchscreen.current;

            if (touchscreen != null)
            {
                if (touchscreen.primaryTouch.press.wasPressedThisFrame)
                {
                    _touchStart = touchscreen.primaryTouch.position.ReadValue();
                    _isTouching = true;
                }
                else if (_isTouching && touchscreen.primaryTouch.press.wasReleasedThisFrame)
                {
                    Vector2 touchEnd = touchscreen.primaryTouch.position.ReadValue();
                    HandleSwipe(touchEnd);
                    _isTouching = false;
                }
            }
        }

        private void HandleSwipe(Vector2 touchEnd)
        {
            Vector2 delta = (touchEnd - _touchStart).normalized;
            Direction direction = GetDirection(delta);
            HandleMove(direction);
        }

        private void HandleMove(Direction direction)
        {
            onSwipe.Raise(direction);
        }

        private Direction GetDirection(Vector2 delta)
        {
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                return delta.x > 0 ? Direction.Right : Direction.Left;
            }

            return delta.y > 0 ? Direction.Up : Direction.Down;
        }

        public void Disable()
        {
            _isActive = false;
        }

        public void Enable()
        {
            _isActive = true;
        }
    }
}
