using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float accel;
    [SerializeField] float topSpeed;
    [SerializeField] float killSpeed;
    [SerializeField] Vector3 velocity;

    [SerializeField] Transform dirIndicator;
    [SerializeField] ParticleSystem speedPs;
    //rotation
    [SerializeField] float rotSpeed;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDist;
    [SerializeField] float minTurnDist = 1;

    public bool grounded;
    Vector3 lastDir;
    Rigidbody rb;
    bool charging;

    float horizontal;
    float vertical;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastDir = Vector3.forward;
    }
    public void SetInput(float v, float h){
        vertical = v;
        horizontal = h;
    }
    private void FixedUpdate()
    {
        float vel = rb.velocity.magnitude;
        Vector3 fDir = dirIndicator.forward;
        fDir.y = 0;
        Vector3 xDir = dirIndicator.right;
        xDir.y = 0;
        if(grounded)
        rb.AddForce((fDir * vertical + xDir * horizontal)*accel);
        if(vel > topSpeed){
            rb.velocity = rb.velocity.normalized * topSpeed;
        }
        if(vel > killSpeed && !charging){
            Charge();
        }
        else if(vel < killSpeed && charging){
            SlowDown();
        }
        AdjustLook();
    }
    void AdjustLook(){
        if(rb.velocity.magnitude > minTurnDist){
            lastDir = rb.velocity;
        }
        Vector3 currentForward = transform.forward;
        Vector3 smoothDir = Vector3.Lerp(currentForward,lastDir.normalized,rotSpeed*Time.fixedDeltaTime);
        rb.MoveRotation(Quaternion.LookRotation(smoothDir));
    }
    private void Update()
    {
        grounded = Physics.Raycast(groundCheck.position,Vector3.down,out RaycastHit hit,groundDist,groundMask);
        if(grounded){
            Vector3 rightVel = Vector3.Cross(rb.velocity.normalized,Vector3.down);
            Vector3 desiredDir = Vector3.Cross(rightVel,hit.normal);
            //lastDir = desiredDir;
        }
    }
    void Charge(){
        charging = true;
        speedPs.Play();

    }
    void SlowDown(){
        charging = false;
        speedPs.Stop(true,ParticleSystemStopBehavior.StopEmitting);
    }
}
