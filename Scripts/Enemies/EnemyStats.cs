using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float Speed;
    public float AttackSpeed;
    public float Damage;
    public float RangedDamage;
    public float Health;

    private void Awake()
    {
        //initalizes stats accounting for difficulty setting
        OnDifficultyChange();
    }

    //called when difficulty is changed on all existing enemies
    public void OnDifficultyChange()
    {
       float diffiucultyModifier = PlayerPrefs.GetFloat("DiffucltyMod", 1.0f);
        Debug.Log(diffiucultyModifier);
        Speed = Speed * diffiucultyModifier;
        AttackSpeed = AttackSpeed * diffiucultyModifier;
        Damage = Damage * diffiucultyModifier;
        RangedDamage = RangedDamage * diffiucultyModifier;
        Health = Health * diffiucultyModifier;


        
    }

}
