using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BabyRhino : MonoBehaviour
{
    [SerializeField] List<Transform> attackers = new List<Transform>();
    [SerializeField] Transform player;
    Movement movement;
    float yMovement;
    float xMovement;
    private void Start()
    {
        movement = GetComponent<Movement>();
        GameEvents.instance.onRemoveAnimal += CheckForEnemyDestroyed;
    }
    void CheckForEnemyDestroyed(Transform enemy)
    {
        print("chekcing");

        if (attackers.Contains(enemy))
        {
            print("found and removed");
            attackers.Remove(enemy);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")&&!other.GetComponent<Health>().isDead)
        {
            attackers.Add(other.transform);
            print("attackers found");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            attackers.Remove(other.transform);
        }
    }
    private void Update()
    {
        movement.SetInput(yMovement, xMovement);
        if(attackers.Count > 0)
        {
            Vector3 dir = Vector3.zero;
            for (int i = 0; i < attackers.Count; i++)
            {
                dir += (transform.position - attackers[i].position).normalized;
            }
            dir /= attackers.Count;
            CalcMovement(dir, 1f);
        }
        else if((transform.position - player.position).magnitude>7)
        {
            CalcMovement(player.position - transform.position, 1f);
        }
        else
        {
            yMovement = 0;
            xMovement = 0;
        }
    }
    void CalcMovement(Vector3 dir, float _forwardSpeed)
    {
        yMovement = _forwardSpeed;
        Vector3 dirToPlayer = dir;
        Vector3 lookDir = transform.forward;
        lookDir.y = 0;
        dirToPlayer.y = 0;
        float angle = AngleDir(lookDir, dirToPlayer, Vector3.up);
        if (angle < 180)
        {
            xMovement = angle * .02f;
        }
    }
    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        float angle = Vector3.Angle(fwd, targetDir);
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);
        if (dir > 0f)
        {
            return angle;
        }
        else if (dir < 0f)
        {
            return -angle;
        }
        else
        {
            return 0f;
        }
    }
}
