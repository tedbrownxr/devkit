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
	public abstract class FiniteState : MonoBehaviour 
	{
		public abstract string StateName { get; }

		protected FiniteStateMachine _fsm;

		public virtual void Initialize (FiniteStateMachine fsm)
		{
			_fsm = fsm;
			OnAfterInitialize();
			gameObject.SetActive(false);
		}

		public virtual void Enter ()
		{
			gameObject.SetActive(true);
			OnAfterEnter();
		}

		public virtual void Process ()
		{
			OnProcess();
		}

		public virtual void Exit ()
		{
			OnBeforeExit();
			gameObject.SetActive(false);
		}

		protected virtual void OnAfterInitialize () {}
		protected virtual void OnAfterEnter () {}
		protected virtual void OnProcess () {}
		protected virtual void OnBeforeExit () {}
	}
}
