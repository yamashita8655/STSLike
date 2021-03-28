using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HuntedItemDialogController : MonoBehaviour
{
	[SerializeField]
	private GameObject DialogRootObject = null;
	
	[SerializeField]
	private Text HeaderText = null;
	
	[SerializeField]
	private GameObject ContentRoot = null;

	private List<GameObject> IconObjectList = new List<GameObject>();

    private List<EquipItemBase> StackItemList = new List<EquipItemBase>();

    private bool IsItemIconCreate = false;

    public void Open() {
        HeaderText.text = "獲得アイテム";
        DialogRootObject.SetActive(true);
    }

    void Update()
    {
        if (IsItemIconCreate == false) {
            if (StackItemList.Count > 0) {
                IsItemIconCreate = true;
                List<EquipItemBase> closeList = new List<EquipItemBase>(StackItemList);
                UpdateItemIcons(closeList);
                StackItemList.Clear();
            }
        }
    }

    public void AddStackItemList(List<EquipItemBase> huntedItemList)
    {
        for (int i = 0; i < huntedItemList.Count; i++) {
            StackItemList.Add(huntedItemList[i]);
        }
    }

    public void UpdateItemIcons(List<EquipItemBase> huntedItemList)
    {
        int loadCount = 0;
        int loadedCount = IconObjectList.Count;

        if (huntedItemList.Count == 0) {
			SetItemIcon(huntedItemList, loadedCount);
		} else {
			for (int i = 0; i < huntedItemList.Count; i++) {
				ResourceManager.Instance.RequestExecuteOrder(
					"Prefab/UI/ItemIcon",
					ExecuteOrder.Type.GameObject,
                    this.gameObject,
					(go) => {
						GameObject iconObject = GameObject.Instantiate(go) as GameObject;
						IconObjectList.Add(iconObject);
						iconObject.transform.SetParent(ContentRoot.transform);
						iconObject.transform.localPosition = Vector3.zero;
						iconObject.transform.localScale = Vector3.one;

						loadCount++;
						if (huntedItemList.Count == loadCount) {
							SetItemIcon(huntedItemList, loadedCount);
						}
					}
				);
			}
		}
    }

    private void SetItemIcon(List<EquipItemBase> huntedItemList, int loadedCount)
	{
        int count = 0;
        int addIndex = loadedCount;

        for (int i = 0; i < huntedItemList.Count; i++) {
			int index = i;
			string path = huntedItemList[index].EquipItemData.ImagePath;
			ResourceManager.Instance.RequestExecuteOrder(
				path,
				ExecuteOrder.Type.Sprite,
                this.gameObject,
				(sprite) => {
					IconObjectList[index+(addIndex)].GetComponent<ItemIcon>().Initialize(null, sprite as Sprite, (icon) => {});
                    count++;
                    if (count == huntedItemList.Count) {
                        IsItemIconCreate = false;
                    }
				}
			);
		}
	}

	public void Close() {
		DialogRootObject.SetActive(false);
	}
}
