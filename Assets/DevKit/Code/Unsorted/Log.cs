// DevKit
// Copyright (c) 2024 Ted Brown

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit
{
	public class Log 
	{
		public static void ConfigurationProblem (string message)
		{
			Debug.LogError(message);
		}

		public static void Info (string message)
		{
			Debug.Log(message);
		}

		public static void RuntimeError (string message)
		{
			Debug.LogError(message);
		}

		public static void Unexpected (string message)
		{
			Debug.LogWarning(message);
		}
	}
}
