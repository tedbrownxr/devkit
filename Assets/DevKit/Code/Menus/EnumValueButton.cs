using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Menu
{
	public class EnumValueButton : MonoBehaviour
	{
		[SerializeField] protected Button _button;
		[SerializeField] protected TMP_Text _label;

		protected EnumField _enumField;
		protected int _value;

		public void Initialize (EnumField enumField, int value, string text)
		{
			_enumField = enumField;
			_value = value;
			_label.text = text;
			_button.onClick.AddListener(HandleClick);
		}

		protected void HandleClick ()
		{
			_enumField?.ChangeValue(_value);
		}
	}
}
