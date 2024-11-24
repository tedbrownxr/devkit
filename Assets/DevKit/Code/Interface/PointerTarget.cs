// DevKit
// Copyright (c) 2024 Ted Brown

using System;
using UnityEngine;

namespace DevKit
{
	public class PointerTarget : MonoBehaviour
	{
		public Action OnHover;
		public Action OnUnhover;

		public bool IsActive => _isActive;
		public bool IsHovered => _isHovered;
		public Vector3 HitNormal => _hitNormal;
		public Vector3 HitPoint => _hitPoint;

		[SerializeField] private bool _activateOnStart = true;

		private bool _isActive;
		private bool _isHovered;
		private Vector3 _hitNormal;
		private Vector3 _hitPoint;

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
			if (_isActive && _isHovered == false)
			{
				_isHovered = true;
				OnHover?.Invoke();
			}
		}

		public void SetPointerHitPoint (Vector3 hitPoint, Vector3 hitNormal)
		{
			_hitNormal = hitNormal;
			_hitPoint = hitPoint;
		}

		public void Unhover ()
		{
			OnUnhover?.Invoke();
			_isHovered = false;
		}

		protected void Start ()
		{
			if (_activateOnStart)
			{
				Activate();
			}
		}
	}
}
