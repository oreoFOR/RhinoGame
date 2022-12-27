using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCon : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float dist;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothSpeed;
    void FixedUpdate()
    {
        Vector3 playerForward = player.forward;
        playerForward.y = 0;

        Vector3 desiredPos = player.position - playerForward *  dist + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos,smoothSpeed * Time.fixedDeltaTime);
        transform.LookAt(player);
        transform.position = smoothPos;
    }
}
