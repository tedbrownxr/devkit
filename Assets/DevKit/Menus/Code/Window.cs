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
	public class Window : MonoBehaviour 
	{
		public List<Row> Rows;
		public WindowManager WindowManager => _windowManager;
		public RectTransform RectTransform => _rectTransform;

		protected List<RadioButtonElement> _radioButtons;
		protected WindowManager _windowManager;
		protected RectTransform _rectTransform;
		
		// https://stackoverflow.com/questions/79126/create-generic-method-constraining-t-to-an-enum
		public EnumField AddEnumField<T> (string label, int startingValue, Action<int> onChange) where T : Enum
		{
			Type enumType = typeof(T);
			if (enumType.IsEnum == false)
			{
				Log.Message("Nope: " + enumType);
				return null;
			}

			var row = CreateRow();
			EnumField enumField = row.gameObject.AddComponent<EnumField>();
			enumField.Initialize<T>(this, row, label, startingValue, onChange);

			return enumField;
		}

		public Row AddLabelRow (string label)
		{
			var labelElement = CreateLabelElement(label);
			var row = CreateRow(labelElement);
			return row;
		}

		public ButtonElement CreateButtonElementWithAction (string label, Action clickAction)
		{
			ButtonElement element = Instantiate(_windowManager.ButtonPrefab, transform).GetComponent<ButtonElement>();
			element.SetLabel(label);
			element.SetClickAction(clickAction);
			return element;
		}

		public ButtonElement CreateButtonElementWithNoAction (string label)
		{
			ButtonElement element = Instantiate(_windowManager.ButtonPrefab, transform).GetComponent<ButtonElement>();
			element.SetLabel(label);
			return element;
		}


		public Element CreateLabelElement (string label)
		{
			Element element = Instantiate(_windowManager.LabelPrefab, transform).GetComponent<Element>();
			element.SetLabel(label);
			return element;
		}

		public RadioButtonElement CreateRadioButtonElement (string label)
		{
			RadioButtonElement element = Instantiate(_windowManager.RadioButtonPrefab, transform).GetComponent<RadioButtonElement>();
			element.gameObject.name = label;
			element.SetLabel(label);
			element.SetSelected(false);
			return element;
		}

		public void FixRowsToBeUniformHeight ()
		{
			float height = 0;
			foreach (Row row in Rows)
			{
				height = Mathf.Max(height, row.GetHeight());
			}
			foreach (Row row in Rows)
			{
				row.SetHeight(height);
			}
		}

		public void FixColumnToBeUniformWidth (int columnIndex)
		{
			float width = 0;
			List<Element> targetElements = new List<Element>();
			foreach (Row row in Rows)
			{
				if (columnIndex < row.Elements.Count && row.Elements[columnIndex] != null)
				{
					width = Mathf.Max(width, row.Elements[columnIndex].Width);
					targetElements.Add(row.Elements[columnIndex]);
				}
			}

			foreach (Element element in targetElements)
			{
				element.SetMinWidth(width);
			}

			foreach (Row row in Rows)
			{
				row.UpdateLayout(_windowManager.ElementSpacingX);
			}
		}

		public float GetWidth ()
		{
			float width = 0;
			foreach (Row row in Rows)
			{
				width = Mathf.Max(width, row.GetWidth());
			}
			return width;
		}

		public void Initialize (WindowManager panelManager, string name)
		{
			_windowManager = panelManager;
			gameObject.name = name;
		}

		public void SortRows ()
		{
			float y = 0;
			foreach (Row row in Rows)
			{
				row.RectTransform.anchoredPosition = new Vector2(0, y);
				y -= row.GetHeight();
				y -= _windowManager.RowPaddingY;
			}
		}

		public Row CreateRow (params Element[] elements)
		{
			float y = 0;
			foreach (Row existingRow in Rows)
			{
				y -= existingRow.GetHeight();
				y -= _windowManager.RowPaddingY;
			}

			GameObject rowObject = new GameObject("Row", typeof(RectTransform));
			RectTransform rowTransform = rowObject.GetComponent<RectTransform>();
			rowTransform.SetParent(_rectTransform);
			rowTransform.localScale = Vector3.one;
			Row row = rowObject.AddComponent<Row>();
			foreach (Element element in elements)
			{
				row.AddElement(element);
			}
			row.RectTransform.anchoredPosition = new Vector2(0, y);
			Rows.Add(row);
			return row;
		}

		protected void Awake ()
		{
			_rectTransform = GetComponent<RectTransform>();

			// anchor preset: upper left
			_rectTransform.anchorMin = new Vector2(0, 1f);
			_rectTransform.anchorMax = new Vector2(0, 1f);
			_rectTransform.pivot = new Vector2(0, 1f);

			_rectTransform.anchoredPosition = Vector2.zero;

			Rows = new List<Row>();

			WindowManager.IncreaseWindowCount();
		}

		protected void OnDestroy ()
		{
			WindowManager.DecreaseWindowCount();
		}
	}


}
