using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit
{
	public class CameraPointer : Pointer 
	{
		[SerializeField] private Vector2 _viewportOffset;
		private Camera _camera;

		protected override Ray GetRay ()
		{
			Vector2 viewportPoint = Vector2.one * 0.5f + _viewportOffset;
			return _camera.ViewportPointToRay(viewportPoint);
		}
	}
}
