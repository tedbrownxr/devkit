// DevKit
// Copyright (c) 2024 Ted Brown

using System;
using UnityEngine;

namespace DevKit
{
	public class PointerButton : MonoBehaviour 
	{
		public Action OnClick;
		public Action OnHover;
		public Action OnUnhover;

		[SerializeField] private PointerTarget _pointerTarget;

		public static float TapTime = 0.2f;
		public static float HoldTime = 0.3f;
		private static GameObject _lockInteractionsToObject;
		
<<<<<<< HEAD
		public float ActiveTime => _state == InputState.Active ? Time.time - _startTime : 0;
=======
		public float ActiveTime => _state == ButtonState.Pressed ? Time.time - _startTime : 0;
>>>>>>> main
		public ButtonEventType Event => _event;
		public InputState State => _state;


		protected float _startTime;
		protected ButtonEventType _event;
		protected InputState _state;

		// if this is locked to a gameobject, 
		// events will only be broadcast to objects that have this object as a parent in their hierarchy.
		public static void LockToObject (GameObject targetObject)
		{
			_lockInteractionsToObject = targetObject;
		}

		public static void Unlock ()
		{
			_lockInteractionsToObject = null;
		}

		public void Activate ()
		{
			_state = ButtonState.Idle;
			_pointerTarget.Activate();
		}

		public void Deactivate ()
		{
			_pointerTarget.Deactivate();
			_state = ButtonState.Deactivated;
		}

		public virtual bool GetPrimaryButton ()
		{
			return Input.GetMouseButton(0);
		}

		protected void Awake ()
		{
<<<<<<< HEAD
			_state = InputState.Inactive;
=======
			_state = ButtonState.Hovered;
>>>>>>> main
		}

		protected void Update ()
		{
			_event = ButtonEventType.None;

			switch (_state)
			{
<<<<<<< HEAD
				case InputState.Inactive:
					if (IsActive())
					{
						_event = ButtonEventType.Start;
						_state = InputState.ActivatedThisFrame;
						_startTime = Time.time;
					}
					break;

				case InputState.DeactivatedThisFrame:
					if (IsActive())
=======
				case ButtonState.None:
					_state = ButtonState.Idle;
					break;

				case ButtonState.Idle:
					if (_pointerTarget.IsHovered)
					{
						_state = ButtonState.Hovered;
						OnHover?.Invoke();
					}
					break;

				case ButtonState.Hovered:
					if (_pointerTarget.IsHovered)
>>>>>>> main
					{
						if (GetPrimaryButton())
						{
<<<<<<< HEAD
							_event = ButtonEventType.Hold;
							_state = InputState.Active;
=======
							_event = ButtonEventType.Start;
							_state = ButtonState.RecentlyPressed;
							_startTime = Time.time;
>>>>>>> main
						}
					}
					else
					{
<<<<<<< HEAD
						if (Time.time - _startTime < TapTime)
						{
							_event = ButtonEventType.Tap;
						}
						else
						{
							_event = ButtonEventType.Release;
						}
						_state = InputState.Inactive;
					}
					break;

				case InputState.Active:
					if (IsActive() == false)
					{
						_event = ButtonEventType.Release;
						_state = InputState.Inactive;
=======
						_state = ButtonState.Idle;
						OnUnhover?.Invoke();
					}
					break;

				case ButtonState.RecentlyPressed:
					if (_pointerTarget.IsHovered)
					{
						if (GetPrimaryButton())
						{
							if (Time.time - _startTime > HoldTime)
							{
								_event = ButtonEventType.Hold;
								_state = ButtonState.Pressed;
							}
						}
						else
						{
							if (Time.time - _startTime < TapTime)
							{
								_event = ButtonEventType.Tap;
							}
							else
							{
								_event = ButtonEventType.Release;
							}
							_state = ButtonState.Hovered;
						}
					}
					else
					{
						_state = ButtonState.Idle;
						OnUnhover?.Invoke();
					}
					break;

				case ButtonState.Pressed:
					if (_pointerTarget.IsHovered)
					{
						if (GetPrimaryButton() == false)
						{
							_event = ButtonEventType.Release;
							_state = ButtonState.Hovered;
						}
					}
					else
					{
						_event = ButtonEventType.Release;
						_state = ButtonState.Idle;
						OnUnhover?.Invoke();
>>>>>>> main
					}
					break;
			}
			
			if (_event == ButtonEventType.Tap)
			{
				if (_lockInteractionsToObject != null)
				{
					bool isAcceptedInteraction = false;
					Transform parent = transform;
					foreach (Transform t in parent)
					{
						// reached root
						if (t == t.parent) break;
						if (t.gameObject == _lockInteractionsToObject)
						{
							isAcceptedInteraction = true;
							break;
						}
					}
					if (isAcceptedInteraction == false)
					{
						return;
					}
				}
				OnClick?.Invoke();
			}
		}
	}
}
