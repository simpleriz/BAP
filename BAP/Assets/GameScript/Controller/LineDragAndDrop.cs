using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class LineDragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{   
    [SerializeField] CodeController codeController;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    GameLine gameLine;
    private RectTransform rectTransform;
    Vector3 startPosition;
    bool isMouseDown = false;
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
            if(codeController.PasteLine(gameLine))
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
        gameLine = new GameLine();
        for(int i = 0;  i < 3; i++){
            switch(Random.Range(1,3))
            {
                case 1:
                    gameLine.actions.Add(new JustHealAction());
                    gameLine.actions.Last().values.Add(GenerateNewValue());
                    break;
                case 2:
                    gameLine.actions.Add(new JustDamageAction());
                    gameLine.actions.Last().values.Add(GenerateNewValue());
                    break;
            }
        }
        textMeshProUGUI.text = gameLine.GetContent("");
    }

    GameValue GenerateNewValue()
    {    
        GameValue gameValue = new StaticValue(Random.Range(0, 100),ValueType.Red);
        switch(Random.Range(1,3))
        {
            case 1:
                switch (Random.Range(1,4))
                {
                    case 1:
                        gameValue = new StaticValue(Random.Range(0, 100),ValueType.Red);
                        break;
                    case 2:
                        gameValue = new StaticValue(Random.Range(0, 100),ValueType.Blue);
                        break;
                    case 3:
                        gameValue = new StaticValue(Random.Range(0, 100),ValueType.Green);
                        break;
                }
                break;
            case 2:
                switch (Random.Range(1,4))
                {
                    case 1:
                        gameValue = new PercentValue(Random.Range(0, 100),ValueType.Red);
                        break;
                    case 2:
                        gameValue = new PercentValue(Random.Range(0, 100),ValueType.Blue);
                        break;
                    case 3:
                        gameValue = new PercentValue(Random.Range(0, 100),ValueType.Green);
                        break;
                }
                break;
        }
            
        return gameValue;
    }
}
