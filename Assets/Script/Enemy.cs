using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Animator animator;
    [SerializeField] float attackSpeed = 1.0f;
    [SerializeField] float attackDelay = 0.3f;
    [SerializeField] float attackDistance = 5f;
    [SerializeField] int attackDamage = 1;
    [SerializeField] ParticleSystem hitEffect;
    public int currentHP;
    public int maxHP;

    void Start(){
        animator = GetComponent<Animator>();
    }
}
