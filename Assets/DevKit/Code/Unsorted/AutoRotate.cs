// DevKit
// Copyright (c) 2024-2025 Ted Brown

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit
{
	public class AutoRotate : MonoBehaviour 
	{
		public Vector3 _rotationAxis;
		public float _speed;
		public Space _rotationSpace;

		protected void Update ()
		{
			transform.Rotate(_rotationAxis * _speed * Time.deltaTime, _rotationSpace);
		}
	}
}
