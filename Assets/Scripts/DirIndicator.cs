using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirIndicator : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float dist;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothSpeed;
    void FixedUpdate()
    {
        Vector3 playerPos = player.position + player.forward * 5;
        Vector3 playerForward = player.forward;
        playerForward.y = 0;

        Vector3 desiredPos = playerPos - playerForward * dist + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.fixedDeltaTime);
        Vector3 smoothLook = Vector3.Lerp(transform.forward, (playerPos - transform.position), smoothSpeed * Time.deltaTime);
        transform.LookAt(transform.position + smoothLook * 10);
        transform.position = smoothPos;
    }
}
