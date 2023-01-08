using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiRaceController : MonoBehaviour
{
    [SerializeField] List<Transform> checkpoints = new List<Transform>();
    int currentCheckpoint;
    [SerializeField]float changeDist = 5;
    [SerializeField] Movement movement;
    [SerializeField] float speed;

    float yMovement;
    float xMovement;
    private void Start()
    {
        movement = GetComponent<Movement>();
        RemoveCheckpoints();
    }
    void RemoveCheckpoints()
    {
        int checkpointsRemoved = 0;
        for (int i = 0; i < checkpoints.Count; i++)
        {
            if (i - checkpointsRemoved > 0)
            {
                if (checkpoints[i - checkpointsRemoved].position.z < transform.position.z)
                {
                    checkpointsRemoved++;
                    checkpoints.Remove(checkpoints[i - checkpointsRemoved]);
                }
            }
        }
    }
    private void Update()
    {
        float dist = (transform.position - checkpoints[currentCheckpoint].position).magnitude;
        if (dist < changeDist)
        {
            currentCheckpoint++;
        }
        CalcMovement(checkpoints[currentCheckpoint], 2);
        movement.SetInput(speed, xMovement);
    }
    void CalcMovement(Transform target, float _forwardSpeed)
    {
        yMovement = _forwardSpeed;
        Vector3 dirToPlayer = target.position - transform.position;
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
