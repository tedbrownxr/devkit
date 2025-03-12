using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Menu
{
	public class EnumField : MonoBehaviour
	{
		public bool HasChanged => _initialValue != _currentValue;
		public bool IsOptionListVisible => _optionsWindow.gameObject.activeInHierarchy;

		public Action<int> OnValueChange;

		protected Window _parentPanel;
		protected Window _optionsWindow;
		protected Row _row;
		protected Element _labelElement;
		protected ButtonElement _buttonElement;

		protected int _initialValue;
		protected int _currentValue;
		protected Type _enumType;
		protected Dictionary<int, RadioButtonElement> _radioButtons;

		// for later use, possibly in a pivot to more generics
		public static T EnumCast<T>(int value) where T : Enum
		{
			return (T)Enum.ToObject(typeof(T), value);
		}

		// this is called by EnumValueButton
		public void ChangeValue (int value)
		{
			_currentValue = value;
			_buttonElement.SetLabel(System.Enum.GetName(_enumType, _currentValue));
			foreach (KeyValuePair<int, RadioButtonElement> pair in _radioButtons)
			{
				pair.Value.SetSelected(pair.Key == value);
			}
			HideOptionsPanel();
			OnValueChange?.Invoke(_currentValue);
		}

		public void HideOptionsPanel ()
		{
			_optionsWindow.gameObject.SetActive(false);
			// FocusManager.Unfocus();
		}

		// https://stackoverflow.com/questions/79126/create-generic-method-constraining-t-to-an-enum
		public void Initialize<T> (Window panel, Row row, string fieldName, int initialValue, Action<int> onValueChange) where T : Enum
		{
			_parentPanel = panel;
			_row = row;
			_initialValue = initialValue;
			_currentValue = initialValue;
			OnValueChange += onValueChange;
			_enumType = typeof(T);

			_labelElement = panel.CreateLabelElement(fieldName);
			_row.AddElement(_labelElement);

			string valueName = EnumCast<T>(initialValue).ToString();
			_buttonElement = panel.CreateButtonElementWithAction(valueName, () => { ToggleOptionsPanel(); } );
			_row.AddElement(_buttonElement);

			_radioButtons = new Dictionary<int, RadioButtonElement>();
			_optionsWindow = WindowManager.CreateChildWindow(_enumType.ToString(), _buttonElement);
			foreach (int i in System.Enum.GetValues(_enumType))
			{
				var enumButton = _optionsWindow.CreateRadioButtonElement(System.Enum.GetName(_enumType, i));
				enumButton.SetClickAction( () => { ChangeValue(i); } );
				enumButton.SetSelected(i == initialValue);
				_optionsWindow.CreateRow(enumButton);
				_radioButtons.Add(i, enumButton);
			}
			_optionsWindow.SortRows();
			_optionsWindow.FixRowsToBeUniformHeight();
			_optionsWindow.FixColumnToBeUniformWidth(0);

			// _valueButtonObjects = new List<GameObject>();
			// foreach (int i in System.Enum.GetValues(_enumType))
			// {
			// 	EnumValueButton valueButton = Instantiate(_enumValueButtonPrefab, _optionsTransform).GetComponent<EnumValueButton>();
			// 	valueButton.Initialize(this, i, System.Enum.GetName(_enumType, i));
			// 	_valueButtonObjects.Add(valueButton.gameObject);
			// }

			// // Add a cancel button
			// OldPanelManager panelManager = GetComponentInParent<OldPanelManager>();
			// DynamicButton dynamicButton = Instantiate(panelManager.DynamicButtonPrefab, _optionsTransform).GetComponent<DynamicButton>();
			// dynamicButton.Initialize("[X] Cancel", () => Hide());
			// _valueButtonObjects.Add(dynamicButton.gameObject);

			HideOptionsPanel();
		}

		public void ShowOptionsPanel ()
		{
			_optionsWindow.gameObject.SetActive(true);
			_optionsWindow.RectTransform.anchoredPosition = new Vector2(_buttonElement.Width + _parentPanel.WindowManager.ElementSpacingX, 0);
			// FocusManager.Focus(gameObject);
		}

		public void ToggleOptionsPanel ()
		{
			if (_optionsWindow.gameObject.activeInHierarchy)
			{
				HideOptionsPanel();
			}
			else
			{
				ShowOptionsPanel();
			}
		}

		protected void HandleClick ()
		{
			if (IsOptionListVisible)
			{
				HideOptionsPanel();
			}
			else
			{
				ShowOptionsPanel();
			}
		}
	}
}
