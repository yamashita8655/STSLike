using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Const {
	public static readonly string RarityFrameImagePath = "Image/UI/Card/cardframe{0}";


	public static readonly string ValueItemPath = "Prefab/UI/ValueItem";

	public static readonly string DamageImagePath = "Image/UI/Card/damage";
	public static readonly string ShieldImagePath = "Image/UI/Card/shield";
	public static readonly string HealImagePath = "Image/UI/Card/heal";
	public static readonly string ShieldBreakImagePath = "Image/UI/Card/shieldbreak";
	public static readonly string PowerImagePath = "Image/UI/Card/power";
	public static readonly string DebugImagePath = "Image/UI/Card/debuff";
	
	// こっちは、数値可変でターン永続
	public static readonly string PowerControllerPath = "Prefab/UI/PowerController";
	public static readonly string StrengthImagePath = "Image/UI/Map/strength";

	// こっちは、数値固定でターン可変
	public static readonly string TurnPowerControllerPath = "Prefab/UI/TurnPowerController";
	public static readonly string DiceMinusOneImagePath = "Image/UI/Map/diceminusone";
}
