using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueItemWrapper
{
    public int UniqueId { get; private set; }
    public EquipItemBase Item { get; private set; }

    public UniqueItemWrapper(int uId, EquipItemBase item)
    {
        UniqueId = uId;
        Item = item;
    }
}
