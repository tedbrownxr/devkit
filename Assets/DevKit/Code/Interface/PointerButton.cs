// Zephyr
// Copyright (c) 2024 Ted Brown

using System;
using UnityEngine;

namespace DevKit
{
	public class PointerButton : MonoBehaviour 
	{
		public Action OnTap;
		
		[SerializeField] private PointerTarget _pointerTarget;

		public static float TapTime = 0.2f;
		public static float HoldTime = 0.3f;
		
		public float ActiveTime => _state == ButtonState.Active ? Time.time - _startTime : 0;
		public ButtonEventType Event => _event;
		public ButtonState State => _state;

		protected float _startTime;
		protected ButtonEventType _event;
		protected ButtonState _state;

		private static GameObject _lockInteractionsToObject;

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

		public bool IsActive ()
		{
			return Input.GetMouseButton(0) && _pointerTarget.IsHovered;
		}

		protected void Awake ()
		{
			_state = ButtonState.Inactive;
		}

		protected void Update ()
		{
			_event = ButtonEventType.None;

			switch (_state)
			{
				case ButtonState.None:
					_state = ButtonState.Inactive;
					break;

				case ButtonState.Inactive:
					if (IsActive())
					{
						_event = ButtonEventType.Start;
						_state = ButtonState.RecentlyActive;
						_startTime = Time.time;
					}
					break;

				case ButtonState.RecentlyActive:
					if (IsActive())
					{
						if (Time.time - _startTime > HoldTime)
						{
							_event = ButtonEventType.Hold;
							_state = ButtonState.Active;
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
						_state = ButtonState.Inactive;
					}
					break;

				case ButtonState.Active:
					if (IsActive() == false)
					{
						_event = ButtonEventType.Release;
						_state = ButtonState.Inactive;
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
				OnTap?.Invoke();
			}
		}
	}
}
