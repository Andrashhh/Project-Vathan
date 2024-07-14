using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType {
    Player,
    Boss,
    Fodder,
}
[CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
public class Entity : ScriptableObject
{
    [Header("Who")]
    public string Name;
    public string Description;
    public EntityType EntityType;
    [Header("Stats")]
    public float MaxHealth;
    public float MaxStamina;
    public float Armor;

}
