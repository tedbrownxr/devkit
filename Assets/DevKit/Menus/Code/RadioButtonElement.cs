// DevKit
// Copyright (c) 2024 Ted Brown

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Menu
{
	public class RadioButtonElement : ButtonElement
	{
		public void SetSelected (bool value)
		{
			if (_optionalLeftIcon != null)
			{
				_optionalLeftIcon.enabled = value;
			}
		}
	}
}
