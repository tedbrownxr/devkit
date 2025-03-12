using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Menu
{
	public class OldPanel : MonoBehaviour
	{
		public static float AnimationDuration = 0.42f;

		public enum Location { Header, Content, Footer }

		public string MenuName;
		// only used for some legacy menus that exist in scene
		public bool DestroyOnClose = true;
		public float Width = 600;

		protected CanvasGroup _canvasGroup;
		protected Coroutine _moveRoutine;
		protected OldPanelManager _panelManager;
		protected List<RectTransform> _headerRows;
		protected List<RectTransform> _contentRows;
		protected List<RectTransform> _footerRows;
		protected RectTransform _rectTransform;
		protected Vector2 _basePosition;
		protected Vector2 _rightPosition;
		protected Vector2 _leftPosition;

		/// <summary>Dismiss menu completely after going back in history</summary>
		public virtual void Close ()
		{
			OnBeforeClose();
			if (_moveRoutine != null)
			{
				StopCoroutine(_moveRoutine);
			}
			StartCoroutine(Move(0, 1, 
				() => 
				{ 
					if (DestroyOnClose) 
						Destroy(gameObject); 
					else 
						gameObject.SetActive(false); 
				}));
		}

		public virtual string GetName ()
		{
			if (string.IsNullOrEmpty(MenuName) == false)
			{
				return MenuName.ToString();
			}
			return gameObject.name;
		}

		/// <summary>Move menu back in the stack to history</summary>
		public virtual void Hide ()
		{
			StartCoroutine(Move(0, -1, 
				() => { gameObject.SetActive(false); }));
		}

		/// <summary>Open a new menu instance</summary>
		public virtual void OpenAsNew ()
		{
			Refresh();
			gameObject.SetActive(true);
			StartCoroutine(Move(1, 0));

			// if (obj == null)
			// {
			// 	obj = new Tactician.ModifyEffectTemplate();
			// 	var dynamicButton = Instantiate(_panelManager.DynamicButtonPrefab, transform).GetComponent<DynamicButton>();
			// 	dynamicButton.Initialize(obj.GetType().ToString(), () => { _panelManager.OpenModifyEffectTemplate(obj); } );
			// }
		}

		public virtual void Refresh ()
		{
		}

		public void SetBasePosition (Vector2 position)
		{
			_basePosition = position;
			_rightPosition = _basePosition + Vector2.right * Width;
			_leftPosition = _basePosition - Vector2.right * Width;
		}

		/// <summary>Re-open a menu instance from history</summary>
		public virtual void ShowAsExisting ()
		{
			Refresh();
			gameObject.SetActive(true);
			StartCoroutine(Move(-1, 0));
		}

		protected GameObject AddBackButton ()
		{
			GameObject obj = AddPrefab(_panelManager.BackButtonPrefab, Location.Footer);
			obj.name = "Back Button";
			return obj;
		}

		protected GameObject AddDeleteObjectButton (System.Action onClick, string text = "Delete")
		{
			GameObject obj = AddPrefab(_panelManager.DynamicButtonPrefab, Location.Footer);
			obj.GetComponent<DynamicButton>().Initialize("[X] " + text, onClick);
			obj.name = "Delete Button";
			return obj;		
		}

		protected DynamicButton AddDynamicButton (string label, System.Action clickAction, Location location = Location.Content)
		{
			var button = AddPrefab(_panelManager.DynamicButtonPrefab, location).GetComponent<DynamicButton>();
			button.Initialize(label, clickAction);
			return button;
		}

		protected GameObject AddNewObjectButton (System.Action onClick, string text = "Add New")
		{
			GameObject obj = AddPrefab(_panelManager.DynamicButtonPrefab, Location.Footer);
			obj.GetComponent<DynamicButton>().Initialize("[+] " + text, onClick);
			obj.name = "Add New Object Button";
			return obj;
		}

		protected GameObject AddPrefab (GameObject prefab, Location location = Location.Content)
		{
			GameObject newObject = Instantiate(prefab, transform);
			RectTransform rectTransform = newObject.GetComponent<RectTransform>();

			switch (location)
			{
				case Location.Content:
					_contentRows.Add(rectTransform);
					break;
				case Location.Footer:
					_footerRows.Add(rectTransform);
					break;
				case Location.Header:
					_headerRows.Add(rectTransform);
					break;
				default:
					Log.Error($"Location.{location} has not been implemented");
					break;
			}

			RefreshRowPositions();

			return newObject;
		}

		// protected ToStringElement AddToStringElement (string text, Location location = Location.Content)
		// {
		// 	ToStringElement element = AddPrefab(_panelManager.ToStringElementPrefab, location).GetComponent<ToStringElement>();
		// 	element.SetText(text);
		// 	return element;
		// }

		// variable parameters are a tactician concept
		// protected VariableParameterField AddVariableParameterField (string name, VariableParameter variableParameter)
		// {
		// 	VariableParameterField field = AddPrefab(_panelManager.VariableParameterFieldPrefab).GetComponent<VariableParameterField>();
		// 	field.Initialize(name, variableParameter);
		// 	return field;
		// }

		protected IEnumerator Move (int startOffset, int endOffset, System.Action finalAction = null)
		{
			// if endOffset is 0, it is arriving on screen
			Vector2 startPosition = _basePosition + Vector2.right * startOffset * Width;
			Vector2 endPosition = _basePosition + Vector2.right * endOffset * Width;

			// if the end offset is zero, then it is arriving on screen and should be full alpha
			float startAlpha = endOffset == 0 ? 0 : 1;
			float endAlpha = endOffset == 0 ? 1 : 0;

			_canvasGroup.interactable = false;

			float timer = 0;
			float duration = AnimationDuration;
			while (timer < duration)
			{
				timer += Time.deltaTime;
				float t = _panelManager.PanelMotionCurve.Evaluate(Mathf.Clamp01(timer / duration));
				_rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
				_canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
				yield return null;
			}
			_rectTransform.anchoredPosition = endPosition;
			_canvasGroup.alpha = endAlpha;

			// if the end offset is zero, it has arrived
			_canvasGroup.interactable = endOffset == 0;

			finalAction?.Invoke();
		}

		protected virtual void OnAfterAwake()
		{

		}

		protected virtual void OnBeforeClose ()
		{

		}

		protected void RefreshRowPositions ()
		{
			List<RectTransform> allRows = new List<RectTransform>();
			allRows.AddRange(_headerRows);
			allRows.AddRange(_contentRows);
			allRows.AddRange(_footerRows);

			// Remove all null values from the list.
			// For this to work properly, deleted rows must use DestroyImmediate
			allRows.RemoveAll(obj => obj == null);

			// Remove all disabled game objects from the list
			allRows.RemoveAll(obj => obj.gameObject.activeSelf == false);

			int y = 0;
			foreach (RectTransform rectTransform in allRows)
			{
				rectTransform.anchoredPosition = new Vector2(0, y);
				y -= PanelSettings.ObjectHeight;

				// if (rectTransform.GetComponent<ToStringElement>())
				// {
				// 	y -= 10;
				// }
			}
		}

		protected void Awake ()
		{
			_headerRows = new List<RectTransform>();
			_contentRows = new List<RectTransform>();
			_footerRows = new List<RectTransform>();
			_panelManager = GetComponentInParent<OldPanelManager>();
			_canvasGroup = gameObject.AddComponent<CanvasGroup>();
			_rectTransform = GetComponent<RectTransform>();

			// NOTE: This assumes the object has been placed by PanelManager.CreatePanelObject
			SetBasePosition(_rectTransform.anchoredPosition);
			OnAfterAwake();
		}
	}
}
