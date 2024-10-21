using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ValueLootTable
{
    static List<ValueLoot> lootTable;

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


        AddUnit(9,new StaticValue(20,ValueType.Red));
        AddUnit(9,new StaticValue(20,ValueType.Green));
        AddUnit(9,new StaticValue(20,ValueType.Blue));

        AddUnit(9,new PercentValue(20,ValueType.Red));
        AddUnit(9,new PercentValue(20,ValueType.Green));
        AddUnit(9,new PercentValue(20,ValueType.Blue));


        AddUnit(3,new StaticValue(30,ValueType.Red));
        AddUnit(3,new StaticValue(30,ValueType.Green));
        AddUnit(3,new StaticValue(30,ValueType.Blue));

        AddUnit(3,new PercentValue(30,ValueType.Red));
        AddUnit(3,new PercentValue(30,ValueType.Green));
        AddUnit(3,new PercentValue(30,ValueType.Blue));


        AddUnit(1,new StaticValue(40,ValueType.Red));
        AddUnit(1,new StaticValue(40,ValueType.Green));
        AddUnit(1,new StaticValue(40,ValueType.Blue));

        AddUnit(1,new PercentValue(40,ValueType.Red));
        AddUnit(1,new PercentValue(40,ValueType.Green));
        AddUnit(1,new PercentValue(40,ValueType.Blue));
    }

    public static void AddUnit(int weight, GameValue content)
    {
        lootTable.Add(new ValueLoot(weight,content));
    }

    public static GameValue GetValue(int cost)
    {
        int i = 0;
        int costCounter = lootTable[0].weight;
        while(true)
        {   
            Debug.Log($"{costCounter},{i},{cost},{cost<=costCounter}");
            if(cost<=costCounter)
            {
                return lootTable[i].content;
            }
            else
            {
                i++;
                costCounter += lootTable[i].weight;
            }
        }
    }

    public static int GetMaxCost(){
        return lootTable.Sum(a => a.weight);
    }
}

/*public static class LineLootTable
{
    static List<ValueLoot> lootTable;

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
    static ValueLootTable()
    {
        lootTable = new List<ValueLoot>();


        AddUnit(9,new StaticValue(20,ValueType.Red));
        AddUnit(9,new StaticValue(20,ValueType.Green));
        AddUnit(9,new StaticValue(20,ValueType.Blue));

        AddUnit(9,new PercentValue(20,ValueType.Red));
        AddUnit(9,new PercentValue(20,ValueType.Green));
        AddUnit(9,new PercentValue(20,ValueType.Blue));


        AddUnit(3,new StaticValue(30,ValueType.Red));
        AddUnit(3,new StaticValue(30,ValueType.Green));
        AddUnit(3,new StaticValue(30,ValueType.Blue));

        AddUnit(3,new PercentValue(30,ValueType.Red));
        AddUnit(3,new PercentValue(30,ValueType.Green));
        AddUnit(3,new PercentValue(30,ValueType.Blue));


        AddUnit(1,new StaticValue(40,ValueType.Red));
        AddUnit(1,new StaticValue(40,ValueType.Green));
        AddUnit(1,new StaticValue(40,ValueType.Blue));

        AddUnit(1,new PercentValue(40,ValueType.Red));
        AddUnit(1,new PercentValue(40,ValueType.Green));
        AddUnit(1,new PercentValue(40,ValueType.Blue));
    }

    public static void AddUnit(int weight, GameValue content)
    {
        lootTable.Add(new ValueLoot(weight,content));
    }

    public static GameValue GetValue(int cost)
    {
        int i = 0;
        int costCounter = lootTable[0].weight;
        while(true)
        {   
            Debug.Log($"{costCounter},{i},{cost},{cost<=costCounter}");
            if(cost<=costCounter)
            {
                return lootTable[i].content;
            }
            else
            {
                i++;
                costCounter += lootTable[i].weight;
            }
        }
    }

    public static int GetMaxCost(){
        return lootTable.Sum(a => a.weight);
    }
}*/