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
	// Run before other things so its singleton is established 
	// before the default Awake call.
	[DefaultExecutionOrder(-100)]
	public class WindowManager : MonoBehaviour 
	{
		public static bool IsWindowOpen => s_windowCount > 0;
		private static int s_windowCount;
		private static WindowManager s_instance;

		public readonly float RowPaddingY = 4;
		public readonly float ElementSpacingX = 4;

		public GameObject ButtonPrefab => _buttonPrefab;
		public GameObject LabelPrefab => _labelPrefab;
		public GameObject RadioButtonPrefab => _radioButtonPrefab;

		[Header("Scene References")]
		[SerializeField] private Canvas _canvas;

		[Header("Project References")]
		[SerializeField] private GameObject _buttonPrefab;
		[SerializeField] private GameObject _labelPrefab;
		[SerializeField] private GameObject _radioButtonPrefab;
		[SerializeField] private GameObject _stringFieldModalPrefab;
		[SerializeField] private GameObject _windowPrefab;



		public static Window CreateWindow (string name)
		{
			GameObject panelObject = new GameObject("Window", typeof(RectTransform));
			panelObject.GetComponent<RectTransform>().SetParent(s_instance._canvas.GetComponent<RectTransform>());
			panelObject.GetComponent<RectTransform>().localScale = Vector3.one;
			Window panel = panelObject.AddComponent<Window>();
			panel.Initialize(s_instance, name);
			return panel;
		}

		public static Window CreateChildWindow (string name, Element parent)
		{
			GameObject panelObject = new GameObject("Window", typeof(RectTransform));
			RectTransform panelTransform = panelObject.GetComponent<RectTransform>();
			Window panel = panelObject.AddComponent<Window>();
			panel.Initialize(s_instance, name);
			panelTransform.SetParent(parent.RectTransform);
			panelTransform.localScale = Vector3.one;
			panelTransform.anchoredPosition = new Vector2(parent.Width + s_instance.ElementSpacingX, 0);
			return panel;			
		}

		public static StringFieldModal CreateStringFieldModal ()
		{
			GameObject obj = Instantiate(s_instance._stringFieldModalPrefab, s_instance._canvas.GetComponent<RectTransform>());
			StringFieldModal modal = obj.GetComponent<StringFieldModal>();
			obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			return modal;
		}

		public static void DecreaseWindowCount ()
		{
			s_windowCount--;
			if (s_windowCount < 0) s_windowCount = 0;
		}

		public static void IncreaseWindowCount ()
		{
			s_windowCount++;
		}

		protected void Awake ()
		{
			s_instance = this;
		}
	}
}
