using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave", order = 0)]
public class Wave : ScriptableObject
{
    public Enemy[] enemies;
    public int count;  //how many enemys will spawn in this wave
    public float timeBetweenSpawns;
}

