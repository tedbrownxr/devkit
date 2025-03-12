using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevKit.Menu
{
	public class History : MonoBehaviour
	{
		[SerializeField] protected GameObject _localCellPrefab;

		protected static History s_this;
		protected static Stack<HistoryCell> s_cells;

		public static void Add (string text)
		{
			HistoryCell cell = Instantiate(s_this._localCellPrefab, s_this.transform).GetComponent<HistoryCell>();
			cell.Show(text);
			s_cells.Push(cell);
		}

		public static void GoBack ()
		{
			if (s_cells.Count > 0)
			{
				s_cells.Peek().Dismiss();
				s_cells.Pop();
			}
		}

		protected void Awake ()
		{
			s_this = this;
			s_cells = new Stack<HistoryCell>();
			_localCellPrefab.gameObject.SetActive(false);
		}
	}
}
