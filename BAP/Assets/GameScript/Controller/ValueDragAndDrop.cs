using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class ValueDragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{   
    [SerializeField] CodeController codeController;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    GameValue gameValue;
    private RectTransform rectTransform;
    Vector3 startPosition;
    bool isMouseDown = false;
    bool lineGenKey;
    // Start is called before the first frame update
    void Start()
    {
        GenerateNewLine();
        rectTransform = textMeshProUGUI.GetComponent<RectTransform>();
        startPosition = rectTransform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, null, out localPoint);
        if (rectTransform.rect.Contains(localPoint))
        {
            isMouseDown = true;
        }   
    }

    // Метод для обработки отпускания кнопки мыши
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isMouseDown)
        {
            
            isMouseDown = false;
            if(codeController.PasteValue(gameValue))
            {
                GenerateNewLine();
            }
            rectTransform.position = startPosition;
            
        }
    }

    void Update(){
        if(isMouseDown){
            rectTransform.position = Input.mousePosition;
        }
    }

    void GenerateNewLine(){
        
        gameValue = ValueLootTable.GetValue(ValueLootTable.GetMaxCost());
        textMeshProUGUI.text = gameValue.GetContent();
    }
}
