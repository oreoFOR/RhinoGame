using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCon : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float dist;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothSpeed;

    [SerializeField] GameObject startingCam;
    [SerializeField] Transform cam;
    [SerializeField] Transform baby;
    private void Start()
    {
        startingCam.transform.position = (player.position - (baby.position - player.position).normalized*(dist*.7f)+ new Vector3(0,8,0));
        startingCam.transform.LookAt(baby);
        StartCoroutine(SwitchCam());
    }
    IEnumerator SwitchCam()
    {
        yield return new WaitForSeconds(3);
        startingCam.SetActive(false);
    }
    void FixedUpdate()
    {
        Vector3 playerPos = player.position + player.forward * 5;
        Vector3 playerForward = player.forward;
        playerForward.y = 0;

        Vector3 desiredPos = playerPos - playerForward *  dist + offset;
        Vector3 smoothPos = Vector3.Lerp(cam.position, desiredPos,smoothSpeed * Time.fixedDeltaTime);
        Vector3 smoothLook = Vector3.Lerp(cam.forward, (playerPos - cam.position), smoothSpeed * Time.deltaTime);
        cam.LookAt(cam.position + smoothLook * 10) ;
        cam.position = smoothPos;
    }
}
