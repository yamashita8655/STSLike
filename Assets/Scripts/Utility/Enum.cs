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
        DamageGainMaxHp,// 通常ダメージと同じだが、トドメをさした場合に最大HPが上昇する
        DamageMultiStrength,// 力に特別な補正をかけた数値分のダメージ
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
        DoubleAttack,	// PlayerCalcDamageNormalDamageの処理を行う効果が含まれるカードを、追加で一度発動させる
        Cost6DoubleAttack,	// アーティファクト、コスト6以上のPlayerCalcDamageNormalDamageの処理を行うカードを一度だけ2回発動する
        Critical,		// 1度だけ、次に使用するPlayerCalcDamageNormalDamageの処理を行うカードの基本効果値のみ倍になる
        AddMaxDiceCost,	// その戦闘中のダイスコスト増減のバフ
        NonDraw,		// そのターンドロー不可（ドローが不可なだけで、もし山札や破棄札から加える効果が実装されたら、それは可能）
        HealCharge,		// この数値分、戦闘終了後にHP回復

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
        HealCharge,	// その戦闘終了後に数値分回復
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
        NonDraw,

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
		Revive = 0,	// 復活
		UsedRevive,// 復活使用済み
		UpgradeReward,// TODO 戦闘報酬グレードアップだけど、効果変える予定
		DiceShield,// サイコロの出ためだけシールド獲得
		RestUp1,// 休憩回復量上昇
		RestUp2,// 休憩回復量上昇
		RestUp3,// 休憩回復量上昇
		AntiCurse,// TODO 呪いにかからないだったけど、修正（今どーなってんだろ
		ApprenticeKnight,// 攻撃と防御のみの効果のカードの、効果量+3
		KnightMaster,// TODO 修正予定
		AddVersak,// 毎ターン開始時、狂戦士1付与
		AddMaxHp1,// 最大HP上昇
		AddMaxHp2,// 最大HP上昇
		AddMaxHp3,// 最大HP上昇
		AddMaxHp4,// 最大HP上昇
		WeaknessUp,// 相手の与ダメ低下の効果量上昇
		VulnerableUp,// 相手に被ダメ上昇の効果量上昇
		Minimalist,// 報酬スキップ時、最大HP+1
		SupportFire,// ターン終了時、相手にダメージ
		AntiWeakness,// 自分の与ダメ低下の効果量減少
		AntiShieldWeakness,// 自分のシールド減少の効果量減少
		ShieldOne,// 1ターン目開始時、シールド
		ShieldTwo,// 2ターン目開始時、シールド
		ShieldThree,// 3ターン目開始時、シールド
		UseCurseShield,// 呪いカード使用時、シールド獲得
		FirstAidKit,// 戦闘終了時、最大HPの半分以下なら、HP回復
		GodBless,// 全てのダメージを受ける時に、1減少
		SeekersAmulet,// 戦闘終了時、最大HP+1
		Momonga,// 獲得時、全ての難易度カードが5変わる
		UsedMomonga,// 使用済みモモンガ
		AssassinRod,// 4以下のダメージが4になる
		HeroSword,// 戦闘開始時HPが最大だった場合、力、守を3得る。
		HeroShield,// 戦闘開始時HPが20以下だった場合、再生を10得る。
		AddDoubleAttack6,// ターン開始時、ダブルアタック6を1得て、ターン終了時にダブルアタック6を1失う。
		ZeroTurnEndShield,// ターン終了時にシールドがなければ、6シールド得る
		DyingAddVersak,// ターン開始時に、HP20以下なら、狂戦士1付与
		GameStart3Draw,// 戦闘開始時に、追加3ドロー
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
