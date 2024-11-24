using UnityEngine;

namespace DevKit
{
	// https://easings.net/
	public class TweenLib
	{
		public static float EaseOutBounce (float x)
		{
			const float n1 = 7.5625f;
			const float d1 = 2.75f;

			if (x < 1 / d1) 
			{
				return n1 * x * x;
			} 
			else if (x < 2 / d1) 
			{
				return n1 * (x -= 1.5f / d1) * x + 0.75f;
			} 
			else if (x < 2.5 / d1) 
			{
				return n1 * (x -= 2.25f / d1) * x + 0.9375f;
			} 
			else 
			{
				return n1 * (x -= 2.625f / d1) * x + 0.984375f;
			}
		}

		public static float EaseOutCirc (float x)
		{
			return Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
		}

		public static float EaseOutCubic (float x)
		{
			return 1 - Mathf.Pow(1 - x, 3);
		}
	}
}
