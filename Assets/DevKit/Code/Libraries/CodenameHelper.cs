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
	public class CodenameHelper 
	{
		public static char Delimiter = '-';
		public enum Adjective
		{
			Ambitious, Beautiful, Creative, Delightful, Elegant, Flexible, Genuine, Harmonic, Innovative, Jovial, Kinetic, Lovely, Majestic, Nimble, Optimistic, Precise, Quirky, Radiant, Serene, Tranquil, Unique, Vigilant, Wonderful, Xenophobic, Youthful, Zealous
		}

		public enum Animal
		{
			Bear, Cat, Chicken, Cow, Deer, Dog, Dolphin, Duck, Eagle, Elephant, Goat, Guppy, Hawk, Horse, Lion, Lizard, Penguin, Pig, Salmon, Shark, Sheep, Snake, Tiger, Turtle, Wolf

		}

		public enum Color
		{
			Beige, Black, Blue, Brown, Crimson, Cyan, Gold, Gray, Green, Indigo, Ivory, Magenta, Maroon, Navy, Orange, Pink, Purple, Red, Silver, Tan, Teal, Turquoise, Violet, White, Yellow
		}

		public static string GetCodename ()
		{
			System.Random rand = new System.Random();
			string[] adjectives = System.Enum.GetNames(typeof(Adjective));
			string[] colors = System.Enum.GetNames(typeof(Color));
			string[] animals = System.Enum.GetNames(typeof(Animal));
			return $"{adjectives[rand.Next(adjectives.Length)]}{Delimiter}{colors[rand.Next(colors.Length)]}{Delimiter}{animals[rand.Next(animals.Length)]}";
		}
	}
}
