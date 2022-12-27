using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] VariableJoystick joystick;
    Movement movement;
    private void Start()
    {
        movement= GetComponent<Movement>();
    }
    void Update(){
        movement.SetInput(joystick.Vertical,joystick.Horizontal);
    }
}
