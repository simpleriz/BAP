using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FightSystem : MonoBehaviour
{   
    public float maxHealth = 100;
    public float health = 100;
    [SerializeField] RectTransform HealthBar; 
    public List<FightVirus> viruses;
    [SerializeField] Transform virusesGrid;

    void Start(){
        HealthBar.localScale = new Vector3(health/maxHealth,1,1);
    }

    public void Act(GameScript script, FightPlace place){
        viruses.RemoveAll(item => item == null);
        foreach(FightVirus virus in viruses){
            virus.Act(script,place,this);
        }
    }
    public void Damage(float damage){
        health -= damage;
        HealthBar.localScale = new Vector3(health/maxHealth,1,1);
    }

    public void NewVirus(GameObject virusPrefab){
        GameObject inst = Instantiate(virusPrefab,virusesGrid);
        viruses.Add(inst.GetComponent<FightVirus>());
        viruses.Last().valueLoot = new List<int> { 3,5};
    }
}
