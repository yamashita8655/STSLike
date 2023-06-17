using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCardController : MonoBehaviour
{
	[SerializeField]
	private Text Name = null;
	
	[SerializeField]
	private GameObject ValueObjectRoot = null;
	
	[SerializeField]
	private SelectCardAnimationController CuAnimationController = null;
	
    private MasterAction2Table.Data Data = null;

	private List<ValueController> ValueControllers = new List<ValueController>();

	private List<ActionPack> ActionPackList = null;
	
	private bool CanAnimationFlag = false;

	private bool CanDestroyFlag = false;
	
	private Action AnimationEndCallback = null;
	private Action HitCallback = null;

	public IEnumerator Initialize(
		MasterAction2Table.Data data
	) {
		Data = data;

		// ValueObjectの生成
		// とりあえず、10個生成
		int loadCount = 10;
		int loadedCount = 0;
		for (int i = 0; i < loadCount; i++) {
			ResourceManager.Instance.RequestExecuteOrder(
				Const.ValueItemPath,
				ExecuteOrder.Type.GameObject,
				gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					var ctrl = obj.GetComponent<ValueController>();
					ctrl.Initialize(ValueObjectRoot);
					ValueControllers.Add(ctrl);
					loadedCount++;
				}
			);
		}

		while (loadCount != loadedCount) {
			yield return null;
		}

		UpdateDisplay();

		// 即演出だと見栄えが悪いので、ちょい待つ
		yield return new WaitForSeconds(0.5f);
		
		CanAnimationFlag = true;
	}

	private void UpdateDisplay() {
		Name.text = Data.Name;

		// 一回全て非表示
		for (int i = 0; i < ValueControllers.Count; i++) {
			ValueControllers[i].Hide();
		}

		ActionPackList = new List<ActionPack>(Data.ActionPackList);
		int index = 0;
		for (index = 0; index < ActionPackList.Count; index++) {
			if (index >= 10) {
				LogManager.Instance.LogError("BattleCardButtonController:UpdateDisplay:添え字10以上になってる:" + Data.Name);
			}
			
			int val = ActionPackList[index].Value;
			// これ、BattleCardButtonControllerの処理と共通化しないとアカンカモね…
			int originalVal = ActionPackList[index].Value;
			if (
				(ActionPackList[index].Effect == EnumSelf.EffectType.Damage) ||
				(ActionPackList[index].Effect == EnumSelf.EffectType.DamageSuction) ||
				(ActionPackList[index].Effect == EnumSelf.EffectType.DamageShieldSuction) ||
				(ActionPackList[index].Effect == EnumSelf.EffectType.DamageGainMaxHp) ||
				(ActionPackList[index].Effect == EnumSelf.EffectType.ShieldBash)
			) {
				val = BattleCalculationFunction.CalcPlayerDamageValue(ActionPackList[index]);
			} else if (
				(ActionPackList[index].Effect == EnumSelf.EffectType.DamageMultiStrength) ||
				(ActionPackList[index].Effect == EnumSelf.EffectType.DamageDiscardCount) ||
				(ActionPackList[index].Effect == EnumSelf.EffectType.DamageTotalSelfTrueDamage) ||
				(ActionPackList[index].Effect == EnumSelf.EffectType.DamageFinish)
			) {
				val = BattleCalculationFunction.CalcPlayerDamageValue(ActionPackList[index]);
				originalVal = 0;
			} else if (ActionPackList[index].Effect == EnumSelf.EffectType.DamageDice) {
				val = BattleCalculationFunction.CalcPlayerDamageValue(ActionPackList[index]);
				originalVal = MapDataCarrier.Instance.CurrentTotalDiceCost;
			} else if (
				(ActionPackList[index].Effect == EnumSelf.EffectType.Shield) ||
				(ActionPackList[index].Effect == EnumSelf.EffectType.StrengthShield)
			) {
				val = BattleCalculationFunction.CalcPlayerShieldValue(ActionPackList[index]);
			}
			ValueControllers[index].UpdateDisplay(
				ActionPackList[index].Effect,
				originalVal,
				val
			);
		}
	}

	public void PlayAnimation(Action hitCallback, Action animationEndCallback)
	{
		AnimationEndCallback = animationEndCallback;
		HitCallback = hitCallback;

		var action = ActionPackList[0];
		ActionPackList.RemoveAt(0);
		if (
			(action.Effect == EnumSelf.EffectType.Damage) ||
			(action.Effect == EnumSelf.EffectType.DamageSuction) ||
			(action.Effect == EnumSelf.EffectType.DamageShieldSuction) ||
			(action.Effect == EnumSelf.EffectType.DamageGainMaxHp) ||
			(action.Effect == EnumSelf.EffectType.ShieldBash)
		) {
			CuAnimationController.Play("Jump", HitCheck, EndCheck);
		} else if (
			(action.Effect == EnumSelf.EffectType.DamageMultiStrength) ||
			(action.Effect == EnumSelf.EffectType.DamageDiscardCount) ||
			(action.Effect == EnumSelf.EffectType.DamageTotalSelfTrueDamage) ||
			(action.Effect == EnumSelf.EffectType.DamageFinish)
		) {
			CuAnimationController.Play("Jump", HitCheck, EndCheck);
		} else if (action.Effect == EnumSelf.EffectType.DamageDice) {
			CuAnimationController.Play("Jump", HitCheck, EndCheck);
		} else if (
			(action.Effect == EnumSelf.EffectType.Shield) ||
			(action.Effect == EnumSelf.EffectType.StrengthShield)
		) {
			CuAnimationController.Play("Scale", HitCheck, EndCheck);
		} else {
			// TODO 後でちゃんと種類ごとにアニメーション分けないとね…
			CuAnimationController.Play("Scale", HitCheck, EndCheck);
		}
	}

	private void HitCheck()
	{
		if (HitCallback != null)
		{
			HitCallback();
		}
	}

	private void EndCheck()
	{
		GameObject.Destroy(ValueControllers[0].gameObject);
		ValueControllers.RemoveAt(0);
		if (AnimationEndCallback != null)
		{
			AnimationEndCallback();
		}

		if (ActionPackList.Count > 0)
		{
			//PlayAnimation();
		}
		else
		{
			CanDestroyFlag = true;
		}
	}

	public bool CanDestroy()
	{
		return CanDestroyFlag;
	}
	
	public bool CanAnimation()
	{
		return CanAnimationFlag;
	}

	public MasterAction2Table.Data GetData() {
		return Data;
	}
}
