using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DevKit
{
	/// <summary>
	/// I/O for text streams as files on the local disk.
	/// </summary>
	public class LocalFileStorage
	{
		const string Subdirectory = "Resources";

		public static bool Exists (string requestedFilename)
		{
			string path = GetPath(requestedFilename);
			return File.Exists(path);
		}

		public static string GetPath (string requestedFilename)
		{
			string filename = CleanFilename(requestedFilename);
			return Path.Combine(Application.dataPath, Subdirectory, filename);
		}

		public static string[] GetFilenames (string resourcePath)
		{
			string fullPath = GetPath(resourcePath);
			List<string> files = new List<string>(Directory.GetFiles(fullPath));
			string removeFilter = ".meta";
			for (int i = files.Count - 1; i >= 0; i--)
			{
				if (files[i].EndsWith(removeFilter))
				{
					files.RemoveAt(i);
				}
			}
			return files.ToArray();
		}

		public static bool TryDelete (string requestedFilename)
		{
			string path = GetPath(requestedFilename);

			// NOTE: Relying on File.Exists before running an operation on that file does not scale in a multiuser environment,
			// because the file might not exist when the delete function is called.
			if (File.Exists(path))
			{
				try
				{
					File.Delete(path);
					return true;
				}
				catch // no exception is thrown when file.delete fails
				{
					Log.Error($"TryDelete [{path}]: Failed");
					return false;
				}
			}
			else
			{
				Log.Error($"TryDelete [{path}]: File does not exist");
				return false;
			}
		}

		/// <summary>
		/// Returns a text stream for a specific ID.
		/// </summary>
		public static bool TryGetTextStream (string requestedFilename, out string text, bool printErrors = true)
		{
			string path = GetPath(requestedFilename);

			try
			{
				using (StreamReader reader = File.OpenText(path))
				{
					text = reader.ReadToEnd();
					reader.Dispose();
				}
				return true;
			}
			catch (Exception e)
			{
				if (printErrors)
				{
					Log.Error($"TryGetTextStream: Error: {e}");
				}
			}

			text = string.Empty;
			return false;
		}

		/// <summary>
		/// Writes a text stream for a specific ID in a specific category.
		/// </summary>
		public static bool TrySaveTextStream (string requestedFilename, string textStream)
		{
			if (string.IsNullOrEmpty(requestedFilename))
			{
				Log.Error("TrySaveTextStream: requestedFilename is null or empty");
				return false;
			}

			if (string.IsNullOrEmpty(textStream))
			{
				Log.Error("TrySaveTextStream: textStream is null or empty");
				return false;
			}

			string path = GetPath(requestedFilename);
			string systemDirectory = Path.GetDirectoryName(path);
			string filename = Path.GetFileName(path);

			if (Directory.Exists(systemDirectory) == false)
			{
				Directory.CreateDirectory(systemDirectory);
				Log.Message($"SaveTextStream: Created directory [{systemDirectory}]");
			}

			try
			{
				File.WriteAllText(path, textStream);
				Log.Message($"SaveTextStream: Save successful at [{path}]");
#if UNITY_EDITOR
				// Must be a path relative to the project folder
				string projectRelativeName = $"Assets/{Subdirectory}/{requestedFilename}";
				AssetDatabase.ImportAsset(projectRelativeName);
#endif
				return true;
			}
			catch (ArgumentException e)
			{
				Log.Error($"SaveTextStream [{path}]: Error: {e}");
			}

			return false;
		}

		/// <summary>
		/// Ensures no illegal characters are used for a file or directory name.
		/// </summary>
		private static string CleanFilename (string filename)
		{
			// This is not 100% robust. Work in progress.
			//string clean = Regex.Replace(filename, "[\\/ :\"*?<>|]+", "");
			string clean = Regex.Replace(filename, "[ :\"*?<>|]+", "");
			return clean;
		}
	}
}
