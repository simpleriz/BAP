using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuesCB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        var values = ValueLootTable.GetValueSequence(5);
        foreach (var value in values) {
            NewValue(value);
        }

    }

    public void NewValue(GameValue value)
    {
        Transform lastChild = transform.GetChild(transform.childCount - 1);
        lastChild.gameObject.GetComponent<ValueDragAndDrop>().SetValue(value);
    }
}
