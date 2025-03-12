// DevKit
// Copyright (c) 2024 Ted Brown

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Menu
{
	/// <summary>
	/// This is designed to be opened as a new instance each time it is used.
	/// It destroys itself when closed.
	/// </summary>
	public class StringFieldModal : MonoBehaviour 
	{
		public Action OnCancel;
		public Action OnClose;
		public Action<string> OnConfirm;

		public ButtonElement CancelButton => _cancelButton;
		public ButtonElement ConfirmButton => _confirmButton;

		private static StringFieldModal s_instance; // we use this to confirm only one instance is open at a time

		[SerializeField] private ButtonElement _cancelButton;
		[SerializeField] private ButtonElement _confirmButton;
		[SerializeField] private TMP_InputField _inputField;
		[SerializeField] private TMP_Text _headerLabel;

		protected Action _onConfirm;

		public void Cancel ()
		{
			OnCancel?.Invoke();
			Close();
		}

		public void Close ()
		{
			OnClose?.Invoke();
			Destroy(gameObject);
		}

		public void Confirm ()
		{
			OnConfirm?.Invoke(_inputField.text);
			Close();
		}

		public void SetHeader (string text)
		{
			_headerLabel.text = text;
		}

		public void SetInputField (string text)
		{
			_inputField.text = text;
		}

		protected void Awake ()
		{
			if (s_instance != null)
			{
				s_instance.Close();
			}
			s_instance = this;
			_cancelButton.SetClickAction(Cancel);
			_confirmButton.SetClickAction(Confirm);
			WindowManager.IncreaseWindowCount();
		}

		protected void OnDestroy ()
		{
			WindowManager.DecreaseWindowCount();
		}
	}
}
