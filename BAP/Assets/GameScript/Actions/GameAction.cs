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
    public abstract IEnumerator RoundCoroutine(GameScript script,GameLine line,int id);
    public abstract string GetContent(string content);
    public abstract GameAction Copy();
    public virtual bool SetValueByContentChar(int contentChar, GameValue gameValue){
        if(valuesContentChars == null){return false;}
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

    public override IEnumerator RoundCoroutine(GameScript script, GameLine line, int id){
        yield return new WaitForSeconds(0.4f*script.corutineSpeed);

        float cof;
        float damageValue = ValueRound(script,out cof);

        if(cof == 1)
        {
            script.codeAnimator.SetBackgroundComplete();
            script.codeAnimator.SetContent(damageValue);
            yield return new WaitForSeconds(0.2f * script.corutineSpeed);
        }
        else
        {
            script.codeAnimator.SetBackgroundComplete();
            script.codeAnimator.SetContent((damageValue/cof).ToString() + " x " + cof.ToString());
            yield return new WaitForSeconds(0.2f * script.corutineSpeed);
        }


        for (int i = Mathf.CeilToInt(damageValue); i > 0; i--)
        {
            if (!(script.corutineSpeed >= 1))
            {
                AttackAction.damage += i;
                break;
            }
            AttackAction.damage ++;
            script.codeAnimator.SetContent(i.ToString());
            if (script.corutineSpeed >= 1)
            {
                yield return new WaitForFixedUpdate();
            }
        }
        script.lastCompleteValue = values[0];
        script.lastCompleteAction = this;

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

    public override IEnumerator RoundCoroutine(GameScript script, GameLine line, int id){
        yield return new WaitForSeconds(0.4f * script.corutineSpeed);

        float cof;
        float healValue = ValueRound(script,out cof);

        if(cof == 1)
        {
            script.codeAnimator.SetBackgroundComplete();
            script.codeAnimator.SetContent(healValue);
            yield return new WaitForSeconds(0.2f * script.corutineSpeed);
        }
        else
        {
            script.codeAnimator.SetBackgroundComplete();
            script.codeAnimator.SetContent((healValue/cof).ToString() + " x " + cof.ToString());
            yield return new WaitForSeconds(0.2f * script.corutineSpeed);
        }
        

        for(int i = Mathf.CeilToInt(healValue);i > 0; i--)
        {
            if (!(script.corutineSpeed >= 1))
            {
                HealAction.restoration += i;
                break;
            }

            HealAction.restoration ++;
            script.codeAnimator.SetContent(i.ToString());

            if (script.corutineSpeed >= 1)
            {
                yield return new WaitForFixedUpdate();
            }

        }

        script.lastCompleteValue = values[0];
        script.lastCompleteAction = this;
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
public class JustLuckAction : GameAction
{

    float ValueRound(GameScript script, out float cof)
    {
        float val;
        cof = 1;
        PercentValue percentValue = values[0] as PercentValue;
        if (percentValue == null)
        {
            val = values[0].GetValue(script);
        }
        else
        {
            val = percentValue.GetValue(script, LuckAttribute.luck);
        }
        if (values[0].CompareType(ValueType.Blue)) cof += 1;
        return val * cof;
    }

    public override IEnumerator RoundCoroutine(GameScript script, GameLine line, int id)
    {
        yield return new WaitForSeconds(0.4f * script.corutineSpeed);

        float cof;
        float luckValue = ValueRound(script, out cof);

        if (cof == 1)
        {
            script.codeAnimator.SetBackgroundComplete();
            script.codeAnimator.SetContent(luckValue);
            yield return new WaitForSeconds(0.2f * script.corutineSpeed);
        }
        else
        {
            script.codeAnimator.SetBackgroundComplete();
            script.codeAnimator.SetContent((luckValue / cof).ToString() + " x " + cof.ToString());
            yield return new WaitForSeconds(0.2f * script.corutineSpeed);
        }


        for (int i = Mathf.CeilToInt(luckValue); i > 0; i--)
        {
            if (!(script.corutineSpeed >= 1))
            {
                LuckAttribute.luck += i;
                break;
            }

            LuckAttribute.luck++;
            script.codeAnimator.SetContent(i.ToString());

            if (script.corutineSpeed >= 1)
            {
                yield return new WaitForFixedUpdate();
            }

        }

        script.lastCompleteValue = values[0];
        script.lastCompleteAction = this;
    }
    public override string GetContent(string content)
    {
        if (valuesContentChars == null || values.Count != valuesContentChars.Length)
        {
            valuesContentChars = new Vector2[values.Count];
        }
        content += "<color=#3477FF>+";
        valuesContentChars[0][0] = GetActualStringLength(content);
        content += values[0].GetContent();
        valuesContentChars[0][1] = GetActualStringLength(content);
        content += "к удаче</color>\n";
        return content;
    }
    public override GameAction Copy()
    {
        GameAction gameAction = new JustLuckAction();
        foreach (GameValue value in values)
        {
            gameAction.values.Add(value.Copy());
        }
        return gameAction;
    }
}

public class UpdateVariableAction : GameAction
{

    float ValueRound(GameScript script)
    {
        float val = 0;
        if(script.lastCompleteValue == null)
        {
            return val;
        }
        PercentValue percentValue = values[0] as PercentValue;
        if (percentValue != null)
        {
            val = percentValue.GetValue(script, script.lastCompleteValue.value);
        }
        return val;
    }

    public override IEnumerator RoundCoroutine(GameScript script, GameLine line, int id)
    {
        yield return new WaitForSeconds(0.4f * script.corutineSpeed);

        if (script.lastCompleteValue != null)
        {
            float updateValue = ValueRound(script);


            script.codeAnimator.SetBackgroundComplete();
            script.codeAnimator.SetContent(updateValue.ToString());
            yield return new WaitForSeconds(0.2f * script.corutineSpeed);



            for (int i = Mathf.CeilToInt(updateValue); i > 0; i--)
            {
                if (!(script.corutineSpeed >= 1))
                {
                    script.lastCompleteValue.value += i;
                    break;
                }
                script.lastCompleteValue.value++;
                script.codeAnimator.SetContent(i.ToString());

                if (script.corutineSpeed >= 1)
                {
                    script.codeController.UpdateContentView();
                    yield return new WaitForFixedUpdate();
                }


            }
            script.codeController.UpdateContentView();

            script.lastCompleteValue = values[0];
            script.lastCompleteAction = this;
        }
        else
        {
            script.codeAnimator.SetBackgroundUncomplete();
            script.codeAnimator.SetContent("ERROR");
            yield return new WaitForSeconds(0.2f * script.corutineSpeed);
        }

    }
    public override string GetContent(string content)
    {
        if (valuesContentChars == null || values.Count != valuesContentChars.Length)
        {
            valuesContentChars = new Vector2[values.Count];
        }
        content += "<color=#3477FF> Увеличивает переменную сверху на";
        valuesContentChars[0][0] = GetActualStringLength(content);
        content += values[0].GetContent();
        valuesContentChars[0][1] = GetActualStringLength(content);
        content += "</color>\n";
        return content;
    }

    public override bool SetValueByContentChar(int contentChar, GameValue gameValue)
    {
        if(gameValue as PercentValue == null)
        {
            return false;
        }
        else
        {
            return base.SetValueByContentChar(contentChar, gameValue);
        }
    }
    public override GameAction Copy()
    {
        GameAction gameAction = new UpdateVariableAction();
        foreach (GameValue value in values)
        {
            gameAction.values.Add(value.Copy());
        }
        return gameAction;
    }
}
public class AdditionAttackAction : GameAction
{
    public override IEnumerator RoundCoroutine(GameScript script, GameLine line, int id)
    {
        yield return new WaitForSeconds(0.4f * script.corutineSpeed);
        script.codeAnimator.SetBackgroundComplete();
        script.codeAnimator.SetContent("+1 target");
        script.activitiesController.AddActivity(new AttackAction());
        yield return new WaitForSeconds(0.2f * script.corutineSpeed);

        script.lastCompleteAction = this;
    }
    public override string GetContent(string content)
    {
        content += "<color=#00DBFF> Ещё одна цель атаки</color>\n";
        return content;
    }
    public override GameAction Copy()
    {
        GameAction gameAction = new AdditionAttackAction();
        return gameAction;
    }
}
public class LuckMultiply : GameAction
{

    float ValueRound(GameScript script)
    {
        float val = values[0].GetValue(script);
        val = (val - 1) * LuckAttribute.luck;
        return val;
    }

    public override IEnumerator RoundCoroutine(GameScript script, GameLine line, int id)
    {
        yield return new WaitForSeconds(0.4f * script.corutineSpeed);

        float cof;
        float luckValue = ValueRound(script);

        script.codeAnimator.SetBackgroundComplete();
        script.codeAnimator.SetContent(luckValue.ToString());
        yield return new WaitForSeconds(0.2f * script.corutineSpeed);


        for (int i = Mathf.CeilToInt(luckValue); i > 0; i--)
        {
            if (!(script.corutineSpeed >= 1))
            {
                LuckAttribute.luck += i;
                break;
            }
            LuckAttribute.luck += 1;
            script.codeAnimator.SetContent(i.ToString());

            if (script.corutineSpeed >= 1)
            {
                yield return new WaitForFixedUpdate();
            }

        }
        script.lastCompleteValue = values[0];
        script.lastCompleteAction = this;
    }

    public override bool SetValueByContentChar(int contentChar, GameValue gameValue)
    {
        if (gameValue.type == ValueType.Smal)
        {
            return base.SetValueByContentChar(contentChar, gameValue);
        }
        else
        {
            return false;
        }
        
    }
    public override string GetContent(string content)
    {
        if (valuesContentChars == null || values.Count != valuesContentChars.Length)
        {
            valuesContentChars = new Vector2[values.Count];
        }
        content += "<color=#3477FF> Увеличить удачу в ";
        valuesContentChars[0][0] = GetActualStringLength(content);
        content += values[0].GetContent();
        valuesContentChars[0][1] = GetActualStringLength(content);
        content += " раз</color>\n";
        return content;
    }
    public override GameAction Copy()
    {
        GameAction gameAction = new LuckMultiply();
        foreach (GameValue value in values)
        {
            gameAction.values.Add(value.Copy());
        }
        return gameAction;
    }
}
public class EmptyAction : GameAction
{
    public override IEnumerator RoundCoroutine(GameScript script, GameLine line, int id){
        yield return new WaitForSeconds(0.4f * script.corutineSpeed);
        script.codeAnimator.SetBackgroundUncomplete();
        script.codeAnimator.SetContent("EMPTY");
        yield return new WaitForSeconds(0.6f * script.corutineSpeed);
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
