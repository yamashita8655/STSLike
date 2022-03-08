using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Const {
	public static readonly int BaseRegularCardMaxCost = 1;
	public static readonly int MaxPoint = 99999999;
	public static readonly int MaxHand = 20;
	public static readonly int DrawCount = 6;

	public static readonly string RarityFrameImagePath = "Image/UI/Card/cardframe{0}";
	public static readonly string AttackButtonImagePath = "Image/UI/Map/attackbuttonframe{0}";
	public static readonly string ArtifactButtonPath = "Prefab/UI/ArtifactButtonItem";
	public static readonly string NotFoundImagePath = "Image/UI/Menu/notfound";
	public static readonly string RegularSettingCardContentItemPath = "Prefab/UI/RegularSettingCardContentItem";
	public static readonly string CardContentItemPath = "Prefab/UI/CardContentItem";
	
	public static readonly string UseTypeDiscardImagePath = "Image/UI/Map/discardcard";
	public static readonly string UseTypeEraseImagePath = "Image/UI/Map/erasecard";

	public static readonly string ValueItemPath = "Prefab/UI/ValueItem";
	public static readonly string BattleCardButtonItemPath = "Prefab/UI/BattleCardButtonItem";
	public static readonly string RegularCardButtonItemPath = "Prefab/UI/RegularCardButtonItem";

	public static readonly string DamageImagePath = "Image/UI/Card/damage";
	public static readonly string TrueDamageImagePath = "Image/UI/Card/truedamage";
	public static readonly string ShieldImagePath = "Image/UI/Card/shield";
	public static readonly string HealImagePath = "Image/UI/Card/heal";
	public static readonly string ShieldBreakImagePath = "Image/UI/Card/shieldbreak";
	public static readonly string WarningImagePath = "Image/UI/Card/warning";
	public static readonly string StunImagePath = "Image/UI/Card/stun";
	public static readonly string DeathImagePath = "Image/UI/Card/death";
	public static readonly string PowerImagePath = "Image/UI/Card/power";
	public static readonly string DebuffImagePath = "Image/UI/Card/debuff";
	public static readonly string DrawImagePath = "Image/UI/Card/draw";
	public static readonly string GainDiceCostImagePath = "Image/UI/Card/gaindicecost";
	public static readonly string Hand2DeckTopImagePath = "Image/UI/Card/hand2decktop";
	public static readonly string Hand2TrashImagePath = "Image/UI/Card/hand2trash";
	public static readonly string Hand2DiscardImagePath = "Image/UI/Card/hand2discard";
	public static readonly string Hand2EraseImagePath = "Image/UI/Card/hand2erase";
	
	// こっちは、数値可変でターン永続
	public static readonly string PowerControllerPath = "Prefab/UI/PowerController";
	public static readonly string StrengthImagePath = "Image/UI/Map/strength";
	public static readonly string FastStrengthImagePath = "Image/UI/Map/faststrength";
	public static readonly string ToughnessImagePath = "Image/UI/Map/toughness";
	public static readonly string RegenerateImagePath = "Image/UI/Map/regenerate";
	public static readonly string PoisonImagePath = "Image/UI/Map/poison";
	public static readonly string ThornImagePath = "Image/UI/Map/thorn";
	public static readonly string RotBodyImagePath = "Image/UI/Map/rotbody";
	public static readonly string VersakImagePath = "Image/UI/Map/versak";
	public static readonly string ReactiveShieldImagePath = "Image/UI/Map/reactiveshield";
	public static readonly string AddMaxDiceCostImagePath = "Image/UI/Map/addmaxdicecost";
	public static readonly string HealChargeImagePath = "Image/UI/Map/healcharge";
	public static readonly string DemonPowerImagePath = "Image/UI/Map/demonpower";
	public static readonly string AddShieldTrueDamageImagePath = "Image/UI/Map/addshieldtruedamage";

	// こっちは、数値固定でターン可変
	public static readonly string TurnPowerControllerPath = "Prefab/UI/TurnPowerController";
	public static readonly string DiceMinusOneImagePath = "Image/UI/Map/diceminusone";
	public static readonly string ReverseHealImagePath = "Image/UI/Map/reverseheal";
	public static readonly string WeaknessImagePath = "Image/UI/Map/weakness";
	public static readonly string ShieldWeaknessImagePath = "Image/UI/Map/shieldweakness";
	public static readonly string VulnerableImagePath = "Image/UI/Map/vulnerable";
	public static readonly string PatientImagePath = "Image/UI/Map/patient";
	public static readonly string AutoShieldImagePath = "Image/UI/Map/autoshield";
	public static readonly string SubStrengthImagePath = "Image/UI/Map/substrength";
	public static readonly string ShieldPreserveImagePath = "Image/UI/Map/shieldpreserve";
	public static readonly string TurnShieldPreserveImagePath = "Image/UI/Map/turnshieldpreserve";
	public static readonly string InvincibleImagePath = "Image/UI/Map/invincible";
	public static readonly string DoubleAttackImagePath = "Image/UI/Map/doubleattack";
	public static readonly string Cost6DoubleAttackImagePath = "Image/UI/Map/cost6doubleattack";
	public static readonly string TurnRegenerateImagePath = "Image/UI/Map/turnregenerate";
	public static readonly string CriticalImagePath = "Image/UI/Map/critical";
	public static readonly string NonDrawImagePath = "Image/UI/Map/nondraw";
}
