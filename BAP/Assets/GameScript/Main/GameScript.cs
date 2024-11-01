using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class GameScript
{
    public List<GameLine> lines;
    public FightPlace place;
    public CodeAnimator codeAnimator;
    public ActivitiesController activitiesController;
    public CodeController codeController;
    public float corutineSpeed = 1f;
    public bool isCorutineActive;
    public GameValue lastCompleteValue;
    public GameAction lastCompleteAction;
    public GameScript(){
        lines = new List<GameLine>();
    } 

    public IEnumerator RoundCoroutine(){
        isCorutineActive = true;
        place.Act(this);
        activitiesController.AddActivity(new AttackAction());
        activitiesController.AddActivity(new HealAction());
        codeAnimator.Restart();
        foreach(GameLine line in lines){
            yield return line.RoundCoroutine(this);
        }
        codeAnimator.Hide();
        isCorutineActive=false;
    }

    public string GetContent(){
        string content = "<color=blue>import</color> <color=grey>base.libraries</color>\n   <sprite=1> - это смайлик?\n\n";

        foreach(GameLine line in lines){
            content = line.GetContent(content);
        }
        //Debug.Log(Regex.Matches(content, @"\n").Count);
        return content;
    }
    public int FindGameLineByContentLine(int contentLine){
        int i = 0;
        foreach(GameLine line in lines){
            if (line.CheckLine(contentLine)){
                return i;
            }
            i++;
        }
        return -1;
    }

    int GetActualStringLength(string input)
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
