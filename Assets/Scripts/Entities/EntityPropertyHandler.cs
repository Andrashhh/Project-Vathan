using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPropertyHandler : MonoBehaviour
{
    [SerializeField] private Entity m_Entity;

    public string Name { get; private set; }
    public string Description { get; private set; }
    public EntityType EntityType { get; private set; }

    public float MaxHealth { get; private set; }
    public float MaxStamina { get; private set; }
    public float Armor { get; private set; }

    void Awake() {

        Name = m_Entity.Name;
        Description = m_Entity.Description;
        EntityType = m_Entity.EntityType;

        MaxHealth = m_Entity.MaxHealth;
        MaxStamina = m_Entity.MaxStamina;
        Armor = m_Entity.Armor;
    }
}
