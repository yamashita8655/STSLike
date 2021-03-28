using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDialogController : MonoBehaviour
{
	[SerializeField]
	private GameObject DialogRootObject = null;
	
	[SerializeField]
	private Text HeaderText = null;
	
	[SerializeField]
	private GameObject ContentRoot = null;

	private List<GameObject> IconObjectList = new List<GameObject>();

	private Action<int> SellButtonCallback = null;

	public void Open(List<EquipItemBase> huntedItemList, Action<int> sellButtonCallback) {
		HeaderText.text = "インベントリ";

		SellButtonCallback = sellButtonCallback;

		UpdateIcon(huntedItemList);
	}

	private void UpdateIcon(List<EquipItemBase> huntedItemList)
	{
		for (int i = 0; i < IconObjectList.Count; i++) {
			IconObjectList[i].SetActive(false);
		}

		if (huntedItemList.Count == 0) {
			DialogRootObject.SetActive(true);
		} else {
			int loadCount = 0;
			int needCount = huntedItemList.Count - IconObjectList.Count;

			if ((needCount == 0) || (needCount < 0)) {
				SetItemIcon(huntedItemList);
			} else {
				for (int i = 0; i < needCount; i++) {
					ResourceManager.Instance.RequestExecuteOrder(
						"Prefab/UI/ItemToggle",
						ExecuteOrder.Type.GameObject,
						this.gameObject,
						(go) => {
							GameObject iconObject = GameObject.Instantiate(go) as GameObject;
							IconObjectList.Add(iconObject);
							iconObject.transform.SetParent(ContentRoot.transform);
							iconObject.transform.localPosition = Vector3.zero;
							iconObject.transform.localScale = Vector3.one;

							loadCount++;
							if (needCount == loadCount) {
								SetItemIcon(huntedItemList);
							}
						}
					);
				}
			}
		}
	}

	private void SetItemIcon(List<EquipItemBase> huntedItemList)
	{
		int count = 0;
		for (int i = 0; i < huntedItemList.Count; i++) {
			int index = i;
			string path = huntedItemList[index].EquipItemData.ImagePath;
			ResourceManager.Instance.RequestExecuteOrder(
				path,
				ExecuteOrder.Type.Sprite,
				this.gameObject,
				(sprite) => {
					IconObjectList[index].GetComponent<ItemToggle>().Initialize(null, sprite as Sprite, huntedItemList[index]);
					IconObjectList[index].SetActive(true);
					count++;
				}
			);
		}
		DialogRootObject.SetActive(true);
	}

	public void Close() {
		DialogRootObject.SetActive(false);
	}

	public void OnClickSellButton()
	{
		string saveString = "";
		List<EquipItemBase> huntedItemList = new List<EquipItemBase>();

		int index = 0;
		int money = 0;
		while (index < IconObjectList.Count) {
			if (IconObjectList[index].activeSelf == false) {
				index++;
				continue;
			}
			var data = IconObjectList[index].GetComponent<ItemToggle>();
			bool isOn = data.IsOn();
			if (isOn == true) {
				money += 1;
			} else {
				huntedItemList.Add(data.Data);
				if (string.IsNullOrEmpty(saveString) == true) {
					saveString += data.Data.EquipItemData.Id.ToString();
				} else {
					saveString += ("," + data.Data.EquipItemData.Id.ToString());
				}
			}
			index++;
		}

		PlayerPrefsManager.Instance.SaveParameter(PlayerPrefsManager.SaveType.Inventory, saveString);
		UpdateIcon(huntedItemList);

		money += int.Parse(PlayerPrefsManager.Instance.GetMoney());
		SellButtonCallback(money);
		PlayerPrefsManager.Instance.SaveMoney(money);
	}
}
