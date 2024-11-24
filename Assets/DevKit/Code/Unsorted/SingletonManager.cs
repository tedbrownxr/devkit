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
	public class SingletonManager 
	{
		private Dictionary<Type, MonoBehaviour> _singletons;

		public T Get<T> () where T : MonoBehaviour
		{
			if (_singletons == null)
			{
				_singletons = new Dictionary<Type, MonoBehaviour>();
			}

			Type type = typeof(T);

			if (_singletons.TryGetValue(type, out MonoBehaviour instance))
			{
				return instance as T;
			}

			instance = GameObject.FindFirstObjectByType(type) as MonoBehaviour;

			if (instance != null)
			{
				_singletons.Add(type, instance);
				return instance as T;
			}

			Debug.LogError($"GlobalAssets.Get: No instance of type [{type.ToString()}] found. Returning null.");
			return null;
		}

		public void Register<T> (MonoBehaviour instance)
		{
			if (_singletons == null)
			{
				_singletons = new Dictionary<Type, MonoBehaviour>();
			}

			Type type = typeof(T);

			if (_singletons.ContainsKey(type))
			{
				_singletons[type] = instance;
			}
			else
			{
				_singletons.Add(type, instance);
			}
		}

		public bool TryFindAndRegister<T> ()
		{
			Type type = typeof(T);
			MonoBehaviour instance = GameObject.FindFirstObjectByType(type) as MonoBehaviour;
			if (instance != null)
			{
				Register<T>(instance);
				return true;
			}
			return false;
		}

		public void Unregister<T> ()
		{
			if (_singletons == null)
			{
				_singletons = new Dictionary<Type, MonoBehaviour>();
			}

			Type type = typeof(T);

			if (_singletons.ContainsKey(type))
			{
				_singletons.Remove(type);
			}
		}		
	}
}
