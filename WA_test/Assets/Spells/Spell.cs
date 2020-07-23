﻿using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "ScriptableObjects/Custom/Spell", order = 1)]
public class Spell : ScriptableObject
{
    //--- Public Variables ---//
    [Header("HUD Information")]
    public string m_name;
    public Sprite m_icon;

    [Header("In-Game Renderables")]
    public GameObject m_fxPrefab;

    [Header("Characteristics")]
    public float m_damage;
    public SpellIndicatorType m_indicatorType;
}
