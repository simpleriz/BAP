using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class GameLine
{
    public List<GameAction> actions;
    protected int startLine;
    protected int endLine;
    public GameLine(){
        actions = new List<GameAction>();
    }
    public virtual IEnumerator RoundCoroutine(GameScript script){
        foreach(GameAction action in actions){
            yield return action.RoundCoroutine(script);
            script.codeAnimator.nextStep();
        }
        yield return new WaitForSeconds(0.4f * script.corutineSpeed);
        script.codeAnimator.nextStep();
    }
    public virtual string GetContent(string content){

        startLine = GetActualLinesCount(content);
        foreach(GameAction action in actions){
            content += " ";
            content = action.GetContent(content);
        }
        endLine = GetActualLinesCount(content);
        content += "\n";
        return content;
    }
    public bool CheckLine(int line){
        return (startLine-1 < line && endLine > line);
    }

    public int FindGameActionByContentLine(int contentLine){
        return contentLine - startLine;
    }

    public List<GameAction> CopyActions(){
        List<GameAction> newActions = new List<GameAction>();
        foreach(GameAction action in actions){
            newActions.Add(action.Copy());
        }
        return newActions;
    }

    public virtual GameLine Copy(){
        GameLine gameLine = new GameLine();
        foreach(GameAction action in actions){
            gameLine.actions.Add(action.Copy());
        }
        return gameLine;
    }

    protected int GetActualStringLength(string input)
    {
        // Remove HTML tags
        string withoutTags = Regex.Replace(input, "<.*?>", string.Empty);

        // Replace newline characters with a single character
        string withoutNewlines = withoutTags.Replace("\n", " ");

        int spriteCount = Regex.Matches(input, "<sprite=.*?>").Count;

        // Return the actual length of the string, including the sprite count
        return withoutNewlines.Length + spriteCount;
    }

    protected int GetActualLinesCount(string content)
    {
        return Regex.Matches(content, @"\n").Count;
    }
}

public class GameConditionalLine: GameLine
{
    public GameCondition condition;

    public GameConditionalLine(GameCondition con){
        condition = con;
    }
    public override IEnumerator RoundCoroutine(GameScript script){
        if(condition.Check(script))
        {
            
            yield return new WaitForSeconds(0.4f * script.corutineSpeed);
            script.codeAnimator.SetBackgroundComplete();
            script.codeAnimator.SetContent("TRUE");
            yield return new WaitForSeconds(0.6f * script.corutineSpeed);
            script.codeAnimator.nextStep();
            yield return base.RoundCoroutine(script);
        }
        else
        {
            yield return new WaitForSeconds(0.4f * script.corutineSpeed);
            script.codeAnimator.SetBackgroundUncomplete();
            yield return new WaitForSeconds(0.6f * script.corutineSpeed);
            //if condition is false code...
        }
    }

    public override string GetContent(string content){
        content += "<color=yellow> Если ";
        content += condition.GetContent();
        content += " то:</color>\n";
        startLine = GetActualLinesCount(content);
        foreach(GameAction action in actions){
            content += "   ";
            content = action.GetContent(content);
        }
        endLine = GetActualLinesCount(content);
        content += "\n";
        return content;
    }
}

public abstract class GameCondition
{
    public abstract bool Check(GameScript script);
    public abstract string GetContent();
}

public class TrueCondition: GameCondition
{
    public override bool Check(GameScript script){
        return true;
    }
    public override string GetContent(){
        return "x = x";
    }
}
