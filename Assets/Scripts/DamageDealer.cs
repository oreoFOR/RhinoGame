using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float knockForce;
    [SerializeField] Rigidbody rb;
    [SerializeField] float height;
    [SerializeField] float damage = 1.5f;
    bool canAttack;

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Attackable") && canAttack){
            Health otherHealth = other.collider.GetComponent<Health>();
            otherHealth.Knock(FindAttackForce(other.relativeVelocity.magnitude));
            print((other.relativeVelocity * knockForce).magnitude);
            otherHealth.DecrementHealth(other.relativeVelocity.magnitude * damage);
            StartCoroutine(CoolDown());
        }
    }
    IEnumerator CoolDown(){
        canAttack = false;
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
    }
    Vector3 FindAttackForce(float collisionMag){
        Vector3 forceDir = rb.velocity.normalized;
        forceDir.y += height;
        return forceDir * knockForce * collisionMag;
    }
}
