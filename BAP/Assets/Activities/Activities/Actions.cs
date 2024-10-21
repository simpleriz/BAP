using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Action
{   
    
    public static GameScript gameScript;
    public Activity activity;
    public bool isAimless;
    public virtual string GetLabel(){
        return "";
    }
    public static void Round()
    {
        AttackAction.damage = 0;
        HealAction.restoration = 0;
    }
    public virtual Color GetColor(){
        return new Color(1,1,1,1);
    }

    public virtual void ActionOnVirus(FightVirus virus){

    }

    public virtual void ActionOnSystem(FightSystem system){

    }

    public virtual void ActionWithoutTarget(){
        
    }

    public virtual bool IsVisible(){
        return true;
    }
}

public class AttackAction : Action
{
    public static float damage;

    public AttackAction(){
        isAimless = false;
    }

    public override string GetLabel(){
        return $"Нанести {damage}\n урона";
    }

    public override Color GetColor()
    {
        return new Color(1,0,0,1);
    }

    public override bool IsVisible(){
        if(damage == 0)
        {
            return false;
        }
        return true;
    }
    
    public override void ActionOnVirus(FightVirus virus)
    {
        virus.Damage(damage);
        activity.Delete();
    }
}

public class HealAction : Action
{
    public static float restoration;

    public HealAction(){
        isAimless = false;
    }

    public override string GetLabel(){
        return $"Восстановить {restoration}\n здоровья системе";
    }

    public override Color GetColor()
    {
        return new Color(0,1,0,1);
    }

    public override bool IsVisible(){
        return !(restoration == 0);
    }
    
    public override void ActionOnSystem(FightSystem system)
    {
        system.Damage(restoration*-1);
        activity.Delete();
    }
}


