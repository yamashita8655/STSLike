using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour {

	public static bool IsInitialized = false;

	public void Initialize () {
		// Boot以外から呼び出された時対策
		DontDestroyOnLoad(this);

		// TODO リリース時は無効化する？
		Application.logMessageReceived += HandleLog;

		Application.targetFrameRate = 60;

		StartCoroutine(CoInitialize());
		
	}
	
	public IEnumerator CoInitialize () {
		//Debug.Log("EntryPoint CoInitialize before");
		// ↑ここまでは、同一フレーム内で実行される

		// ↓ここで処理が返ると、他のMonoBehaviourのStartやAwakeが呼び出されてしまい
		// 初期化の役割を果たせなくなる
		// なので、初期化の役割を果たさせる場合は
		// 1．コルーチンを使わないようにするか
		// 2．有効となるシーンの先々で、ここで行う初期化が完了しているかどうかを待つ
		// 3．この初期化が終わるまで、シーン上に有効なオブジェクトを配置しない。つまり、ここで配置を行うようにする
		// どれかになるのではないかと思われる。
		

		yield return null;
		// TODO どのシーンから起動されても、最初はBootシーンを起動する
		SceneManager.LoadScene("Boot", LoadSceneMode.Single);
		yield return null;
		//SerializeFieldResourceManager.Instance.Initialize();

		yield return null;

		yield return SoundManager.Instance.CoInitialize();

		LocalSceneManager.Instance.Initialize();
		yield return null;

		NetworkManager.Instance.Initialize();
		ResourceManager.Instance.Initialize();
		PlayerPrefsManager.Instance.Initialize();
		FadeManager.Instance.Initialize();
		SystemDialogManager.Instance.Initialize();

		// マスターデータ読み込み
		MasterTextTable.Instance.Initialize();
		MasterAction2Table.Instance.Initialize();
		MasterEnemyTable.Instance.Initialize();
		MasterEquipItemDataTable.Instance.Initialize();
		MasterHealTable.Instance.Initialize();
		MasterDungeonTable.Instance.Initialize();
		MasterEnemyLotTable.Instance.Initialize();
		MasterArtifactTable.Instance.Initialize();
		MasterEnemyAITable.Instance.Initialize();
		MasterCardLotTable.Instance.Initialize();
		MasterArtifactLotTable.Instance.Initialize();
		MasterStringTable.Instance.Initialize();
		MasterRegularCardMaxCostTable.Instance.Initialize();

		// マスターデータ読み込んでないと出来ない初期化があるので、これはマスターデータ読み終わった後に対応
		DebugManager.Instance.Initialize();

		// 色々
		LocalServerManager.Instance.Initialize();
		// フェードアウトをしておかないと、背景が見えるので、
		// ここで最初のフェードアウトだけしておく
		FadeManager.Instance.FadeOut(
			FadeManager.Type.Simple,
			0.01f,
			() => {
				LocalSceneManager.Instance.LoadScene(LocalSceneManager.Instance.GetFirstSceneName(), null);
			}
		);

		//PlayerPrefsManager playerPrefsManager = PlayerPrefsManager.Instance;
		//SoundManager soundManager = SoundManager.Instance;
		//// サウンドの初期設定
		//soundManager.SetBgmVolume(playerPrefsManager.GetVolume(PlayerPrefsManager.SaveType.BgmVolume));
		//soundManager.SetBgmMuteFlag(playerPrefsManager.GetIsMute(PlayerPrefsManager.SaveType.BgmMute));
		//soundManager.SetSeVolume(playerPrefsManager.GetVolume(PlayerPrefsManager.SaveType.SeVolume));
		//soundManager.SetSeMuteFlag(playerPrefsManager.GetIsMute(PlayerPrefsManager.SaveType.SeMute));

		// TODO Bootで行う処理が無くなったので、破棄してみる
		SceneManager.UnloadSceneAsync("Boot");

		EntryPoint.IsInitialized = true;
	}

	private void HandleLog(string condition, string stackTrace, LogType type)
	{
		// 全てのログやExeptionを検知しているときりがないので、
		// Exeptionのみを検知するようにし、一度検知したら、それ以後は正常に動かない物として、検知をやめる
		if (type == LogType.Exception) {
			if (EntryPoint.IsInitialized == true) {
				DebugManager.Instance.UpdateDebugLog(condition);
				DebugManager.Instance.UpdateDebugLog(stackTrace);
				DebugManager.Instance.UpdateDebugLog(type.ToString());
				Application.logMessageReceived -= HandleLog;
			}
		}
	}

	//// Use this for initialization
	//void Awake () {
	//	Debug.Log("EntryPoint Awake");
	//}

	//// Use this for initialization
	//public void Initialize () {
	//	StartCoroutine(CoInitialize());
	//}

	//public IEnumerator CoInitialize () {
	//	Debug.Log("EntryPoint CoInitialize before");
	//	// ↑ここまでは、同一フレーム内で実行される

	//	// ↓ここで処理が返ると、他のMonoBehaviourのStartやAwakeが呼び出されてしまい
	//	// 初期化の役割を果たせなくなる
	//	// なので、初期化の役割を果たさせる場合は
	//	// 1．コルーチンを使わないようにするか
	//	// 2．有効となるシーンの先々で、ここで行う初期化が完了しているかどうかを待つ
	//	// 3．この初期化が終わるまで、シーン上に有効なオブジェクトを配置しない。つまり、ここで配置を行うようにする
	//	// どれかになるのではないかと思われる。
	//	yield return null;
	//	Debug.Log("EntryPoint CoInitialize after");
	//}

	//// Use this for initialization
	//void Awake () {
	//	Debug.Log("EntryPoint Awake");
	//}

	//// Use this for initialization
	//IEnumerator Start () {
	//	// 今回の実験と関係ないけど、IEnumeratorが戻り値のStartは、デフォで用意されているっぽい
	//	// 自分ではStartCoroutineやMoveNextは呼び出していないが、自動的に最後の処理まで実行される
	//	Debug.Log("EntryPoint CoStart before");
	//	yield return null;
	//	Debug.Log("EntryPoint CoStart after");
	//}
}
