using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Menu
{
	/// An element is the base level "box" for menu items.
	/// Each has a left icon, text, and right icon.
	// There should be a child image object set to match size
	public class Element : MonoBehaviour 
	{
		public readonly float PaddingX = 16;
		public readonly float PaddingY = 8;

		public virtual float Height => _rectTransform.sizeDelta.y;
		public virtual float Width => _rectTransform.sizeDelta.x;

		public RectTransform RectTransform => _rectTransform;

		[SerializeField] protected Image _backgroundImage;
		[SerializeField] protected TMP_Text _label;
		[SerializeField] protected Image _optionalLeftIcon;
		[SerializeField] protected Image _optionalRightIcon;

		protected float _minHeight;
		protected float _minWidth;
		protected RectTransform _rectTransform;

		public virtual float GetContentHeight ()
		{
			// Use the height of the tallest element plus padding on top and bottom
			float iconLeftHeight = _optionalLeftIcon == null ? 0 : _optionalLeftIcon.rectTransform.sizeDelta.y;
			float iconRightHeight = _optionalRightIcon == null ? 0 : _optionalRightIcon.rectTransform.sizeDelta.y;
			float labelHeight = _label == null ? 0 : _label.preferredHeight;
			float height = Mathf.Max(iconLeftHeight, iconRightHeight, labelHeight) + (PaddingY * 2);
			return height;
		}

		public virtual float GetContentWidth ()
		{
			float width = PaddingX;
			width += _optionalLeftIcon == null ? 0 : _optionalLeftIcon.rectTransform.sizeDelta.x + PaddingX;
			width += _label == null ? 0 : _label.preferredWidth + PaddingX;
			width += _optionalRightIcon == null ? 0 : _optionalRightIcon.rectTransform.sizeDelta.x + PaddingX;
			return width;
		}

		public void SetLabel (string value)
		{
			_label.text = value;
			UpdateSize();
		}

		public void SetMinHeight (float value)
		{
			_minHeight = value;
			UpdateSize();
		}

		public void SetMinWidth (float value)
		{
			_minWidth = value;
			UpdateSize();
		}

		public void UpdateSize ()
		{
			float height = Mathf.Max(_minHeight, GetContentHeight());
			float width = Mathf.Max(_minWidth, GetContentWidth());
			float x = PaddingX;
			if (_optionalLeftIcon != null)
			{
				_optionalLeftIcon.rectTransform.anchoredPosition = new Vector2(x, 0);
				x += _optionalLeftIcon.rectTransform.sizeDelta.x + PaddingX;
			}
			if (_label != null)
			{
				_label.rectTransform.anchoredPosition = new Vector2(x, 0);
			}
			if (_optionalRightIcon != null)
			{
				_optionalLeftIcon.rectTransform.anchoredPosition = new Vector2(width - PaddingX - _optionalLeftIcon.rectTransform.anchoredPosition.x, 0);
			}
			_rectTransform.sizeDelta = new Vector2(width, height);
		}

		protected virtual void OnAfterAwake ()
		{
		}

		protected void Awake ()
		{
			_rectTransform = GetComponent<RectTransform>();
			_rectTransform.localScale = Vector3.one;

			// transform anchor preset: upper left
			_rectTransform.anchorMin = new Vector2(0, 1f);
			_rectTransform.anchorMax = new Vector2(0, 1f);
			_rectTransform.pivot = new Vector2(0, 1f);			
			_rectTransform.anchoredPosition = Vector2.zero;

			// label anchor preset: middle left
			if (_label != null)
			{
				_label.rectTransform.anchorMin = new Vector2(0, 0.5f);
				_label.rectTransform.anchorMax = new Vector2(0, 0.5f);
				_label.rectTransform.pivot = new Vector2(0, 0.5f);
				_label.horizontalAlignment = HorizontalAlignmentOptions.Left;
				_label.verticalAlignment = VerticalAlignmentOptions.Middle;
				_label.enableWordWrapping = false;
				_label.overflowMode = TextOverflowModes.Overflow;
				_label.raycastTarget = false;
			}

			// left icon preset: middle left
			if (_optionalLeftIcon != null)
			{
				_optionalLeftIcon.rectTransform.anchorMin = new Vector2(0, 0.5f);
				_optionalLeftIcon.rectTransform.anchorMax = new Vector2(0, 0.5f);
				_optionalLeftIcon.rectTransform.pivot = new Vector2(0, 0.5f);
			}

			// right icon preset: middle left
			if (_optionalRightIcon != null)
			{
				_optionalRightIcon.rectTransform.anchorMin = new Vector2(0, 0.5f);
				_optionalRightIcon.rectTransform.anchorMax = new Vector2(0, 0.5f);
				_optionalRightIcon.rectTransform.pivot = new Vector2(0, 0.5f);
			}

			UpdateSize();

			OnAfterAwake();
		}
	}
}
