// Mountain Hikers
// Copyright (c) 2023 Ted Brown

using System;
using UnityEngine;

namespace DevKit
{
	public class PointerTarget : MonoBehaviour
	{
		public Action OnHover;
		public Action OnUnhover;

		public bool IsActive
		{
			get { return _isActive; }
		}

		public bool IsHovered
		{
			get { return _isHovered; }
		}

		private bool _isActive;
		private bool _isHovered;

		public void Activate ()
		{
			_isActive = true;
		}

		public void Deactivate ()
		{
			_isActive = false;
		}

		public void Hover ()
		{
			_isHovered = true;
			OnHover?.Invoke();
		}

		public void Unhover ()
		{
			_isHovered = false;
			OnUnhover?.Invoke();
		}
	}
}
