using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Menu
{
	public class PanelElement : MonoBehaviour
	{
		protected OldPanelManager PanelManager
		{
			get
			{
				if (_panelManager == null)
				{
					_panelManager = GetComponentInParent<OldPanelManager>();
				}
				return _panelManager;
			}
		}
			
		[SerializeField] protected RectTransform _rectTransform;
		[SerializeField] protected TMP_Text _label;

		protected OldPanelManager _panelManager;

		protected virtual void OnAfterAwake ()
		{
		}

		protected void Awake ()
		{
			_rectTransform.sizeDelta = new Vector2(PanelSettings.ObjectWidth, PanelSettings.ObjectHeight);
			OnAfterAwake();
		}
	}
}
