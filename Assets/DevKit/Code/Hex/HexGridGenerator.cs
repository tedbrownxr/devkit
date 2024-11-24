// DevKit
// Copyright (c) 2024 Ted Brown

using System;
using System.Collections.Generic;

namespace DevKit
{
	// https://www.redblobgames.com/grids/hexagons/
	/// <summary>Generates a hex grid using offset ("rectangular") coordinates</summary>
	public class HexGridGenerator
	{
		public static List<Hex> GetOffsetGrid (int columns, int rows, HexOffset offsetType, HexTop hexTop)
		{
			Func<int, int, Hex> conversionFunction = null;
			if (hexTop == HexTop.Flat)
			{
				if (offsetType == HexOffset.Even)
				{
					conversionFunction = evenq_to_axial;
				}
				else // offset type == OffsetType.Odd
				{
					conversionFunction = oddq_to_axial;
				}
			}
			else // hexTop == HexTop.Pointy
			{
				if (offsetType == HexOffset.Even)
				{
					conversionFunction = evenr_to_axial;
				}
				else // offset type == OffsetType.Odd
				{
					conversionFunction = oddr_to_axial;
				}
			}

			List<Hex> hexes = new List<Hex>();
			for (int q = 0; q < columns; q++)
			{
				for (int r = 0; r < rows; r++)
				{
					hexes.Add(conversionFunction(q, r));
				}
			}

			return hexes;
		}

		// Flat top hex / Even
		// Shoves even columns by 1/2 row
		static Hex evenq_to_axial (int x, int y)
		{
			var q = x;
			var r = y - (x + (x&1)) / 2;
			return new Hex(q, r);
		}

		// Pointy top hex / Even
		// Shoves even rows by 1/2 column
		static Hex evenr_to_axial (int x, int y)
		{
			var q = x - (y + (y&1)) / 2;
			var r = y;
			return new Hex(q, r);
		}

		// Flat top hex / Odd
		// Shoves odd columns by 1/2 row
		static Hex oddq_to_axial (int x, int y)
		{
			var q = x;
			var r = y - (x - (x&1)) / 2;
			return new Hex(q, r);
		}

		// Pointy top hex / Odd
		// Shoves odd rows by 1/2 column
		static Hex oddr_to_axial (int x, int y)
		{
			var q = x - (y - (y&1)) / 2;
			var r = y;
			return new Hex(q, r);
		}
	}
}
