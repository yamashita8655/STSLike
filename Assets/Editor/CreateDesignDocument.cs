﻿using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//using System.Drawing.Image;
//using System.Drawing.Imaging;

/// <summary>
/// </summary>
public class CreateDesignDocumentFromEditor : Editor
{
	public class ResourceData {
		public string Tag;
		public string FilePathAndName;
		public ResourceData(string tag, string text) {
			Tag = tag;
			FilePathAndName = text;
		}
	}

    //[MenuItem("ShortCutCommand/CreateDesignDocument")]
    //private static void CreateDesignDocument()
    //{
    //	// 定数扱いの変数たち
    //	string imagePath = "Assets/Resources/Image";

    //	// まずは、フォルダリストを取得
    //	string[] imageFolders = Directory.GetDirectories(imagePath, "*", System.IO.SearchOption.AllDirectories);

    //	//// TODO
    //	//for (int i = 0; i < subFolders.Length; i++) {
    //	//	Debug.Log(subFolders[i]);
    //	//}
    //	
    //	string output = "";
    //	output += ":lang: ja\n";
    //	output += ":doctype: book\n";
    //	output += ":toc: left\n";
    //	output += ":toclevels: 3\n";
    //	output += ":toc-title: 目次\n";
    //	output += ":sectnums:\n";
    //	output += ":sectnumlevels: 4\n";
    //	output += ":sectlinks:\n";
    //	output += "//:imagesdir: ./_images\n";
    //	output += ":imagesdir: ./img\n";
    //	output += ":icons: font\n";
    //	output += ":source-highlighter: coderay\n";
    //	output += ":example-caption: 例\n";
    //	output += ":table-caption: 表\n";
    //	output += ":figure-caption: 図\n";
    //	output += ":docname: = デザイン資料\n";
    //	output += ":author: NOA.TEC株式会社\n";
    //	output += ":revnumber: 0.1\n";
    //	output += ":revdate: 2020/01/01\n";
    //	output += ":pdf-fontsdir: ./fonts\n";
    //	output += ":pdf-style: custom-theme.yml\n";
    //	output += "= デザイン資料\n";
    //	output += "== リスト\n";
    //	output += "[%hardbreaks]\n";

    //	output = CreateDesignDocumentFromEditor.GetFilePaths(imageFolders, output);

    //	string asciiDocFilePathAndName = "Doc/doc.adoc";
    //	File.WriteAllText(asciiDocFilePathAndName, output);

    //	// バッチ起動
    //	var app = new System.Diagnostics.ProcessStartInfo();
    //	app.FileName = "Doc\\create_pdf_for_unitytools.bat";
    //	System.Diagnostics.Process.Start(app);
    //}

    [MenuItem("ShortCutCommand/CreateDesignDocumentOnlyMeta")]
    private static void CreateDesignDocumentOnlyMeta()
    {
        // 定数扱いの変数たち
        string imagePath = "Assets/Resources/Image";

        // まずは、フォルダリストを取得
        string[] imageFolders = Directory.GetDirectories(imagePath, "*", System.IO.SearchOption.AllDirectories);

        string output = "";
        output += ":lang: ja\n";
        output += ":doctype: book\n";
        output += ":toc: left\n";
        output += ":toclevels: 3\n";
        output += ":toc-title: 目次\n";
        output += ":sectnums:\n";
        output += ":sectnumlevels: 4\n";
        output += ":sectlinks:\n";
        output += "//:imagesdir: ./_images\n";
        output += ":imagesdir: ./img\n";
        output += ":icons: font\n";
        output += ":source-highlighter: coderay\n";
        output += ":example-caption: 例\n";
        output += ":table-caption: 表\n";
        output += ":figure-caption: 図\n";
        output += ":docname: = デザイン資料\n";
        output += ":author: NOA.TEC株式会社\n";
        output += ":revnumber: 0.1\n";
        output += ":revdate: " + System.DateTime.Now.ToString("yyyy/MM/dd") + "\n";
        output += ":pdf-fontsdir: ./fonts\n";
        output += ":pdf-style: custom-theme.yml\n";
        output += "= デザイン資料\n\n";

        // ここから、シーンの大まかな説明
        output += "== シーン詳細\n\n";
        StreamReader descSr = new StreamReader(@"Meta/SceneDescriptionMetaData.txt", Encoding.GetEncoding("UTF-8"));
        string descStr = descSr.ReadToEnd();
        descSr.Close();

        string[] metaList = descStr.Split('\n');

        for (int i = 0; i < metaList.Length; i++) {
            string[] keys = metaList[i].Split(':');
            if (keys[0] == "Title") {
                output += "\n=== " + keys[1] + "\n\n";
            } else if (keys[0] == "Image") {
                output += "image::" + keys[1] + "[name, 150, 100]\n\n";
            } else if (keys[0] == "Description") {
                output += (keys[1] + " +\n");
            } else if (keys[0] == "BlockStart") {
                output += ("....\n");
            } else if (keys[0] == "BlockEnd") {
                output += ("....\n");
            }
        }

        string asciiDocFilePathAndName = "Doc/designdoc.adoc";
        File.WriteAllText(asciiDocFilePathAndName, output);

        // バッチ起動
        var app = new System.Diagnostics.ProcessStartInfo();
        app.FileName = "Doc\\create_designpdf_for_unitytools.bat";
        System.Diagnostics.Process.Start(app);
    }

    [MenuItem("ShortCutCommand/CreateDesignDocument")]
	private static void CreateDesignDocument()
	{
		// 定数扱いの変数たち
		string imagePath = "Assets/Resources/Image";

		// まずは、フォルダリストを取得
		string[] imageFolders = Directory.GetDirectories(imagePath, "*", System.IO.SearchOption.AllDirectories);

		string output = "";
		output += ":lang: ja\n";
		output += ":doctype: book\n";
		output += ":toc: left\n";
		output += ":toclevels: 3\n";
		output += ":toc-title: 目次\n";
		output += ":sectnums:\n";
		output += ":sectnumlevels: 4\n";
		output += ":sectlinks:\n";
		output += "//:imagesdir: ./_images\n";
		output += ":imagesdir: ./img\n";
		output += ":icons: font\n";
		output += ":source-highlighter: coderay\n";
		output += ":example-caption: 例\n";
		output += ":table-caption: 表\n";
		output += ":figure-caption: 図\n";
		output += ":docname: = デザイン資料\n";
		output += ":author: NOA.TEC株式会社\n";
		output += ":revnumber: 0.1\n";
		output += ":revdate: " + System.DateTime.Now.ToString("yyyy/MM/dd") + "\n";
		output += ":pdf-fontsdir: ./fonts\n";
		output += ":pdf-style: custom-theme.yml\n";
		output += "= デザイン資料\n\n";

		// ここから、シーンの大まかな説明
		output += "== シーン詳細\n\n";
		StreamReader descSr = new StreamReader(@"Meta/SceneDescriptionMetaData.txt", Encoding.GetEncoding("UTF-8"));
		string descStr = descSr.ReadToEnd();
		descSr.Close();
		
		string[] metaList = descStr.Split('\n');

		for (int i = 0; i < metaList.Length; i++) {
			string[] keys = metaList[i].Split(':');
			if (keys[0] == "Title") {
				output += "=== " + keys[1] + "\n";
			} else if (keys[0] == "Image") {
				output += "image::" + keys[1] + "[name, 150, 100]\n";
			} else if (keys[0] == "Description") {
				output += (keys[1] + " +\n\n");
			}
		}

		// ここから、Resourcesに含まれるImageのパーツ詳細説明
		output += "== 個別素材詳細\n\n";
		//output += "=== リスト\n";
		Dictionary<string, List<ResourceData>> dict = new Dictionary<string, List<ResourceData>>();

        dict = CreateDesignDocumentFromEditor.GetFilePaths(imageFolders, dict);

		StreamReader sr = new StreamReader(@"Meta/SceneMetaData.txt", Encoding.GetEncoding("UTF-8"));
		string str = sr.ReadToEnd();
		sr.Close();
		
		string[] sceneNameList = str.Split('\n');

		// まずは、シーンの並び順にデータを取得
		for (int i = 0; i < sceneNameList.Length; i++) {
			List<ResourceData> list = null;
			if (dict.TryGetValue(sceneNameList[i], out list) == true) {
				output += ("=== " + sceneNameList[i] + "\n");
				for (int j = 0; j < list.Count; j++) {
					string fileName = list[j].FilePathAndName;
					using (BinaryReader bin = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read))) {
						byte[] rb = bin.ReadBytes((int)bin.BaseStream.Length);
						bin.Close();
						int pos = 16, width = 0, height = 0;
						for (int loop = 0; loop < 4; loop++) width  = width  * 256 + rb[pos++];
						for (int loop = 0; loop < 4; loop++) height = height * 256 + rb[pos++];
						
						output += "image::../../" + fileName + "[name, 100, 100]\n";
						output += "fileName:" + fileName + " +\n";
						output += "Width:" + width + " +\n";
						output += "Height:" + height + " +\n";
						output += "\n";
					}
				}
				dict.Remove(sceneNameList[i]);
			}
		}

		// シーンに関連づいていない物を、その他扱いでリスト化
		output += ("== その他\n");
		foreach (var data in dict) {
			for (int i = 0; i < data.Value.Count; i++) {
				string fileName = data.Value[i].FilePathAndName;
				using (BinaryReader bin = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read))) {
					byte[] rb = bin.ReadBytes((int)bin.BaseStream.Length);
					bin.Close();
					int pos = 16, width = 0, height = 0;
					for (int loop = 0; loop < 4; loop++) width  = width  * 256 + rb[pos++];
					for (int loop = 0; loop < 4; loop++) height = height * 256 + rb[pos++];
					
					output += "image::../../" + fileName + "[name, 100, 100]\n";
					output += "fileName:" + fileName + " +\n";
					output += "Width:" + width + " +\n";
					output += "Height:" + height + " +\n";
					output += "\n";
				}
			}
		}

		//foreach (var data in dict) {
		//	string log = data.Key + "\n";
		//	for (int i = 0; i < data.Value.Count; i++) {
		//		log += (data.Value[i].FilePathAndName + "\n");
		//	}
		//	Debug.Log(log);
		//}

		string asciiDocFilePathAndName = "Doc/designdoc.adoc";
		File.WriteAllText(asciiDocFilePathAndName, output);

		// バッチ起動
		var app = new System.Diagnostics.ProcessStartInfo();
		app.FileName = "Doc\\create_designpdf_for_unitytools.bat";
		System.Diagnostics.Process.Start(app);
	}
	
	//public static string GetFilePaths(string[] directoryPaths, string output)
	//{
	//	for (int i = 0; i < directoryPaths.Length; i++) {
	//		string[] subFolders = Directory.GetDirectories(directoryPaths[i], "*", System.IO.SearchOption.AllDirectories);
	//		//output = CreateDesignDocumentFromEditor.GetFilePaths(subFolders, output);

	//		//string[] files = System.IO.Directory.GetFiles(directoryPaths[i], "*", System.IO.SearchOption.AllDirectories);
	//		string[] files = System.IO.Directory.GetFiles(directoryPaths[i], "*", System.IO.SearchOption.TopDirectoryOnly);
	//		for (int j = 0; j < files.Length; j++) {
	//			if (files[j].Contains(".meta")) {
	//				continue;
	//			}
	//			if (files[j].Contains(".png") == false) {
	//				Debug.LogWarning(files[j]);
	//				continue;
	//			}
	//			string info = "";
	//			using (BinaryReader bin = new BinaryReader(new FileStream(files[j], FileMode.Open, FileAccess.Read))) {
	//				byte[] rb = bin.ReadBytes((int)bin.BaseStream.Length);
	//				bin.Close();
	//				int pos = 16, width = 0, height = 0;
	//				for (int loop = 0; loop < 4; loop++) width  = width  * 256 + rb[pos++];
	//				for (int loop = 0; loop < 4; loop++) height = height * 256 + rb[pos++];
	//				
	//				info = string.Format("fileName:{0}\nwidth:{1}\nheight:{2}", files[j], width, height);

	//				Debug.Log(info);
	//				output += "image::../../" + files[j] + "[name, 100, 100]\n";
	//				output += "fileName:" + files[j] + " +\n";
	//				output += "Width:" + width + " +\n";
	//				output += "Height:" + height + " +\n";
	//				output += "\n";
	//			}
	//		}
	//	}
	//	return output;
	//}
	
	public static Dictionary<string, List<ResourceData>> GetFilePaths(string[] directoryPaths, Dictionary<string, List<ResourceData>> output)
	{
		for (int i = 0; i < directoryPaths.Length; i++) {
			string[] subFolders = Directory.GetDirectories(directoryPaths[i], "*", System.IO.SearchOption.AllDirectories);

			string[] files = System.IO.Directory.GetFiles(directoryPaths[i], "*", System.IO.SearchOption.TopDirectoryOnly);
			for (int j = 0; j < files.Length; j++) {
				if (files[j].Contains(".meta")) {
					continue;
				}
				if (files[j].Contains(".png") == false) {
					Debug.LogWarning(files[j]);
					continue;
				}

				string filePathAndName = files[j].Replace("\\", "/");
				string[] stringList = filePathAndName.Split('/');
				string tag = stringList[stringList.Length-2];

                List<ResourceData> list = null;
                if (output.TryGetValue(tag, out list) == true) {
                    list.Add(new ResourceData(tag, filePathAndName));
                } else {
                    list = new List<ResourceData>();
                    list.Add(new ResourceData(tag, filePathAndName));
                    output.Add(tag, list);
                }
			}
		}
		return output;
	}
}
