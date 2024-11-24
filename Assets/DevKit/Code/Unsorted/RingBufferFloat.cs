namespace DevKit
{
	public class RingBufferFloat
	{
		public bool IsFull => _currentLength == _values.Length;
		public int Size => _values.Length;

		private int _currentIndex;
		private int _currentLength;
		private float[] _values;

		public RingBufferFloat (int length)
		{
			_values = new float[length];
		}

		public void Add (float value)
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

		public void Fill (float value)
		{
			for (int i = 0; i < _values.Length; i++)
			{
				_values[i] = value;
			}
		}

		public float GetAverage ()
		{
			float average = 0;
			if (_currentLength == 0)
			{
				return average;
			}
			for (int i = 0; i < _currentLength; i++)
			{
				average += _values[i];
			}
			average /= _currentLength;
			return average;
		}
	}
}
