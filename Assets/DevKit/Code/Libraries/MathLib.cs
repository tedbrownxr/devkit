using UnityEngine;

namespace DevKit
{
	public class MathLib 
	{
		/// <summary>Assumes capsule collider is aligned to Y-Axis. Height is cylinder height.</summary>
		public static bool CapsuleColliderContainsPoint (CapsuleCollider capsuleCollider, Vector3 point)
		{
			// Capsule Data
			Vector3 scale = capsuleCollider.transform.lossyScale;
			float radiusScaler = Mathf.Max(scale.x, scale.z);
			float radius = capsuleCollider.radius * radiusScaler;
			float height = (capsuleCollider.height * scale.y) - (radius * 2);
			if (height < radius * 2) 
			{
				height = 0;
			}

			// Point data
			Vector3 localPoint = capsuleCollider.transform.InverseTransformPoint(point);
			localPoint = Vector3.Scale(localPoint, scale);
			float localPointHypotenuse = Mathf.Sqrt((localPoint.x * localPoint.x) + (localPoint.z * localPoint.z));
			float absY = Mathf.Abs(localPoint.y);

//			Log.Message($"Radius: {radius.ToString("0.00")} / Hyp: {localPointHypotenuse.ToString("0.00")} / Height: {height.ToString("0.00")} / Y: {localPoint.y}");

			// in the cylinder
			if (absY < height / 2 && localPointHypotenuse < radius) 
			{
				return true;
			}

			// above the top or below the bottom
			if (absY > (height/2) + radius) 
			{
				return false;
			}

			// top sphere check
			Vector3 spherePoint = Vector3.up * height / 2;
			if (Vector3.Distance(spherePoint, localPoint) < radius)
			{
				return true;
			} 

			// bottom sphere check
			spherePoint = Vector3.down * height / 2;
			if (Vector3.Distance(spherePoint, localPoint) < radius) 
			{
				return true;
			}

			return false;
		}

		public static bool GetRayPlaneIntersection (Ray ray, Vector3 planeCenter, Vector3 planeNormal, out Vector3 position)
		{
			Plane plane = new Plane(planeNormal, planeCenter);
			
			// If the ray and plane are parallel, no intersection is possible
			if (Mathf.Approximately(Vector3.Dot(plane.normal, ray.direction), 0f))
			{
				position = Vector3.zero;
				return false;
			}

			var diff = ray.origin - planeCenter;
            var prod1 = Vector3.Dot(diff, plane.normal);
            var prod2 = Vector3.Dot(ray.direction, plane.normal);
            var prod3 = prod1 / prod2;
            position = ray.origin - ray.direction * prod3;

			return true;
		}

		public static float Remap (float value, float oldMin, float oldMax, float newMin, float newMax)
		{
			return newMin + (value - oldMin) * (newMax - newMin) / (oldMax - oldMin);
		}

	}
}
