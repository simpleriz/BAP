using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public static class LootController
{
    public static List<List<GameValue>> valuesBoxes;

    static LootController()
    {
        valuesBoxes = new List<List<GameValue>>();
    }
    static public void newValueBox(int weight)
    {
        valuesBoxes.Add(new List<GameValue>());
        for (int i = 0; i < weight; i++)
        {
            valuesBoxes.Last().Add(ValueLootTable.GetValue());
        }
    }
}


public static class ValueLootTable
{
    static List<ValueLoot> lootTable;
    static List<ValueLoot> lowLootTable;

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

        lowLootTable = new List<ValueLoot>();


        AddLowUnit(9, new StaticValue(10, ValueType.Red));
        AddLowUnit(9, new StaticValue(10, ValueType.Green));
        AddLowUnit(9, new StaticValue(10, ValueType.Blue));

        AddLowUnit(9, new PercentValue(10, ValueType.Red));
        AddLowUnit(9, new PercentValue(10, ValueType.Green));
        AddLowUnit(9, new PercentValue(10, ValueType.Blue));

        AddLowUnit(9, new StaticValue(1, ValueType.Smal));

    }

    public static void AddUnit(int weight, GameValue content)
    {
        lootTable.Add(new ValueLoot(weight, content));
    }

    public static void AddLowUnit(int weight, GameValue content)
    {
        lowLootTable.Add(new ValueLoot(weight, content));
    }

    public static GameValue GetValue(int cost)
    {
        int i = 0;
        int costCounter = lootTable[0].weight;
        while (true)
        {
            if (cost <= costCounter)
            {
                return lootTable[i].content.Copy();
            }
            else
            {
                i++;
                costCounter += lootTable[i].weight;
            }
        }
    }

    public static GameValue GetValue()
    {
        return GetValue(Random.Range(0,GetMaxCost()+1));
    }

    public static GameValue GetLowValue(int cost)
    {
        int i = 0;
        int costCounter = lowLootTable[0].weight;
        while (true)
        {
            if (cost <= costCounter)
            {
                return lowLootTable[i].content.Copy();
            }
            else
            {
                i++;
                costCounter += lowLootTable[i].weight;
            }
        }
    }

    public static int GetMaxCost()
    {
        return lootTable.Sum(a => a.weight);
    }

    public static int GetLowMaxCost()
    {
        return lowLootTable.Sum(a => a.weight);
    }
}

public static class LineLootTable
{
    static List<LineLoot> lootTable;

    private class LineLoot
    {
        public int weight;
        public GameAction content;
        public int valuesCount;
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
        for(int ii = 0;ii < lineLoot.valuesCount; ii++)
        {
            gameAction.values.Add(ValueLootTable.GetLowValue(ValueLootTable.GetLowMaxCost()));
        }
        return gameAction;
    }

    public static int GetMaxCost(){
        return lootTable.Sum(a => a.weight);
    }
}

