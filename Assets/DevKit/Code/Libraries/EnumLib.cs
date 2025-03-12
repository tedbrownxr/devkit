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
	public class EnumLib 
	{
		public static bool TryConvertStringToEnum<T> (string typeString, out T enumValue) where T : struct, Enum
		{
			return System.Enum.TryParse<T>(typeString, ignoreCase: true, out enumValue);
		}		
	}
}
