using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
	[SerializeField]
	private Text DebugText = null;

	[SerializeField]
	private GameObject LogObject = null;
	
	[SerializeField]
	private Dropdown FindCardDropDown = null;
	
	[SerializeField]
	private InputField AddPointInputField = null;

	private int LogIndex = 1;

	public void Initialize()
	{
		DebugManager.Instance.CloseDebug();
		LogObject.SetActive(false);
		
		FindCardDropDown.ClearOptions();
		List<string> options = new List<string>();
		for (int i = 1; i < 6; i++) {
			var list = MasterAction2Table.Instance.GetRarityCardCloneList(i);
			for (int i2 = 0; i2 < list.Count; i2++) {
				options.Add(list[i2].ToString());
			}
		}
		FindCardDropDown.AddOptions(options);
	}

	public void OnClickOpenButton()
	{
		DebugManager.Instance.OpenDebug();
	}

	public void OnClickCloseButton()
	{
		DebugManager.Instance.CloseDebug();
	}

	public void OnClickOpenLogButton()
	{
		LogObject.SetActive(true);
	}

	public void OnClickCloseLogButton()
	{
		LogObject.SetActive(false);
	}
	
	public void OnClickClearLogButton()
	{
		DebugText.text = "";
	}

	public void UpdateDebugLog(string addText)
	{
		// TODO 文字数が15000文字だかを超えると、エラーが出るので、念頭に入れておくこと
		DebugText.text += LogIndex + ":" + addText + "\n";
		LogIndex++;
	}
	
	public void OnClickCardFindAllButton()
	{
		for (int i = 1; i < 6; i++) {
			var list = MasterAction2Table.Instance.GetRarityCardCloneList(i);
			for (int i2 = 0; i2 < list.Count; i2++) {
				PlayerPrefsManager.Instance.SaveFindCardId(list[i2]);
			}
		}
	}
	
	public void OnClickCardFindIdButton()
	{
		int id = int.Parse(FindCardDropDown.options[FindCardDropDown.value].text);
		PlayerPrefsManager.Instance.SaveFindCardId(id);
	}
	
	public void OnClickAddPointButton()
	{
		int point = int.Parse(AddPointInputField.text);
		PlayerPrefsManager.Instance.AddPoint(point);
	}
}
