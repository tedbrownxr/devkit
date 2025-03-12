// DevKit
// Copyright (c) 2024 Ted Brown

using DevKit.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Example
{
	public enum ExampleTileType { None, Grass, Forest, Desert, Mountain, Island }

	[Serializable]
	public class ExampleTile
	{
		public string Id;
		public ExampleTileType TileType;
	}

	public class ExampleMenu : MonoBehaviour 
	{
		public ExampleTile ExampleTile;

		private void HandleChange (int value)
		{
			ExampleTile.TileType = (ExampleTileType) value;
		}

		protected void Awake ()
		{
			ExampleTile = new ExampleTile();
			ExampleTile.Id = "Example 1,2";

			Window panel = WindowManager.CreateWindow("Tile Panel");
			panel.AddLabelRow(ExampleTile.Id);
			panel.AddEnumField<ExampleTileType>("Tile Type", (int) ExampleTile.TileType, HandleChange);
			panel.FixColumnToBeUniformWidth(0);
		}

	}
}
