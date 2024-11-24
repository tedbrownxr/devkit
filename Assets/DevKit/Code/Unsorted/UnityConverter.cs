// DevKit
// Copyright (c) 2024 Ted Brown

using UnityEngine;

namespace DevKit
{
	public class UnityConverter 
	{
		public static Vector3 FloatArrayToVector3 (float[] values)
		{
			return new Vector3(values[0], values[1], values[2]);
		}

		public static float[] Vector3ToFloatArray (Vector3 vector)
		{
			float[] floats = new float[]
			{
				vector.x,
				vector.y,
				vector.z
			};
			return floats;
		}		
	}
}
