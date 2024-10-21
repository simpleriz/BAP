using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlace : MonoBehaviour
{
    public FightSystem[] systems;
    public GameObject[] lowCostViruses;
    public GameObject[] highCostViruses;
    public void Start(){
    }
    public void Act(GameScript script){
        foreach(FightSystem system in systems){
            system.Act(script,this);
        }
    }
    public void GenerateNewVirusesByCost(int cost){
        while(cost > 0){
            if(Random.Range(1,3) == 1 || cost == 1){
                systems[Random.Range(0,systems.Length)].NewVirus(lowCostViruses[Random.Range(0,lowCostViruses.Length)]);
                cost -= 1;
            }
            else{
                systems[Random.Range(0,systems.Length)].NewVirus(highCostViruses[Random.Range(0,highCostViruses.Length)]);
                cost -= 2;
            }
        }
    }
}




