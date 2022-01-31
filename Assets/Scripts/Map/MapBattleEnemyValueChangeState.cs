using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleEnemyValueChangeState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
		MasterAction2Table.Data data = enemy.GetActionData2();

		int count = MapDataCarrier.Instance.EnemyActionPackCount;
		ActionPack pack = data.ActionPackList[count]; 

		if (pack.Effect == EnumSelf.EffectType.Damage) {
			CalcDamageNormalDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Heal) {
			CalcHeal(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Shield) {
			CalcShield(pack);
		}

		MapDataCarrier.Instance.EnemyActionPackCount++;

		scene.UpdateParameterText();

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleCheck);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
	
	private void CalcDamageNormalDamage(ActionPack pack) {
		int shield = 0;
		int overDamage = 0;
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			shield = MapDataCarrier.Instance.CuPlayerStatus.GetNowShield();
			MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(-pack.Value);
			overDamage = shield - pack.Value;
			if (overDamage < 0) {
				MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(overDamage);
			}
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			shield = MapDataCarrier.Instance.CuEnemyStatus.GetNowShield();
			MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(-pack.Value);
			overDamage = shield - pack.Value;
			if (overDamage < 0) {
				MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(overDamage);
			}
		}
	}
	
	private void CalcHeal(ActionPack pack) {
		int heal = pack.Value;
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(heal);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(heal);
		}
	}
	
	private void CalcShield(ActionPack pack) {
		int shield = pack.Value;
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(shield);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(shield);
		}
	}
}
