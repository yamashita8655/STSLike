using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
//using System.Drawing.Image;
//using System.Drawing.Imaging;

/// <summary>
/// </summary>
public class CreateLocalSceneManagerFromEditor : Editor
{
	[MenuItem("ShortCutCommand/CreateLocalSceneManagerScript")]
	private static void CreateLocalSceneManager()
	{
		string sceneNameString = "";
		StreamReader sr = new StreamReader(@"Meta/SceneMetaData.txt", Encoding.GetEncoding("UTF-8"));
		string str = sr.ReadToEnd();
		sr.Close();
		
		string[] sceneNameList = str.Split('\n');

		string output = "";
		output += "using System;\n";
		output += "using System.Collections;\n";
		output += "using System.Collections.Generic;\n";
		output += "using UnityEngine;\n";
		output += "using UnityEngine.SceneManagement;\n";
		output += "\n";
		output += "public class LocalSceneManager : SimpleMonoBehaviourSingleton<LocalSceneManager> {\n";
		output += "	private List<string> SceneNameList = new List<string>() {\n";

		for (int i = 0; i < sceneNameList.Length; i++) {
			output += ("		\"" + sceneNameList[i] + "\",\n");
		}

		output += "	};\n";
		output += "\n";
		output += "	public enum SceneName : int {\n";

		for (int i = 0; i < sceneNameList.Length; i++) {
			if (i == 0) {
				output += ("		" + sceneNameList[i] + " = 0,\n");
			} else {
				output += ("		" + sceneNameList[i] + ",\n");
			}
		}

		output += "		None\n";
		output += "	};\n";
		output += "\n";
		output += "	private SceneName FirstSceneName = SceneName.Splash;\n";
		output += "\n";
		output += "	private SceneName CurrentSceneName = SceneName.None;\n";
		output += "	\n";
		output += "	public SceneDataBase SceneData { get; private set;}\n";
		output += "\n";
		output += "	public void Initialize() {\n";
		output += "		SceneData = null;\n";
		output += "		SceneManager.LoadScene(SceneNameList[(int)SceneName.Fade], LoadSceneMode.Additive);\n";
		output += "	}\n";
		output += "\n";
		output += "	public SceneName GetFirstSceneName() {\n";
		output += "		return FirstSceneName;\n";
		output += "	}\n";
		output += "	\n";
		output += "	public void LoadScene(SceneName name, SceneDataBase sceneData) {\n";
		output += "		SceneData = sceneData;\n";
		output += "\n";
		output += "		// 本来は、この辺りでフェードなどの切り替え処理が入るので、\n";
		output += "		// LoadとUnloadは一辺に行うべきではない\n";
		output += "		SceneManager.LoadScene(SceneNameList[(int)name], LoadSceneMode.Additive);\n";
		output += "		if (CurrentSceneName != SceneName.None) {\n";
		output += "			SceneManager.UnloadSceneAsync(SceneNameList[(int)CurrentSceneName]);\n";
		output += "		}\n";
		output += "\n";
		output += "		CurrentSceneName = name;\n";
		output += "	}\n";
		output += "}\n";

		string scriptPathAndName = "Assets/Scripts/Manager/LocalSceneManager.cs";
		File.WriteAllText(scriptPathAndName, output);
	}
}
