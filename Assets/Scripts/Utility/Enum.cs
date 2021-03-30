using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enum : MonoBehaviour {
	public enum Bgm {
		Title = 0,
		Home,
		InGame,
		None
	};
	
	public enum Se {
		Effect_CountDownEffect = 0,
		Effect_EndEffect,
		Home_InGameButtonSelect,
		InGame_FlickFailure,
		InGame_FlickSuccess,
		InGame_QuestionFailure,
		InGame_QuestionSuccess,
		InGame_SelectBomb,
		Result_ResultJingle,
		Title_TitleStart,
		UI_ButtonCancel,
		UI_ButtonOk,
	};

	public enum MapType {
		Enemy = 0,
		Elite,
		Boss,
		Treasure,
		Heal,
		Max
	};
	
	public enum ActionType {
        None,
        AddDamage,
		Max
	};
}
