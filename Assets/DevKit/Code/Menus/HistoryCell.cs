using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DevKit.Menu
{
	public class HistoryCell : MonoBehaviour
	{
		[SerializeField] protected TMP_Text _label;

		public void Dismiss ()
		{
			Destroy(gameObject);
		}

		public void Show (string text)
		{
			gameObject.SetActive(true);
			_label.text = text;
		}
	}
}
