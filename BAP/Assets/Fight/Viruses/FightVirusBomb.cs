using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightVirusBomb : FightVirusSkill
{   
    [SerializeField] int bombDamage = 150; 
    protected override void Skill(GameScript script, FightPlace place, FightSystem system){
        system.Damage(bombDamage);
        Kill();
    }
}
