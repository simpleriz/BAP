using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueLootBox : MonoBehaviour
{
    [SerializeField] GameObject selectiveValuePrefab;
    [SerializeField] Transform valuesGrid;
    [SerializeField] ValuesCB clipBoard;
    [SerializeField] GameObject boxWindow;
    ValueLootBoxCell selectedCell;

    public void SetSelected(ValueLootBoxCell newSelected)
    {
        if (selectedCell != null)
        {
            selectedCell.SetSelect(false);
        }
        selectedCell = newSelected;
    }

    void nextStep()
    {
        foreach (Transform child in valuesGrid)
        {
            Destroy(child.gameObject);
        }
        if (LootController.valuesBoxes.Count > 0)
        {
            boxWindow.SetActive(true);
            foreach (GameValue gameValue in LootController.valuesBoxes[0]) 
            {
                AddValue(gameValue);
            }
            LootController.valuesBoxes.RemoveAt(0);
        }
        else
        {
            boxWindow.SetActive(false);
        }
    }

    void OnEnable()
    {
        if (boxWindow.activeSelf == false)
        {
            nextStep();
        }
    }

    void AddValue(GameValue value)
    {
        ValueLootBoxCell inst = Instantiate(selectiveValuePrefab,valuesGrid).GetComponent<ValueLootBoxCell>();
        inst.value = value;
        inst.lootBox = this;
    }

    public void OnSkip()
    {
        nextStep();
    }

    public void OnAccept()
    {
        if(selectedCell != null)
        {
            clipBoard.NewValue(selectedCell.value);
            nextStep();
        }
    }
}
