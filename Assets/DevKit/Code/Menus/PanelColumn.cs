using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Menu
{
	public class PanelColumn : MonoBehaviour 
	{
		public RectTransform RectTransform => _rectTransform;

		public Vector2 MaxElementSize => _maxElementSize;
		public Vector2 MinElementSize => _minElementSize;

		protected List<Element> _elements;
		protected RectTransform _rectTransform;
		protected Vector2 _maxElementSize = new Vector2(800, 60);
		protected Vector2 _minElementSize = new Vector2(400, 60);

		public static PanelColumn CreatePanelColumnObject (RectTransform parentTransform)
		{
			RectTransform panel = new GameObject("PanelColumn", typeof(RectTransform)).transform as RectTransform;
			panel.SetParent(parentTransform);
			panel.localScale = Vector3.one;
			// pivot top left
			panel.pivot = new Vector2(0, 1);
			// anchor top left
			panel.anchorMin = new Vector2(0, 1);
			panel.anchorMax = new Vector2(0, 1);
			panel.anchoredPosition = new Vector2(0, 0);
			return panel.gameObject.AddComponent<PanelColumn>();
		}

		public void Add (Element element)
		{

		}

		protected void Awake ()
		{
			_rectTransform = GetComponent<RectTransform>();
			_elements = new List<Element>();
		}
	}
}
