using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuesCB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost()));
        NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost()));
        NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost())); NewValue(ValueLootTable.GetValue(ValueLootTable.GetMaxCost()));

    }

    public void NewValue(GameValue value)
    {
        Transform lastChild = transform.GetChild(transform.childCount - 1);
        lastChild.gameObject.GetComponent<ValueDragAndDrop>().SetValue(value);
    }
}
