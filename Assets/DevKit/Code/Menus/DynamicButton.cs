using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Menu
{
	public class DynamicButton : PanelElement
	{
		[SerializeField] protected Button _button;
		[SerializeField] protected GameObject _arrowIndicator;

		protected Action _dynamicAction;

		public void Initialize (string label, Action dynamicAction, bool showArrowIndicator = false)
		{
			_label.text = label;
			_dynamicAction = dynamicAction;
			_arrowIndicator?.SetActive(showArrowIndicator);
			_button.onClick.AddListener(HandleClick);
			DecreaseTextSizeToFit();
		}

		public void SetWidth (int width)
		{
			_rectTransform.sizeDelta = new Vector2(width, _rectTransform.sizeDelta.y);
			DecreaseTextSizeToFit();
		}

		protected void HandleClick ()
		{
			_dynamicAction?.Invoke();
		}

		protected void DecreaseTextSizeToFit ()
		{
			float maxWidth = _rectTransform.sizeDelta.x - 20;
			while (_label.preferredWidth > maxWidth && _label.fontSize > 8)
			{
				_label.fontSize--;
			}
		}
	}
}
