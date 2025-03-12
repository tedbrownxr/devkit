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
	public class Row : MonoBehaviour 
	{
		public RectTransform RectTransform => _rectTransform;
		public List<Element> Elements;

		protected RectTransform _rectTransform;

		public void AddElement (Element element)
		{
			float x = 0;
			foreach (Element existingElement in Elements)
			{
				x += existingElement.Width;
			}
			Elements.Add(element);
			element.RectTransform.SetParent(_rectTransform);
			element.RectTransform.localScale = Vector3.one;
			element.RectTransform.anchoredPosition = new Vector2(x, 0);
		}

		public float GetHeight ()
		{
			float height = 0;
			foreach (Element element in Elements)
			{
				height = Mathf.Max(height, element.Height);
			}
			return height;
		}

		public float GetWidth ()
		{
			float width = 0;
			foreach (Element element in Elements)
			{
				width += element.Width;
			}
			return width;
		}

		public void SetHeight (float value)
		{
			foreach (Element element in Elements)
			{
				element.SetMinHeight(value);
			}
		}

		public void UpdateLayout (float elementSpacingX)
		{
			float x = 0;
			foreach (Element element in Elements)
			{
				element.RectTransform.anchoredPosition = new Vector2(x, 0);
				x += element.Width + elementSpacingX;
			}
		}

		protected void Awake ()
		{
			_rectTransform = GetComponent<RectTransform>();

			// anchor preset: upper left
			_rectTransform.anchorMin = new Vector2(0, 1f);
			_rectTransform.anchorMax = new Vector2(0, 1f);
			_rectTransform.pivot = new Vector2(0, 1f);

			_rectTransform.anchoredPosition = Vector2.zero;

			Elements = new List<Element>();
		}

	}
}
