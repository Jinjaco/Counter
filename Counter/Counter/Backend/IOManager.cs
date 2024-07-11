using Newtonsoft.Json;
using System.Data;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

public static class IOManager
{
	#region Enums

	#endregion

	#region Classes


	#endregion
	private static string GetSceenName { get => "test"; }

	public static T LoadFileObjectFromJSON<T>(string Filename, bool useSceenname = true)
	{
		T returnObject;
		try
		{

			string json = ReadFromFile(Filename, useSceenname ? GetSceenName : string.Empty);
			returnObject = JsonConvert.DeserializeObject<T>(json);

		}
		catch (System.Exception)
		{

			return default(T);
		}

		return returnObject;

	}

	public static void SaveObjectToJSON(string Filename, object DataObject, bool useSceenname = true)
	{
		string json = JsonConvert.SerializeObject(DataObject);

		WriteToFile(Filename, useSceenname ? GetSceenName : string.Empty, json);
	}

	private static void WriteToFile(string fileName, string ScenName, string json)
	{
		string path = GetFilePath(fileName, ScenName);
		FileStream fileStream = new FileStream(path, FileMode.Create);

		using (StreamWriter writer = new StreamWriter(fileStream))
		{
			writer.Write(json);
		}
	}

	private static string ReadFromFile(string fileName, string ScenName)
	{
		string path = GetFilePath(fileName, ScenName);
		if (File.Exists(path))
		{
			using (StreamReader reader = new StreamReader(path))
			{
				string json = reader.ReadToEnd();
				return json;
			}
		}
		else
		{
			Console.WriteLine("File not found");
		}

		return "Success";
	}

	private static string GetFilePath(string fileName, string ScenName)
	{
		string middleScetion = ScenName == string.Empty ? string.Empty : "/" + ScenName;
		string firstSection = Path.GetDirectoryName(
		System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
		firstSection = firstSection.Substring(6);
        string PathFull = firstSection + middleScetion;

		EnsureDirectoryExists(PathFull);
		PathFull = Path.Combine(PathFull, fileName);

		return PathFull;
	}

	private static void EnsureDirectoryExists(string filePath)
	{
		FileInfo fi = new FileInfo(filePath);
		if (!fi.Directory.Exists)
		{
			System.IO.Directory.CreateDirectory(fi.DirectoryName);
		}
	}
	//private static void OnApplicationPause(bool pause)
	//{
	//	if (pause) Save();
	//}

	public static void ToCSV(DataTable dt, string strFilePath)
	{
		StreamWriter sw = new StreamWriter(strFilePath, false);
		//headers    
		for (int i = 0; i < dt.Columns.Count; i++)
		{
			sw.Write(dt.Columns[i]);
			if (i < dt.Columns.Count - 1)
			{
				sw.Write(";");
			}
		}
		sw.Write(sw.NewLine);
		foreach (DataRow dr in dt.Rows)
		{
			for (int i = 0; i < dt.Columns.Count; i++)
			{
				if (!Convert.IsDBNull(dr[i]))
				{
					string value = dr[i].ToString();
					string replaceWith = " ";
					value = value.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
					if (value.Contains(';'))
					{
						value = System.String.Format("\"{0}\"", value);
						sw.Write(value);
					}
					else
					{
						sw.Write(value);
					}
				}
				if (i < dt.Columns.Count - 1)
				{
					sw.Write(";");
				}
			}
			sw.Write(sw.NewLine);
		}
		sw.Close();
	}
}
