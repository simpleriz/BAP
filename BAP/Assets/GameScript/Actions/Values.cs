using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameValue
{
    public float value;

    public ValueType type = ValueType.NoneType;
    public abstract float GetValue(GameScript script);
    public abstract string GetContent();
    public abstract GameValue Copy();
    public virtual bool CompareType(ValueType valueType){
        return valueType == type;
    }
}

public class StaticValue : GameValue
{
    public StaticValue(float val){
        value = val;
    }
    public StaticValue(float val, ValueType type){
        value = val;
        base.type = type;
    }
    public StaticValue(){}
    public override float GetValue(GameScript script){
        return value;
    }

    public override string GetContent(){
        switch(base.type)
        {
            case ValueType.NoneType:
                return "<b><color=white>["+value.ToString()+"]</b></color>";
            case ValueType.Red:
                return "<b><color=red>["+value.ToString()+"]</b></color>";
            case ValueType.Blue:
                return "<b><color=blue>["+value.ToString()+"]</b></color>";
            case ValueType.Green:
                return "<b><color=green>[" + value.ToString() + "]</b></color>";
            case ValueType.Smal:
                return "<b><color=#00FFFF>[" + value.ToString() + "]</b></color>";
            default:
                return "<b><color=black>["+value.ToString()+"]</b></color>";
        }
    }

     public override GameValue Copy(){
        GameValue gameValue = new StaticValue(value,base.type);
        return gameValue;
    }
}

public class PercentValue : GameValue
{
    public PercentValue(float val){
        value = val;
    }
    public PercentValue(float val, ValueType type){
        value = val;
        base.type = type;
    }
    public PercentValue(){}

    public override float GetValue(GameScript script){
        return 0;
    }
    public virtual float GetValue(GameScript script, float divisible){
        return Mathf.Ceil(divisible/100 * value);
    }

    public override string GetContent(){
        switch(base.type)
        {
            case ValueType.NoneType:
                return "<b><color=#6900C6>[" + value.ToString()+"%]</b></color>";
            default:
                return "<b><color=black>["+value.ToString()+"%]</b></color>";
        }
    }
    public override GameValue Copy(){
        GameValue gameValue = new PercentValue(value,base.type);
        return gameValue;
    }

}

public enum ValueType
{   
    NoneType,
    Red,
    Green,
    Blue,
    Smal,
}