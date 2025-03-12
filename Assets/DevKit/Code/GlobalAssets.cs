// DevKit
// Copyright (c) 2024 Ted Brown
// Author: Ted Brown <tedbrownxr@gmail.com>

using System;
using System.Collections.Generic;
using UnityEngine;

namespace DevKit
{
	public class GlobalAssets
	{
		private Dictionary<Type, MonoBehaviour> _globalAssets;

		public T Get<T> () where T : MonoBehaviour
		{
			if (_globalAssets == null)
			{
				_globalAssets = new Dictionary<Type, MonoBehaviour>();
			}

			Type type = typeof(T);

			if (_globalAssets.TryGetValue(type, out MonoBehaviour instance))
			{
				return instance as T;
			}

			instance = GameObject.FindFirstObjectByType(type) as MonoBehaviour;

			if (instance != null)
			{
				_globalAssets.Add(type, instance);
				return instance as T;
			}

			Debug.LogError($"GlobalAssets.Get: No instance of type [{type.ToString()}] found. Returning null.");
			return null;
		}

		public void Register<T> (MonoBehaviour instance)
		{
			if (_globalAssets == null)
			{
				_globalAssets = new Dictionary<Type, MonoBehaviour>();
			}

			Type type = typeof(T);

			if (_globalAssets.ContainsKey(type))
			{
				_globalAssets[type] = instance;
			}
			else
			{
				_globalAssets.Add(type, instance);
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
			if (_globalAssets == null)
			{
				_globalAssets = new Dictionary<Type, MonoBehaviour>();
			}

			Type type = typeof(T);

			if (_globalAssets.ContainsKey(type))
			{
				_globalAssets.Remove(type);
			}
		}
	}
}
