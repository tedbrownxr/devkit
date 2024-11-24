using System;
using System.Collections.Generic;

namespace DevKit
{
	// https://www.redblobgames.com/pathfinding/a-star/implementation.html#csharp
	public class HexAStarSearch 
	{
		public const int InvalidMoveCost = -123;
		
		// public List<Hex> Path;

		// private Dictionary<Hex, Hex> _cameFrom = new Dictionary<Hex, Hex>();
		// private Dictionary<Hex, int> _costSoFar = new Dictionary<Hex, int>();

		/// <summary>
		/// Returns a list of hexes that lead to a target in the cheapest way possible.
		/// List does NOT include the start hex.
		/// </summary>
		public static bool TryGetPath (Hex start, Hex target, Func<Hex, bool> isValidHex, Func<Hex, Hex, int> getMoveCost, out List<Hex> path)
		{
			Dictionary<Hex, Hex> cameFrom = new Dictionary<Hex, Hex>();
			Dictionary<Hex, int> costSoFar = new Dictionary<Hex, int>();

			var frontier = new PriorityQueue<Hex, int>();
			frontier.Enqueue(start, 0);

			cameFrom[start] = start;
			costSoFar[start] = 0;

			// build a flow map that directs us towards the goal
			while (frontier.Count > 0)
			{
				Hex current = frontier.Dequeue();

				if (current.Equals(target))
				{
					break;
				}

				// Get all of the hypothetical neighbors for this hex, 
				// then remove ones that aren't valid for this map.
				List<Hex> neighbors = new List<Hex>(current.GetNeighbors());
				for (int i = neighbors.Count - 1; i >= 0; i--)
				{
					if (isValidHex(neighbors[i]) == false)
					{
						neighbors.RemoveAt(i);
					}
				}

				foreach (Hex next in neighbors)
				{
					int costToNextHex = getMoveCost(current, next);
					int newCost = costSoFar[current] + costToNextHex;
					if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
					{
						costSoFar[next] = newCost;
						// this formula is referred to as "the heuristic"
						int priority = newCost + Math.Abs(next.q - target.q) + Math.Abs(next.r - target.r);
						frontier.Enqueue(next, priority);
						cameFrom[next] = current;
					}
				}
			}

			path = new List<Hex>();

			if (cameFrom.ContainsKey(target) == false)
			{
				return false;
			}

			// walk backwards from the goal to the start to create the optimal path
			{
				var current = target;
				while (current != start)
				{
					path.Add(current);
					current = cameFrom[current];
				}
				path.Reverse();
			}

			return true;
		}

		// public AStarSearch (HexMap map, Hex start, Hex goal, bool includeStart = false)
		// {
		// 	var frontier = new PriorityQueue<Hex, int>();
		// 	frontier.Enqueue(start, 0);

		// 	_cameFrom[start] = start;
		// 	_costSoFar[start] = 0;

		// 	// build a flow map that directs us towards the goal
		// 	while (frontier.Count > 0)
		// 	{
		// 		Hex current = frontier.Dequeue();

		// 		if (current.Equals(goal))
		// 		{
		// 			break;
		// 		}

		// 		foreach (Hex next in map.GetNeighbors(current))
		// 		{
		// 			int newCost = _costSoFar[current] + map.GetCost(current, next);
		// 			if (!_costSoFar.ContainsKey(next) || newCost < _costSoFar[next])
		// 			{
		// 				_costSoFar[next] = newCost;
		// 				// this formula is referred to as "the heuristic"
		// 				int priority = newCost + Math.Abs(next.q - goal.q) + Math.Abs(next.r - goal.r);
		// 				frontier.Enqueue(next, priority);
		// 				_cameFrom[next] = current;
		// 			}
		// 		}
		// 	}

		// 	// walk backwards from the goal to the start to create the optimal path
		// 	{
		// 		Path = new List<Hex>();
		// 		var current = goal;
		// 		while (current != start)
		// 		{
		// 			Path.Add(current);
		// 			current = _cameFrom[current];
		// 		}
		// 		if (includeStart)
		// 		{
		// 			Path.Add(start);
		// 		}
		// 		Path.Reverse();
		// 	}
		// }
	}
}
