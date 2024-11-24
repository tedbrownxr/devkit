using UnityEngine;

namespace DevKit
{
	public class RingBufferVector3 
	{
		public bool IsFull => _currentLength == _values.Length;
		public int Size => _values.Length;

		private int _currentIndex;
		private int _currentLength;
		private Vector3[] _values;

		public RingBufferVector3 (int length)
		{
			_values = new Vector3[length];
		}

		public void Add (Vector3 value)
		{
			_values[_currentIndex] = value;
			_currentIndex = (_currentIndex + 1) % _values.Length;
			if (_currentLength < _values.Length)
			{
				_currentLength++;
			}
		}

		public void Clear ()
		{
			_currentIndex = 0;
			_currentLength = 0;
		}

		public void Fill (Vector3 value)
		{
			for (int i = 0; i < _values.Length; i++)
			{
				_values[i] = value;
			}
		}

		public Vector3 GetAverage ()
		{
			if (_currentLength == 0)
			{
				return Vector3.zero;
			}
			Vector3 average = Vector3.zero;
			float scaler = 100;
			for (int i = 0; i < _currentLength; i++)
			{
				average += _values[i] * scaler;
			}
			average /= _currentLength;
			return average / scaler;
		}

		public Vector3 GetDirection ()
		{
			if (_currentLength == 0)
			{
				return Vector3.zero;
			}
			Vector3 direction = Vector3.zero;
			float scaler = 100;
			for (int i = 0; i < _currentLength - 1; i++)
			{
				direction += (_values[i + 1] * scaler) - (_values[i] * scaler).normalized;
			}
			direction /= (_currentLength - 1);
			return direction.normalized;
		}

		/// <summary>
		/// Calculates the maximum distance between all points in the buffer.
		/// A high value indicates the tracked object is moving. Inversely, a low value indicates rest.
		/// </summary>
		public float GetMaxVariance ()
		{
			float variance = 0;
			for (int a = 0; a < _currentLength; a++)
			{
				for (int b = 0; b < _currentLength; b++)
				{
					if (a == b) continue;
					// sqrMagnitude is used to minimize compute at expense of accuracy
					variance = Mathf.Max((_values[a] - _values[b]).sqrMagnitude);
				}
			}
			return variance;
		}
	}
}
