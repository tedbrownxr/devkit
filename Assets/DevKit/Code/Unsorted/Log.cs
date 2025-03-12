// DevKit
// Copyright (c) 2024 Ted Brown

#if UNITY_EDITOR || UNITY_STANDALONE
#define UNITY
#endif

#if UNITY
using UnityEngine;
#endif

using System;

namespace DevKit
{
	public class Log 
	{
		public static Action<string> OnWarning;
		public static Action<string> OnConfigurationProblem;
		public static Action<string> OnMessage;
		public static Action<string> OnError;

		public static void Warning (string message)
		{
#if UNITY
			Debug.Log(message);
#endif
			OnWarning?.Invoke(message);
		}

#if UNITY
		public static void Warning (string message, GameObject targetObject)
		{
			Debug.LogWarning(message, targetObject);
			OnWarning?.Invoke(message);
		}

		public static void ConfigurationProblem (string message, GameObject targetObject)
		{
			Debug.LogError(message, targetObject);
			OnConfigurationProblem?.Invoke(message);
		}

		public static void Message (string message, GameObject targetObject)
		{
			Debug.Log(message, targetObject);
			OnMessage?.Invoke(message);
		}

		public static void Error (string message, GameObject targetObject)
		{
			Debug.LogError(message, targetObject);
			OnError?.Invoke(message);
		}
#endif

		public static void ConfigurationProblem (string message)
		{
#if UNITY
			Debug.LogError(message);
#endif
			OnConfigurationProblem?.Invoke(message);
		}



		public static void Message (string message)
		{
#if UNITY
			Debug.Log(message);
#endif
			OnMessage?.Invoke(message);
		}


		public static void Error (string message)
		{
#if UNITY
			Debug.LogError(message);
#endif
			OnError?.Invoke(message);
		}
	}

}
