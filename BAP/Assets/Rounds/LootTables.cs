using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
//using System;


public static class LootController
{
    public static List<List<GameValue>> valuesBoxes;

    static LootController()
    {
        valuesBoxes = new List<List<GameValue>>();
    }
    static public void NewValueBox(int weight)
    {
        valuesBoxes.Add(ValueLootTable.GetValueSequence(weight));
    }
}


public static class ValueLootTable
{
    static List<ValueLoot> lootTable;
    static List<int> valueSequence;
    static List<int> valueBuffer;
    const int valueBufferSize = 15;

    private class ValueLoot
    {
        public int weight;
        public GameValue content;
        public ValueLoot(int w,GameValue gameValue)
        {
            weight = w;
            content = gameValue;
        }
    }
    static ValueLootTable()
    {
        lootTable = new List<ValueLoot>();

        //percent
        AddUnit(9, new PercentValue(20, ValueType.NoneType));
        AddUnit(3, new PercentValue(30, ValueType.NoneType));
        AddUnit(1, new PercentValue(40, ValueType.NoneType));

        //small value
        AddUnit(9, new StaticValue(2, ValueType.Smal));
        AddUnit(3, new StaticValue(3, ValueType.Smal));
        AddUnit(1, new StaticValue(4, ValueType.Smal));

        //every color value
        AddUnit(9, new StaticValue(20, ValueType.Red));
        AddUnit(9, new StaticValue(20, ValueType.Green));
        AddUnit(9, new StaticValue(20, ValueType.Blue));

        AddUnit(3, new StaticValue(30, ValueType.Red));
        AddUnit(3, new StaticValue(30, ValueType.Green));
        AddUnit(3, new StaticValue(30, ValueType.Blue));

        AddUnit(1, new StaticValue(40, ValueType.Red));
        AddUnit(1, new StaticValue(40, ValueType.Green));
        AddUnit(1, new StaticValue(40, ValueType.Blue));


        GenerateValueSequence();
    }

    public static void AddUnit(int weight, GameValue content)
    {
        lootTable.Add(new ValueLoot(weight, content));
    }

    static void GenerateValueSequence()
    {
        valueSequence = new List<int>();
        valueBuffer = new List<int>();
        int i = 0;
        foreach(var value in lootTable)
        {
            valueSequence.AddRange(Enumerable.Repeat(i, value.weight));
            i++;
        }
        valueSequence = valueSequence.OrderBy(x => Random.Range(0, int.MaxValue)).ToList();
        Debug.Log(string.Join(", ", valueSequence));
    }

    static void AddToBuffer(int x)
    {
        valueBuffer.Add(x);
        if(valueBuffer.Count >= valueBufferSize)
        {
            valueSequence.AddRange(valueBuffer.OrderBy(x => Random.Range(0, int.MaxValue)));
            valueBuffer = new List<int>();
        } 
    }

    public static GameValue GetValue()
    {
        var value = lootTable[valueSequence[0]].content;
        AddToBuffer(valueSequence[0]);
        valueSequence.RemoveAt(0);
        return value.Copy();
    }

    public static List<GameValue> GetValueSequence(int lenght)
    {
        List<GameValue> gameValues = new List<GameValue>();
        var values = valueSequence.Distinct().Take(lenght).ToList();
        foreach (var value in values)
        {
            AddToBuffer(value);
            valueSequence.Remove(value);
            gameValues.Add(lootTable[value].content.Copy());
        }
        return gameValues;
    }
}

public static class LineLootTable
{
    static List<LineLoot> lootTable;

    private class LineLoot
    {
        public int weight;
        public GameAction content;
        public LineLoot(int w,GameAction gameValue)
        {
            weight = w;
            content = gameValue;
        }
    }
    static LineLootTable()
    {
        lootTable = new List<LineLoot>();

        AddUnit(9, new JustHealAction(), new List<GameValue> { new StaticValue(10, ValueType.NoneType) });
        AddUnit(9, new JustDamageAction(), new List<GameValue> { new StaticValue(10, ValueType.NoneType) });
    }

    public static void AddUnit(int weight, GameAction content, List<GameValue> values)
    {
        lootTable.Add(new LineLoot(weight,content));
        lootTable.Last().content.values = values;
    }

    public static GameAction GetLine(int cost)
    {
        int i = 0;
        int costCounter = lootTable[0].weight;
        LineLoot lineLoot;
        while(true)
        {   
            if(cost<=costCounter)
            {
                lineLoot = lootTable[i];
                break;
            }
            else
            {
                i++;
                costCounter += lootTable[i].weight;
            }
        }
        GameAction gameAction = lineLoot.content.Copy();
        return gameAction;
    }

    public static int GetMaxCost(){
        return lootTable.Sum(a => a.weight);
    }
}

