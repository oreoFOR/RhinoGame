using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RootMotion.Dynamics;

public class Health : MonoBehaviour
{
    [SerializeField] Rigidbody mainRb;
    [SerializeField] float health;
    [SerializeField] PuppetMaster pm;
    [SerializeField] bool baby;
    public bool isDead;
    public void DecrementHealth(float damage){
        health -= damage;
        if (baby)
        {
            GameEvents.instance.TakeDamage(health);
            StartCoroutine(GetComponent<Movement>().UncapVelocity());
        }
        if(health <= 0){
            health = 0;
            Die();
        }
    }
    public void Knock(Vector3 force){
        mainRb.AddForce(force);
        if (!baby)
        {
            Die();
        }
    }
    public event Action onDie;
    void Die()
    {
        isDead = true;
        if(onDie != null)
        {
            onDie();
        }
        pm.state = PuppetMaster.State.Dead;
        //GameEvents.instance.AnimalDie();
        //GameEvents.instance.RemoveAnimal(transform);
        if (!baby)
        {
            StartCoroutine(DestroyModel());
        }
    }
    public void ActivateRagdoll(bool value)
    {
        pm.mode = value ? PuppetMaster.Mode.Active : PuppetMaster.Mode.Kinematic;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pm.state = PuppetMaster.State.Dead;
        }
    }
    IEnumerator DestroyModel()
    {
        yield return new WaitForSeconds(3);
        //GameEvents.instance.RemoveAnimal(transform);
        Destroy(transform.parent.gameObject);
    }
}
