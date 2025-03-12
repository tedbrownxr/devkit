using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Menu
{
	public class FocusManager : MonoBehaviour
	{
		protected static Image _backgroundImage;
		protected static GameObject _focusObject;
		protected static Transform _thisTransform;
		protected static Transform _previousTransform;
		
		public static void Focus (GameObject focusObject)
		{
			Unfocus();
			_thisTransform.SetAsLastSibling();
			_focusObject = focusObject;
			_previousTransform = _focusObject.transform.parent;
			_focusObject.transform.parent = _thisTransform;
			_backgroundImage.enabled = true;
		}

		public static void Unfocus ()
		{
			if (_focusObject != null)
			{
				_focusObject.transform.parent = _previousTransform;
				_focusObject = null;
				_previousTransform = null;
			}
			_backgroundImage.enabled = false;
		}

		protected void Awake ()
		{
			_thisTransform = transform;
			_backgroundImage = GetComponent<Image>();
			Unfocus();
		}
	}
}
