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
	public class ButtonElement : Element
	{
		protected Action _clickAction;
		protected Button _button;

		public void SetClickAction (Action clickAction)
		{
			_clickAction = clickAction;
		}

		protected void HandleClick ()
		{
			if (_clickAction != null)
			{
				_clickAction?.Invoke();
			}
			else
			{
				Log.Warning("No click action for this button: " + _label.text, gameObject);
			}
		}

		protected override void OnAfterAwake()
		{
			_button = GetComponentInChildren<Button>();
			_button.onClick.AddListener(HandleClick);

			// _label = GetComponentInChildren<TMP_Text>();
			// _label.raycastTarget = false;
			// // anchor preset: middle left
			// _label.rectTransform.anchorMin = new Vector2(0, 0.5f);
			// _label.rectTransform.anchorMax = new Vector2(0, 0.5f);
			// _label.rectTransform.pivot = new Vector2(0, 0.5f);
			// _label.horizontalAlignment = HorizontalAlignmentOptions.Left;
			// _label.verticalAlignment = VerticalAlignmentOptions.Middle;
			// _label.enableWordWrapping = false;
			// _label.overflowMode = TextOverflowModes.Overflow;
			// SetLabel(_label.text);
		}		
	}
}
