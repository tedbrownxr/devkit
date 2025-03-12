// DevKit
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
		
		public float ActiveTime => _state == InputState.Active ? Time.time - _startTime : 0;
		public ButtonEventType Event => _event;
		public InputState State => _state;

		protected float _startTime;
		protected ButtonEventType _event;
		protected InputState _state;

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
			_state = InputState.Inactive;
		}

		protected void Update ()
		{
			_event = ButtonEventType.None;

			switch (_state)
			{
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
					{
						if (Time.time - _startTime > HoldTime)
						{
							_event = ButtonEventType.Hold;
							_state = InputState.Active;
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
						_state = InputState.Inactive;
					}
					break;

				case InputState.Active:
					if (IsActive() == false)
					{
						_event = ButtonEventType.Release;
						_state = InputState.Inactive;
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
