using UnityEditor;
using UnityEngine;

/// <summary>
/// Unityエディタ上からGameビューのスクリーンショットを撮るEditor拡張
/// </summary>
public class CaptureScreenshotFromEditor : Editor
{
	/// <summary>
	/// キャプチャを撮る
	/// </summary>
	/// <remarks>
	/// Edit > CaptureScreenshot に追加。
	/// HotKeyは Ctrl + Shift + F12。
	/// </remarks>
	[MenuItem("ShortCutCommand/CaptureScreenshot #%F12")]
	private static void CaptureScreenshot()
	{
		// 現在時刻からファイル名を決定
		var path = EditorApplication.currentScene;
		//現在読み込まれているシーン数だけループ
		string sceneName = "";
		for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount ; i++) {
			//読み込まれているシーンを取得し、その名前をログに表示
			sceneName = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).name; 
			if (sceneName != "Boot" && sceneName != "Fade") {
				break;
			}
		}
		string fileName = sceneName + ".png";

		// ここからは独自処理
		// AsciiDoc用に、保存先を変える
		fileName = "Doc/img/" + fileName;

		Debug.Log(fileName);
		// キャプチャを撮る
#if UNITY_2017_1_OR_NEWER
		ScreenCapture.CaptureScreenshot(fileName); // ← GameViewにフォーカスがない場合、この時点では撮られない
#else
		Application.CaptureScreenshot(fileName); // ← GameViewにフォーカスがない場合、この時点では撮られない
#endif
		// GameViewを取得してくる
		var assembly = typeof(EditorWindow).Assembly;
		var type = assembly.GetType("UnityEditor.GameView");
		var gameview = EditorWindow.GetWindow(type);
		// GameViewを再描画
		gameview.Repaint();
	}
	
	[MenuItem("ShortCutCommand/CaptureScreenshotNameTime")]
	private static void CaptureScreenshotNameTime()
	{
		// 現在時刻からファイル名を決定
		var path = EditorApplication.currentScene;
		//現在読み込まれているシーン数だけループ
		string sceneName = "";
		for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount ; i++) {
			//読み込まれているシーンを取得し、その名前をログに表示
			sceneName = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).name; 
			if (sceneName != "Boot" && sceneName != "Fade") {
				break;
			}
		}
		string time = System.DateTime.Now.ToString("yyyyMMddHHmmss");
		string fileName = sceneName + time + ".png";

		// ここからは独自処理
		// AsciiDoc用に、保存先を変える
		fileName = "Doc/img/" + fileName;

		Debug.Log(fileName);
		// キャプチャを撮る
#if UNITY_2017_1_OR_NEWER
		ScreenCapture.CaptureScreenshot(fileName); // ← GameViewにフォーカスがない場合、この時点では撮られない
#else
		Application.CaptureScreenshot(fileName); // ← GameViewにフォーカスがない場合、この時点では撮られない
#endif
		// GameViewを取得してくる
		var assembly = typeof(EditorWindow).Assembly;
		var type = assembly.GetType("UnityEditor.GameView");
		var gameview = EditorWindow.GetWindow(type);
		// GameViewを再描画
		gameview.Repaint();
	}
}
