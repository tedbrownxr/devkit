using UnityEngine;

namespace DevKit
{
	public class RectCollider : MonoBehaviour 
	{
		private BoxCollider _boxCollider;
		private RectTransform _rectTransform;

		protected void Awake ()
		{
			_boxCollider = gameObject.GetComponent<BoxCollider>();
			if (_boxCollider == null)
			{
				_boxCollider = gameObject.AddComponent<BoxCollider>();
			}
			_rectTransform = GetComponent<RectTransform>();
			_boxCollider.size = new Vector3(_rectTransform.rect.width, _rectTransform.rect.height, 1);			
		}

		protected void Update ()
		{
			_boxCollider.size = new Vector3(_rectTransform.rect.width, _rectTransform.rect.height, 1);			
		}
	}
}
