using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItemBase
{
    public MasterEquipItemDataTable.Data EquipItemData { get; private set; }
    public Parameter BaseParameter { get; private set; }
    public Image Icon { get; private set; }

    public EquipItemBase(
        MasterEquipItemDataTable.Data equipItemData,
        Parameter baseParameter,
        Image icon
    )
    {
        EquipItemData = equipItemData;
        BaseParameter = baseParameter;
        Icon = icon;
    }
}
