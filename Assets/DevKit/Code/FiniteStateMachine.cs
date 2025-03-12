// DevKit
// Copyright (c) 2024 Ted Brown
// Author: Ted Brown <tedbrownxr@gmail.com>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit
{
	public class FiniteStateMachine : MonoBehaviour 
	{
		public enum OperationStep { Awake, Start }

		public string ActiveStateName => _activeState != null ? _activeState.StateName : "Null";

		public string PreviousStateName
		{
			get
			{
				int count = _stateHistory.Count;
				if (count > 0)
				{
					return _stateHistory[count - 1];
				}
				return _startState.name;
			}
		}

		[SerializeField] private FiniteState _startState;

		private bool _hasStarted;
		private Dictionary<string, FiniteState> _finiteStates;
		private FiniteState _activeState;
		private List<string> _stateHistory;
		private string _nextStateName = string.Empty;

		public void GoToStateAtEndOfUpdate (string stateName)
		{
			_nextStateName = stateName;
		}

		public bool GoToStateImmediately (string stateName)
		{
			// clear the state name that will be entered at the end of the frame
			_nextStateName = string.Empty;

			if (_activeState != null)
			{
				_stateHistory.Add(ActiveStateName);
				_activeState.Exit();
			}

			if (_finiteStates.TryGetValue(stateName, out _activeState))
			{
				_activeState.Enter();
				return true;
			}
			else
			{
				_activeState = null;
				return false;
			}
		}

		protected void Awake ()
		{
			_stateHistory = new List<string>();
			_finiteStates = new Dictionary<string, FiniteState>();
			FiniteState[] finiteStates = GetComponentsInChildren<FiniteState>();
			int errorCount = 0;

			foreach (FiniteState finiteState in finiteStates)
			{
				if (_finiteStates.ContainsKey(finiteState.StateName))
				{
					errorCount++;
					Debug.LogError($"Finite State Machine [{gameObject.name}]: Duplicate Finite State Name: {finiteState.StateName}. Objects: [{_finiteStates[finiteState.StateName].gameObject.name}] / [{finiteState.gameObject.name}]", gameObject);
					continue;
				}
				_finiteStates.Add(finiteState.StateName, finiteState);
			}

			if (_finiteStates.Count == 0)
			{
				Debug.LogError($"Finite State Machine [{gameObject.name}]: No states added.", gameObject);
				errorCount++;
			}

			if (errorCount > 0)
			{
				Debug.LogError($"Finite State Machine [{gameObject.name}] encountered {errorCount} errors.", gameObject);
				Debug.Break();
				return;
			}

			if (_startState == null)
			{
				Debug.LogWarning($"Finite State Machine [{gameObject.name}]: No start state defined. Using first in list.");
				_startState = finiteStates[0];
			}

			foreach (FiniteState finiteState in _finiteStates.Values)
			{
				finiteState.Initialize(this);
			}
		}

		protected void Update ()
		{
			if (_hasStarted == false)
			{
				// If it's a Meta Quest scene, confirm tracking has started by seeing if the camera is no longer at zero
				// if (FindObjectOfType<OVRBody>())
				// {
				// 	// This is an error that can happen when the connection to Link is corrupted.
				// 	if (Camera.main == null)
				// 	{
				// 		Debug.Log("no main camera");
				// 		return;
				// 		// Debug.LogError("System connection failure. Restart Unity, restart Link, and reconnect USB cable.");
				// 		// Debug.Break();
				// 		// return;
				// 	}
				// 	if (Vector3.Equals(Camera.main.transform.position, Vector3.zero))
				// 	{
				// 		return;
				// 	}
				// 	GoToStateImmediately(_startState.StateName);
				// 	_hasStarted = true;
				// }
				// This is not a Meta Quest scene. Go ahead and start.
				// else
				// {
					GoToStateImmediately(_startState.StateName);
					_hasStarted = true;
				// }
			}

			if (_activeState)
			{
				_activeState.Process();
			}

			if (string.Equals(_nextStateName, string.Empty) == false)
			{
				GoToStateImmediately(_nextStateName);
			}
		}
	}
}
