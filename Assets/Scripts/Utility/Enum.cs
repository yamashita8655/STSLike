using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnumSelf : MonoBehaviour {

	public enum MapType {
		Enemy = 0,
		Elite,
		Boss,
		Heal,
		Treasure,
		Max
	};
	
	public enum EffectType {
        None,
        Damage,			// 通常ダメージ
        DamageSuction,	// 与ダメ回復
        DamageShieldSuction,	// 与ダメシールド獲得
        ShieldBash,		// シールド値ダメージ
        TrueDamage,		// 全てを無視した固定ダメージ
        RemovePower,	// 状態変化全て解除
        Shield,			// シールド
        ShieldDouble,	// シールド倍化
        ShieldDamage,	// シールドのみに作用するダメージ
        Heal,			// 回復
        Warning,		// 警告（未行動）
        Stun,			// スタン（未行動）
        Death,			// 即死
		Curse,			// 呪い付与
        Draw,			// 山札を引く
        GainDiceCost,	// 一時的なダイスコスト増加（減は、元々のダイスコスト下げればいいだけなので、システムとして使わないから実装しない
        Hand2DeckTop,	// 手札をデッキトップに戻す
        Hand2Trash,		// 手札を捨て札に送る
        Hand2Discard,	// 手札を破棄する
        Hand2Erase,		// 手札を削除
		// バフ、デバフ
        Strength,		// 力上昇
        FastStrength,	// ターン終了後に下がる力上昇
        Toughness,		// 守り上昇
        DiceMinusOne,	// サイコロ1減少
        Regenerate,		// ターン終了時に減少しない回復
        ReverseHeal,	// 回復効果反転
        Poison,			// 毒
        Weakness,		// 与ダメ低下
        Vulnerable,		// 被ダメ上昇
        ShieldWeakness,	// シールド獲得減少
        TurnRegenerate,	// ターン終了時に減少する回復
        Patient,		// 一定ダメージ受けたら、AI変更（やせ我慢）
        AutoShield,		// 自動盾
        Thorn,			// 反射ダメージ
        RotBody,		// 朽ちた体（与ダメが数値ずつ上がる）
        Versak,			// 与ダメ2倍
        ReactiveShield,	// 被ダメ時、シールド獲得
        SubStrength,	// FastStrengthによる筋力減少
        ShieldPreserve,	// ターン終了時に、シールドが無くならない
        Invincible,		// 一度だけ、被ダメ0
        DoubleAttack,	// 選択Damage効果が含まれるカードを、追加で一度発動させる
        Cost6DoubleAttack,	// アーティファクト、コスト6以上のカードを一度だけ2回発動する
        Critical,		// 1度だけ、次に使用するカードのDamage、DamageSuction、ShieldBashの基本効果値のみ倍になる
        AddMaxDiceCost,	// その戦闘中のダイスコスト増減のバフ

		// デバッグ用状態変化
        DebugDisaster,
		Max
	};
	
	public enum TargetType {
        None,
        Self,
		Opponent,
		Max
	};
	
	public enum TimingType {
        None,
		BattleStart,
		TurnStart,
		BeforeAdd,
		Add,
		AfterAdd,
		TurnEnd,
		SelfDeath,
		OpponentDeath,
		BattleEnd,
		Max
	};

	public enum EnemyActionType {
		None,
		Random,
		Rotation
	}
	
	// TODO これ、マスターデータで設定しないから、どこかにどっちのタイプに属するのか
	// 書いておかないとダメだな…。
	public enum PowerKind {
		Power = 0,
		TurnPower,
	}

	
	//
	public enum PowerType {
		None = -1,
		Strength = 0,
		FastStrength,
		Toughness,
		Regenerate,// TODO これも、マイナス値が無いから、TurnPowerなのでは…
		Poison,// TODO これも、マイナス値が無いから、TurnPowerなのでは…
        AddMaxDiceCost,	// その戦闘中のダイスコスト増減のバフ
		Max
	}
	
	// TODO こっちの定義、マイナス値が存在しない物、というくくりにした方が良さそう…。
	public enum TurnPowerType {
		None = -1,
		DiceMinusOne = 0,
		ReverseHeal,
		Weakness,
		ShieldWeakness,
        Vulnerable,
		ShieldPreserve,
		TurnRegenerate,

		// ターン経過時に変動しない物(他の条件で減少する物、もしくはしない物)
		Patient,
		AutoShield,
		Thorn,
		Versak,
		ReactiveShield,
		SubStrength,
		Invincible,
        Critical,

		// ターン経過時に、1にリセットする物
        RotBody,
		
		// ターン終了時に0にするもの
		DoubleAttack,
		Cost6DoubleAttack,
		Max
	}
	
	public enum AIChangeType {
		None = -1,
		TurnProgress = 0,// 指定されたターンが経過したら、指定のAIIDに遷移する。基のIDに戻る際も、Rotationで2個セットして、2ターンと決め打ちすれば、これを使って実現可能
		HpBorder,// 指定されたHp以下になったら、指定のAIIDに遷移する。基のIDに戻ると、またHp減った時にHpボーダーを参照するので、同じ行動をするHpボーダーを条件としないAIを作って遷移した方が良い。ちなみに、殴った瞬間AI変わるのは、相手がスタンするならまだしも、理不尽過ぎるので、通常は下記の行動抽選時に行う方が、猶予を与えられて良き
		LotHpBorder,// 敵抽選開始時に指定されたHp以下なら、指定のAIIDに遷移する。基のIDに戻ると、またHp減った時にHpボーダーを参照するので、同じ行動をするHpボーダーを条件としないAIを作って遷移した方が良い。
		Max
	}

	public enum ArtifactEffectType {
		None = -1,
		ExecuteAction = 0,
		AddParameter,
	}
	
	
	// これは、ユニークにする。同じ効果タイプを持つものは複数設定しない
	// 同じ効果を実装したければ、別の定義で同じ効果として合算なり、別々で処理をするなりで対処する
	public enum ParameterType {
		None = -1,
		Revive = 0,
		UsedRevive,
		UpgradeReward,
		DiceShield,
		RestUp1,
		RestUp2,
		RestUp3,
		AntiCurse,
		ApprenticeKnight,
		KnightMaster,
		AddVersak,
		AddMaxHp1,
		AddMaxHp2,
		AddMaxHp3,
		AddMaxHp4,
		WeaknessUp,
		VulnerableUp,
		Minimalist,
		SupportFire,
		AntiWeakness,
		AntiShieldWeakness,
		ShieldOne,
		ShieldTwo,
		ShieldThree,
		UseCurseShield,
		FirstAidKit,
		GodBless,
		SeekersAmulet,
		Momonga,
		UsedMomonga,
		DummyPower,
		AssassinRod,
		HeroSword,
		HeroShield,
		AddDoubleAttack6,
		Award,
		Max,
	}
	
	public enum ContentStatusType {
		NotFound = 0,	// 未発見
		Lock,			// 未解放
		Unlock,			// 解放済み
	}
	
	public enum UseType {
		Repeat = 0,	// 繰り返し使える
		Discard,	// 破棄される
		Erase,		// 破棄にも登録されない
	}
}
