using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FightVirus : MonoBehaviour
{
    public float maxHealth = 100;
    public float health = 100;
    [SerializeField] RectTransform HealthBar; 
    public float damage;
    public List<int> valueLoot;

    public virtual void UntilAct(GameScript script, FightPlace place, FightSystem system)
    {

    }
    public virtual void Act(GameScript script, FightPlace place, FightSystem system)
    {
        system.Damage(damage);
    }
    public virtual void Kill(){
        foreach (var item in valueLoot)
        {
            LootController.NewValueBox(item);
        }
        Destroy(gameObject);
    }
    public virtual void Damage(float value){
        health -= value;
        HealthBar.localScale = new Vector3(health/maxHealth,1,1);
        if(health <= 0)
            Kill();
    }
}

