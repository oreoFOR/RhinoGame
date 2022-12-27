using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    Movement movement;
    [SerializeField] Transform player;
    [SerializeField] Transform baby;
    [SerializeField] Transform hideout;
    Rigidbody rb;
    [SerializeField] float xMovement;
    [SerializeField] float yMovement;
    [SerializeField] float forwardSpeed = 2;
    [SerializeField] int animalSize;
    Health health;
    bool dead;
    public enum State{
        targetBaby,
        targetPlayer,
        squareingPlayer,
        running
    }
    public State state;
    void Start(){
        movement= GetComponent<Movement>();
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
        health.onDie += DisableInput;
    }
    void DisableInput(){
        dead = true;
        movement.SetInput(0,0);
    }
    void Update(){
        if(dead)return;
        movement.SetInput(yMovement,xMovement);
        if(state == State.squareingPlayer){
            CalcMovement(player,0);
        }
        else if(state == State.targetBaby){
            CalcMovement(baby,forwardSpeed);
        }
        else if(state == State.targetPlayer){
            CalcMovement(player,forwardSpeed);
        }
        else if(state == State.running){
            CalcMovement(hideout,forwardSpeed);
        }
    }
    void CalcMovement(Transform target,float _forwardSpeed){
        yMovement = _forwardSpeed;
        Vector3 dirToPlayer = target.position - transform.position;
        Vector3 lookDir = transform.forward;
        lookDir.y = 0;
        dirToPlayer.y = 0;
        float angle = AngleDir(lookDir,dirToPlayer,Vector3.up);
        if(angle <180){
            xMovement = angle * .02f;
        }
    }
    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) {
        float angle = Vector3.Angle(fwd,targetDir);
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);
		if (dir > 0f) {
			return angle;
		} else if (dir < 0f) {
			return -angle;
		} else {
			return 0f;
		}
	}
    IEnumerator Flee(){
        yield return new WaitForSeconds(Random.Range(5,7));
        state = State.targetBaby;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            if(animalSize>2){
                state = State.targetPlayer;
            }
            else{
                state = State.running;
                StartCoroutine(Flee());
            }
        }
    }
}
