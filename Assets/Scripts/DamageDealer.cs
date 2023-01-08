using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float knockForce;
    [SerializeField] Rigidbody rb;
    [SerializeField] float height;
    [SerializeField] float damage = 1.5f;
    [SerializeField] string enemyTag = "Player";
    [SerializeField] Movement movement;
    [SerializeField] Animator anim;
    GameObject muscleFocus;
    bool canAttack;
    private void Start()
    {
        canAttack = true;
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag(enemyTag) && canAttack){
            Health otherHealth = other.collider.GetComponent<Muscle>().health;

            StartCoroutine(movement.Boost());
            StartCoroutine(CoolDown());
            otherHealth.Knock(FindAttackForce(Mathf.Max(12, other.relativeVelocity.magnitude)));
            print((other.relativeVelocity * knockForce).magnitude);
            otherHealth.DecrementHealth(damage);
        }
    }
    #region activate ragdoll
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attackable") && muscleFocus == null && canAttack)
        {
            print("do it");
            muscleFocus = other.gameObject;
            other.GetComponent<Muscle>().health.ActivateRagdoll(true);
            anim.SetTrigger("attack");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Attackable") && other.gameObject == muscleFocus)
        {
            muscleFocus = null;
            other.GetComponent<Muscle>().health.ActivateRagdoll(false);
        }
    }
    #endregion
    IEnumerator CoolDown(){
        canAttack = false;
        yield return new WaitForSeconds(.35f);
        canAttack = true;
    }
    Vector3 FindAttackForce(float collisionMag){
        Vector3 forceDir = rb.velocity.normalized;
        forceDir.y += height;
        return forceDir * knockForce * collisionMag;
    }
}
