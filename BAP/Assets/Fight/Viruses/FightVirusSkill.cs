using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FightVirusSkill : FightVirus
{
    [SerializeField] protected int maxCooldown = 5;
    [SerializeField] protected int cooldown;
    
    public override void Act(GameScript script, FightPlace place, FightSystem system){
        if(cooldown == 0){
            cooldown = maxCooldown;
            Skill(script, place, system);
        }
        else{
            cooldown -= 1;
            base.Act(script,place,system);
        }
    }
    protected abstract void Skill(GameScript script, FightPlace place, FightSystem system);
}
