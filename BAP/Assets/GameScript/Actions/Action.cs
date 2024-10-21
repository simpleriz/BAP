using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public abstract class GameAction
{   
    public List<GameValue> values;
    protected Vector2[] valuesContentChars;     
    public GameAction(){
        values = new List<GameValue>();
    }
    public abstract IEnumerator RoundCoroutine(GameScript script);
    public abstract string GetContent(string content);
    public abstract GameAction Copy();
    public virtual bool SetValueByContentChar(int contentChar, GameValue gameValue){
        int i = 0;
        foreach(Vector2 valueContentChar in valuesContentChars){
            if(valueContentChar[0] < contentChar & valueContentChar[1] > contentChar){
                values[i] = gameValue;
                return true;
            }
            i++;
        }
        return false;
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
}

public class JustDamageAction : GameAction
{
    float ValueRound(GameScript script, out float cof)
    {
        float val;
        cof = 1;
        PercentValue percentValue = values[0] as PercentValue;
        if(percentValue == null)
        {
            val = values[0].GetValue(script);
        }
        else
        {
            val = percentValue.GetValue(script, AttackAction.damage);
        }
        if(values[0].CompareType(ValueType.Red)) cof += 2;
        return val*cof;
    }

    public override IEnumerator RoundCoroutine(GameScript script){
        yield return new WaitForSeconds(0.4f);

        float cof;
        float damageValue = ValueRound(script,out cof);

        if(cof == 1)
        {
            script.codeAnimator.SetBackgroundComplete();
            script.codeAnimator.SetContent(damageValue);
            yield return new WaitForSeconds(0.2f);
        }
        else
        {
            script.codeAnimator.SetBackgroundComplete();
            script.codeAnimator.SetContent((damageValue/cof).ToString() + " x " + cof.ToString());
            yield return new WaitForSeconds(0.2f);
        }
        

        for(float i = damageValue;i > 0; i--)
        {
            AttackAction.damage ++;
            script.codeAnimator.SetContent(i.ToString());
            yield return new WaitForFixedUpdate();
        }

    }
    public override string GetContent(string content){
        if(valuesContentChars == null || values.Count != valuesContentChars.Length){
            valuesContentChars = new Vector2[values.Count];
        }
        content += "<color=#3477FF>+";
        valuesContentChars[0][0] = GetActualStringLength(content);
        content += values[0].GetContent();
        valuesContentChars[0][1] = GetActualStringLength(content);
        content += "к урону</color>\n";
        return content;
    }
    public override GameAction Copy(){
        GameAction gameAction = new JustDamageAction();
        foreach(GameValue value in values){
            gameAction.values.Add(value.Copy());
        }
        return gameAction;
    }
}

public class JustHealAction : GameAction
{

    float ValueRound(GameScript script, out float cof)
    {
        float val;
        cof = 1;
        PercentValue percentValue = values[0] as PercentValue;
        if(percentValue == null)
        {
            val = values[0].GetValue(script);
        }
        else
        {
            val = percentValue.GetValue(script, HealAction.restoration);
        }
        if(values[0].CompareType(ValueType.Green)) cof += 1;
        return val*cof;
    }

    public override IEnumerator RoundCoroutine(GameScript script){
        yield return new WaitForSeconds(0.4f);

        float cof;
        float healValue = ValueRound(script,out cof);

        if(cof == 1)
        {
            script.codeAnimator.SetBackgroundComplete();
            script.codeAnimator.SetContent(healValue);
            yield return new WaitForSeconds(0.2f);
        }
        else
        {
            script.codeAnimator.SetBackgroundComplete();
            script.codeAnimator.SetContent((healValue/cof).ToString() + " x " + cof.ToString());
            yield return new WaitForSeconds(0.2f);
        }
        

        for(float i = healValue;i > 0; i--)
        {
            HealAction.restoration ++;
            script.codeAnimator.SetContent(i.ToString());
            yield return new WaitForFixedUpdate();
        }

    }
    public override string GetContent(string content){
        if(valuesContentChars == null || values.Count != valuesContentChars.Length){
            valuesContentChars = new Vector2[values.Count];
        }
        content += "<color=#3477FF>+";
        valuesContentChars[0][0] = GetActualStringLength(content);
        content += values[0].GetContent();
        valuesContentChars[0][1] = GetActualStringLength(content);
        content += "к восстановлению</color>\n";
        return content;
    }
    public override GameAction Copy(){
        GameAction gameAction = new JustHealAction();
        foreach(GameValue value in values){
            gameAction.values.Add(value.Copy());
        }
        return gameAction;
    }
}

public class EmptyAction : GameAction
{
    public override IEnumerator RoundCoroutine(GameScript script){
        yield return new WaitForSeconds(0.4f);
        script.codeAnimator.SetBackgroundUncomplete();
        script.codeAnimator.SetContent("EMPTY");
        yield return new WaitForSeconds(0.6f);
    }
    public override string GetContent(string content){
        content += "<color=#3477FF>";
        content += "ничего не делает...";
        content += "</color>\n";
        return content;
    }
    public override GameAction Copy(){
        return new EmptyAction();
    }
}
