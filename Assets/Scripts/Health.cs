using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] Rigidbody mainRb;
    [SerializeField] float health;
    public void DecrementHealth(float damage){
        health -= damage;
        if(health <= 0){
            health = 0;
            Die();
        }
    }
    public void Knock(Vector3 force){
        mainRb.AddForce(force);
    }
    public event Action onDie;
    void Die(){
        if(onDie != null)
        onDie();
    }
}
