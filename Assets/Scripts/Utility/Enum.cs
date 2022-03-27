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
		Event,
		Max
	};
	
	public enum EffectType {
        None,
        Damage,			// 通常ダメージ
        DamageSuction,	// 与ダメ回復
        DamageShieldSuction,	// 与ダメシールド獲得
        DamageFinish,	// 使用した攻撃xNダメージ
        DamageDice,	// 現在のダイスコスト分の数値ダメージ
        ShieldBash,		// シールド値ダメージ
        DamageGainMaxHp,// 通常ダメージと同じだが、トドメをさした場合に最大HPが上昇する
        DamageMultiStrength,// 力に特別な補正をかけた数値分のダメージ
        DamageDiscardCount,// 破棄されているカード数xnダメージ
        DamageTotalSelfTrueDamage,	// 受けた自分の自確定ダメージxｎダメージ(TrueDamageでは無いので、注意)
        TrueDamage,		// 全てを無視した固定ダメージ
        SelfTrueDamage,		// 全てを無視した固定ダメージを自身に与える
        RemovePower,	// 状態変化全て解除
        Shield,			// シールド
        ShieldDouble,	// シールド倍化
        ShieldDamage,	// シールドのみに作用するダメージ
        StrengthShield,	// 力の数値分、シールドを得る
        Heal,			// 回復
        Warning,		// 警告（未行動）
        Stun,			// スタン（未行動）
        Death,			// 即死
		Curse,			// 呪い付与
		AddCard2Deck,	// カードを、一時的にデッキに加える。
		AddCard2Hand,	// カードを、一時的に手札に加える。
		AddCurse2Deck,	// 呪いを、一時的にデッキに加える。呪いもこれで扱う。
		AddCurse2Hand,	// 呪いを、一時的に手札に加える。呪いもこれで扱う。
		AddCurse2DeckEternal,	// 呪いを、恒久的にデッキに加える。
        Draw,			// 山札を引く
        GainDiceCost,	// 一時的なダイスコスト増加（減は、元々のダイスコスト下げればいいだけなので、システムとして使わないから実装しない
        Hand2DeckTop,	// 手札をデッキトップに戻す
        Hand2Trash,		// 手札を捨て札に送る
        Hand2Discard,	// 手札を破棄する
        Hand2Erase,		// 手札を削除
        HandCurseDiscard,	// 手札の呪いを全て破棄
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
        TurnShieldPreserve,	// ターン終了時に、シールドが無くならない。こっちは、ターン経過で1ずつ減少
        ShieldPreserve,	// ターン終了時に、シールドが無くならない。こっちは、解除されない限りは永続
        Invincible,		// 一度だけ、被ダメ0
        DoubleAttack,	// PlayerCalcDamageNormalDamageの処理を行う効果が含まれるカードを、追加で一度発動させる
        Cost6DoubleAttack,	// アーティファクト、コスト6以上のPlayerCalcDamageNormalDamageの処理を行うカードを一度だけ2回発動する
        Critical,		// 1度だけ、次に使用するPlayerCalcDamageNormalDamageの処理を行うカードの基本効果値のみ倍になる
        AddMaxDiceCost,	// その戦闘中のダイスコスト増減のバフ
        NonDraw,		// そのターンドロー不可（ドローが不可なだけで、もし山札や破棄札から加える効果が実装されたら、それは可能）
        HealCharge,		// この数値分、戦闘終了後にHP回復
        DoubleStrength,	// 現在の数値分力を倍化させ、増加した数値分力減少(ターン終了時減少するデバフ)を付与する
        DemonPower,	// 数値分、ターン開始時に力を得る。
		AddShieldTrueDamage, // シールド獲得時、相手に数値分の確定ダメージを与える
        TurnThorn,			// そのターンのみの反射ダメージ
        SupportShoot,		// カードを使用する度に、相手に〇確定ダメージ
        AfterImage,		// カードを使用する度に、1シールドを得る
        Activity,		// 次のターン開始時、数値分ダイスコスト加算
        Resist,		// 山札から呪いを引いた時に、追加数値分ドロー
		AddHandCurseDamage,// 手札に呪いが加わった時に、数値分ダメージ
		DiscardCurseHeal,// 呪いを破棄した時に、数値分HP回復
		DiscardShield,// カード破棄した時に、数値分シールドを得る
		SelfHarm,// ターン開始時に、手札に自傷を数値分加える
		CurseReturn,// 呪いを破棄する度、数値分相手に与ダメ低下と被ダメ上昇を与える
		DiscardDamage,// カード破棄した時に、数値分ダメージを与える
		AddSelfTrueDamageStrength,// 自確定ダメージ受けた時、力を獲得
		DrawSelfTrueDamage,// 自確定ダメージ受けた時、1枚ドロー
		AddSelfTrueDamageHealCharge,// 自確定ダメージ受けた時、回復チャージを1得る

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
		TurnShieldPreserve,
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
        DemonPower,
		AddShieldTrueDamage,
		SupportShoot,
		AfterImage,
		Resist,
		AddHandCurseDamage,
		DiscardCurseHeal,
		DiscardShield,
		SelfHarm,
		CurseReturn,
		DiscardDamage,
		AddSelfTrueDamageStrength,
		DrawSelfTrueDamage,
		AddSelfTrueDamageHealCharge,

		// ターン経過時に、1にリセットする物
        RotBody,
		
		// ターン終了時に0にするもの
		DoubleAttack,
		Cost6DoubleAttack,
        NonDraw,

		// ダイス振った後に0にするもの
		Activity,

		// ターン開始時に0にするもの
		TurnThorn,

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
		AddAnotherStrength1,// 力獲得時、追加で力を1獲得
		DamageAddStrength1,// ダメージを与える度、力と力減少を1得る。
		NoAttackGetTurnShieldPreserve, // このターン攻撃をしていなければ、ターン終了時鉄壁を1付与。
		Use3ShieldAddToughness, // ターン中にシールドを3回獲得した場合、守を1得る
		ReduseShieldLimit15, // ターン終了時のシールド減少量が-15になる
		ZeroHand1Draw, // 手札が0枚になった時、カードを1枚ドロー
		Under1CostGainDamage4, // 1コスト以下のカードのダメージ+4
		TurnStartDrawGain1, // ターン開始時、+1ドロー
		Use10Card1Draw, // カードを10枚使用する毎に、1ドロー
		Use10CardGain3Activity, // カードを10枚使用する毎に、活性を3得る。
		TurnStart1SelfTrueDamage2Shield, // ターン開始時、1自確定ダメージ、2シールドを得る
		ExtraAddHealCharge1, // 回復チャージを得る時、追加で1回復チャージを得る
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
	
	public enum CostType {
		None = 0,
		ReduceUseCardSheet,	// ターン中に使用したカードの枚数分、コストが下がる
		ReduceSelfTrueDamage,	// 戦闘中に受けた自確定ダメージ分、コストが下がる
	}
}
