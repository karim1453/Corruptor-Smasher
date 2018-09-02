﻿using UnityEngine;
using System.Collections;
using Assets.Assets;
using Assets.Scripts.Collectible_Item;
using System;

public class TowerHealth : MonoBehaviour {

    public delegate void ReportHp();
    public event ReportHp OnZeroHp;

    private float intialHp;
    private float currentHp;

    private Tower tower;
    public float CurrentHp { get { return currentHp; } }

    void Start()
    {
        tower = GetComponent<Tower>();
        intialHp = tower.Hp;
        currentHp = intialHp;
        tower.onCollisionEnemy += OnTrigger;
    }
    void OnDisable()
    {
        tower.onCollisionEnemy -= OnTrigger;
    }
    public void BonusHealth(float health)
    {
        if (currentHp >= intialHp) return;
        currentHp += health;
    }
    private void OnTrigger(Collider2D other)
    {
        if (other.tag == Tag.Enemy)
        {
            float attackPower = other.GetComponent<Enemy>().AttackPower;
            AttackHp(attackPower);
        }
    }
    private void AttackHp(float damage)
    {
        currentHp -= damage;
        CheckHp();
    }
    private void CheckHp()
    {
        if (currentHp<=0)
        {
            if (OnZeroHp != null)
                OnZeroHp();
        }
    }
}
