using UnityEngine;

namespace DevKit
{
	// NOTE: To get BlockedByCanvas to work, add CanvasPointerCheck to all Canvas elements in your scene.
	public class Pointer : MonoBehaviour 
	{
		public bool HasTarget => _pointerTarget != null;
		public PointerTarget Target => _pointerTarget;
		public Quaternion Rotation => Quaternion.LookRotation(GetRay().direction, Vector3.up);
		public Vector3 HitNormal => _hitNormal;
		public Vector3 HitPoint => _hitPoint;
		public Vector3 Origin => GetRay().origin;

		[SerializeField] protected LayerMask _layerMask = -1;

		protected int _lastUpdatedFrame;
		protected PointerTarget _pointerTarget;
		protected Ray _ray;
		protected Vector3 _hitNormal;
		protected Vector3 _hitPoint;

		protected virtual Ray GetRay ()
		{
			if (Time.frameCount == _lastUpdatedFrame)
			{
				return _ray;
			}
			_lastUpdatedFrame = Time.frameCount;
			_ray = new Ray(transform.position, transform.forward);
			return _ray;
		}

		protected virtual PointerTarget GetPointerTarget ()
		{
			Ray ray = GetRay();
			if (Physics.Raycast(ray, out RaycastHit hit, 1000, _layerMask, QueryTriggerInteraction.Ignore))
			{
				_hitPoint = hit.point;
				_hitPoint = hit.normal;
				PointerTarget target = hit.collider.gameObject.GetComponentInParent<PointerTarget>();
				if (target.IsActive)
				{
					return target;
				}
			}
			return null; 
		}

		protected virtual void Update ()
		{
			PointerTarget previousTarget = _pointerTarget;
			PointerTarget currentTarget = GetPointerTarget();

			// if the current target is the same as the previous target, only do something if the target was disabled
			if (currentTarget == previousTarget)
			{
				if (currentTarget != null)
				{
					if (currentTarget.IsActive == false)
					{
						currentTarget.Unhover();
						currentTarget = null;
					}
				}
			}
			// If we have a new target, unhover the old and hover the new
			else if (currentTarget != previousTarget)
			{
				if (previousTarget != null)
				{
					previousTarget.Unhover();
				}
				// Here we assume GetPointerTarget checks if the target is enabled or not
				// before it returns one. Otherwise, it should return a null value.
				if (currentTarget != null)
				{
					currentTarget.Hover();
				}
			}

			_pointerTarget = currentTarget;
		}
	}
}
