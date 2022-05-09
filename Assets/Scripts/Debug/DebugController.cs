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

    [SerializeField]
    private Dropdown FindArtifactDropDown = null;

    [SerializeField]
    private Dropdown EnemyKillCountDropDown = null;

    [SerializeField]
    private InputField EnemyKillCountInputField = null;

    [SerializeField]
    private InputField HealCountInputField = null;

    [SerializeField]
    private InputField DiceCostUpCountInputField = null;

    [SerializeField]
    private InputField EraseCountInputField = null;

    private int LogIndex = 1;

	public void Initialize()
	{
		DebugManager.Instance.CloseDebug();
		LogObject.SetActive(false);
		
		FindCardDropDown.ClearOptions();
		List<string> options = new List<string>();
		for (int i = 1; i < 6; i++) {
			var list = MasterAction2Table.Instance.GetAllRarityCardCloneList(i);
			for (int i2 = 0; i2 < list.Count; i2++) {
				options.Add(list[i2].ToString());
			}
		}
		FindCardDropDown.AddOptions(options);

        FindArtifactDropDown.ClearOptions();
        options = new List<string>();
        for (int i = 1; i < 6; i++) {
            var list = MasterArtifactTable.Instance.GetRarityArtifactCloneList(i);
            for (int i2 = 0; i2 < list.Count; i2++) {
                options.Add(list[i2].ToString());
            }
        }
        FindArtifactDropDown.AddOptions(options);

        EnemyKillCountDropDown.ClearOptions();
        options = new List<string>();
        var dict = MasterEnemyTable.Instance.GetCloneDict();
        foreach (var data in dict) {
            options.Add(data.Key.ToString());
        }
        EnemyKillCountDropDown.AddOptions(options);
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
			var list = MasterAction2Table.Instance.GetAllRarityCardCloneList(i);
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

    public void OnClickArtifactFindAllButton()
    {
        for (int i = 1; i < 6; i++) {
            var list = MasterArtifactTable.Instance.GetRarityArtifactCloneList(i);
            for (int i2 = 0; i2 < list.Count; i2++) {
                PlayerPrefsManager.Instance.SaveFindArtifactId(list[i2]);
            }
        }
    }

    public void OnClickArtifactFindIdButton()
    {
        int id = int.Parse(FindArtifactDropDown.options[FindArtifactDropDown.value].text);
        PlayerPrefsManager.Instance.SaveFindArtifactId(id);
    }

    public void OnClickAddPointButton()
	{
		int point = int.Parse(AddPointInputField.text);
		PlayerPrefsManager.Instance.AddPoint(point);
	}

    public void OnClickAddEnemyKillCountButton()
    {
        int count = int.Parse(EnemyKillCountInputField.text);
        PlayerPrefsManager.Instance.AddEnemyKillCount(int.Parse(EnemyKillCountDropDown.options[EnemyKillCountDropDown.value].text), count);

		int id = int.Parse(EnemyKillCountDropDown.options[EnemyKillCountDropDown.value].text);
		int now = PlayerPrefsManager.Instance.GetEnemyKillCount(id);

        UpdateDebugLog($"{EnemyKillCountDropDown.options[EnemyKillCountDropDown.value].text}：{now}");
    }
    
	public void OnClickAddHealCountButton()
    {
        int count = int.Parse(HealCountInputField.text);
        PlayerPrefsManager.Instance.AddHealCount(count);
    }
	
	public void OnClickAddDiceCostUpCountButton()
    {
        int count = int.Parse(DiceCostUpCountInputField.text);
        PlayerPrefsManager.Instance.AddDiceCostUpCount(count);
    }
	
	public void OnClickAddEraseCountButton()
    {
        int count = int.Parse(EraseCountInputField.text);
        PlayerPrefsManager.Instance.AddEraseCount(count);
    }
}
