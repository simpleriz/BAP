using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ValueLootBoxCell : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject frame;
    [SerializeField] TextMeshProUGUI textMeshPro;
    public ValueLootBox lootBox;
    public GameValue value;
    bool isSelect;

    void Start()
    {
        textMeshPro.text = value.GetContent();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isSelect)
        {
            SetSelect(true);
            lootBox.SetSelected(this);
        }   
    }

    public void SetSelect(bool select)
    {
        frame.SetActive(select);
        isSelect = select;
    }


}
