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
	public class Column
	{
		public List<Row> Rows;

		public Column ()
		{
			Rows = new List<Row>();
		}

		public void AddRow (Row row)
		{
			Rows.Add(row);
		}


	}
}
