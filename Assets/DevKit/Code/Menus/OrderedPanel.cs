using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Menu
{
	public class OrderedPanel : MonoBehaviour 
	{
		public GameObject LabelElementPrefab;

		protected List<PanelColumn> _columns;
		protected RectTransform _rectTransform;

		public void AddLabel (string value, int column = 0)
		{
			Element label = Instantiate(LabelElementPrefab, _rectTransform).GetComponent<Element>();
			label.SetLabel(value);
			_columns[column].Add(label);
		}

		protected void Awake ()
		{
			RectTransform rectTransform = GetComponent<RectTransform>();
			_columns = new List<PanelColumn>();
			PanelColumn c0 = PanelColumn.CreatePanelColumnObject(rectTransform);
			c0.name = "Column 0";
			_columns.Add(c0);

			PanelColumn c1 = PanelColumn.CreatePanelColumnObject(rectTransform);
			c1.name = "Column 1";
			_columns.Add(c1);
		}

		protected void Start ()
		{
			TMP_Text text = GetComponentInChildren<TMP_Text>();
			_rectTransform = GetComponent<RectTransform>();
			_rectTransform.sizeDelta = new Vector2(text.preferredWidth + 40, 40);
			text.GetComponent<RectTransform>().sizeDelta = new Vector2(text.preferredWidth + 40, 40);
			text.GetComponent<RectTransform>().anchoredPosition = new Vector2(16, 0);
		}
	}
}
