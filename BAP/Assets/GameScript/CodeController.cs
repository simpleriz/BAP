using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.EventSystems;

public class CodeController : MonoBehaviour
{   
    //main code
    [SerializeField] TextMeshProUGUI codeText;
    [SerializeField] RectTransform codeZone;
    [SerializeField] CodeAnimator codeAnimator;

    [SerializeField] FightPlace fightPlace;
    public GameScript script;
    
    public ActivitiesController activitiesController;
    
    public void Start()
    {
        script = new GameScript();

        script.place = fightPlace;
        script.codeAnimator = codeAnimator;
        script.activitiesController = activitiesController;
        script.codeController = this;

        script.lines.Add(new GameLine());
        script.lines.Last().actions.Add(new EmptyAction());

        script.lines.Add(new GameConditionalLine(new TrueCondition()));
        script.lines.Last().actions.Add(new EmptyAction());

        //StartCoroutine(script.RoundCoroutine());
        UpdateContentView();

        Action.gameScript = script;
    }

    public void Act(){
        StartCoroutine(script.RoundCoroutine());
    }

    public bool PasteLine(GameLine clipboard)
    {
        int clickLine = TMP_TextUtilities.FindIntersectingLine(codeText, Input.mousePosition, null);
        int clickCodeLine = script.FindGameLineByContentLine(clickLine);
        if(clickCodeLine != -1 & IsMouseOverText()){
            script.lines[clickCodeLine].actions = clipboard.CopyActions();
            UpdateContentView();
            return true;
        }
        return false;
    }

    public bool PasteValue(GameValue clipboard)
    {
        int clickLine = TMP_TextUtilities.FindIntersectingLine(codeText, Input.mousePosition, null);
        int clickCodeLine = script.FindGameLineByContentLine(clickLine);
        if(clickCodeLine != -1){
            int clickCodeAction = script.lines[clickCodeLine].FindGameActionByContentLine(clickLine);
            if(script.lines[clickCodeLine].actions[clickCodeAction].SetValueByContentChar(TMP_TextUtilities.FindNearestCharacterOnLine(codeText, Input.mousePosition, clickLine , null, true),clipboard))
            {
                UpdateContentView();
                return true;
            }
            else{
                return false;
            }
        }
        return false;
    }

    public void UpdateContentView()
    {
        codeText.text = script.GetContent();
    }

    private bool IsMouseOverText()
    {
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            codeZone, 
            Input.mousePosition, 
            null, 
            out localMousePosition
        );
        return codeZone.rect.Contains(localMousePosition);
    }
}










