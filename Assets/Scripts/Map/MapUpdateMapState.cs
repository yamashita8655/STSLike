using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUpdateMapState : StateBase {

	//private readonly int EnemyRatio = 250;
	//private readonly int EliteRatio = 250;
	//private readonly int TreasureRatio = 250;
	//private readonly int HealRatio = 250;
	private readonly int EnemyRatio = 1000;
	private readonly int EliteRatio = 0;
	private readonly int TreasureRatio = 0;
	private readonly int HealRatio = 0;

	/// <summary>
	/// メイン前処理.
	/// </summary>
	override public bool OnBeforeMain()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		if (MapDataCarrier.Instance.NowFloor < MapDataCarrier.Instance.MaxFloor) {
			// マップを追加
			if (MapDataCarrier.Instance.MapTypeList.Count == 0) {
				MapDataCarrier.Instance.NowFloor = 1;
				// 初回は、3つ生成する
				for (int i = 0; i < 3; i++) {
					Enum.MapType type = LotteryMapType();
					MapDataCarrier.Instance.MapTypeList.Add(type);
				}
			} else {
				MapDataCarrier.Instance.NowFloor++;

				// マップは、最初に3個分作っているので、
				// 2引いた値で上限チェックする
				if (MapDataCarrier.Instance.NowFloor <= (MapDataCarrier.Instance.MaxFloor-2)) {
					Enum.MapType type = LotteryMapType();
					MapDataCarrier.Instance.MapTypeList.Add(type);
				}
			}
		}

		scene.NowFloorText.text = MapDataCarrier.Instance.NowFloor.ToString();

		// まず、真ん中の画像表示
		int index = MapDataCarrier.Instance.CurrentMapNumber;
		scene.MapImages[2].sprite = scene.MapSprites[(int)MapDataCarrier.Instance.MapTypeList[index]];

		// 前2つ
		if ((index-2) < 0) {
			scene.MapImages[0].gameObject.SetActive(false);
		} else {
			scene.MapImages[0].gameObject.SetActive(true);
			scene.MapImages[0].sprite = scene.MapSprites[(int)MapDataCarrier.Instance.MapTypeList[index-2]];
		}

		// 前1つ
		if ((index-1) < 0) {
			scene.MapImages[1].gameObject.SetActive(false);
		} else {
			scene.MapImages[1].gameObject.SetActive(true);
			scene.MapImages[1].sprite = scene.MapSprites[(int)MapDataCarrier.Instance.MapTypeList[index-1]];
		}
		
		// 次1つ
		if ((index+1) >= MapDataCarrier.Instance.MapTypeList.Count) {
			scene.MapImages[3].gameObject.SetActive(false);
		} else {
			scene.MapImages[3].gameObject.SetActive(true);
			scene.MapImages[3].sprite = scene.MapSprites[(int)MapDataCarrier.Instance.MapTypeList[index+1]];
		}
		
		// 次2つ
		if ((index+2) >= MapDataCarrier.Instance.MapTypeList.Count) {
			scene.MapImages[4].gameObject.SetActive(false);
		} else {
			scene.MapImages[4].gameObject.SetActive(true);
			scene.MapImages[4].sprite = scene.MapSprites[(int)MapDataCarrier.Instance.MapTypeList[index+2]];
		}

		return false;
	}

	private Enum.MapType LotteryMapType() {
		int seedEnd = (EnemyRatio + EliteRatio + TreasureRatio + HealRatio);
		int seed = UnityEngine.Random.Range(0, seedEnd);
		Enum.MapType type = Enum.MapType.Max;

		int startBorder = 0;
		int endBorder = EnemyRatio;

		if (startBorder <= seed && seed < endBorder) {
			type = Enum.MapType.Enemy;
		}

		startBorder = endBorder;
		endBorder += EliteRatio;
		if (startBorder <= seed && seed < endBorder) {
			type = Enum.MapType.Elite;
		}
		
		startBorder = endBorder;
		endBorder += TreasureRatio;
		if (startBorder <= seed && seed < endBorder) {
			type = Enum.MapType.Treasure;
		}
		
		startBorder = endBorder;
		endBorder += HealRatio;
		if (startBorder <= seed && seed < endBorder) {
			type = Enum.MapType.Heal;
		}

		return type;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		//FadeManager.Instance.FadeIn(FadeManager.Type.Mask, 0.5f, () => {
		//	LocalSceneManager.Instance.LoadScene(MapDataCarrier.Instance.NextSceneName, null);
		//});
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
