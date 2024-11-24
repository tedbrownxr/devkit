using System;
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
					Debug.LogError($"TryDelete [{path}]: Failed");
					return false;
				}
			}
			else
			{
				Debug.LogError($"TryDelete [{path}]: File does not exist");
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
					Debug.LogError($"TryGetTextStream: Error: {e}");
				}
			}

			text = string.Empty;
			return false;
		}

		/// <summary>
		/// Writes a text stream for a specific ID in a specific category.
		/// </summary>
		public static bool TrySaveTextStream (string requestedFilename, string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				Debug.LogError("SaveTextStream: Null or empty string passed as text data");
				return false;
			}

			string path = GetPath(requestedFilename);
			string dir = Path.GetDirectoryName(path);

			if (Directory.Exists(dir) == false)
			{
				Directory.CreateDirectory(dir);
				Debug.Log($"SaveTextStream: Created directory [{dir}]");
			}

			try
			{
				File.WriteAllText(path, text);
				Debug.Log($"SaveTextStream: Save successful at [{path}]");
#if UNITY_EDITOR
				AssetDatabase.ImportAsset(Path.Combine("Assets", Subdirectory, Path.GetFileName(path)));
#endif
				return true;
			}
			catch (ArgumentException e)
			{
				Debug.LogError($"SaveTextStream [{path}]: Error: {e}");
			}

			return false;
		}

		/// <summary>
		/// Ensures no illegal characters are used for a file or directory name.
		/// </summary>
		private static string CleanFilename (string filename)
		{
			// This is not 100% robust. Work in progres.
			string clean = Regex.Replace(filename, "[\\/ :\"*?<>|]+", "");
			return clean;
		}
	}
}
