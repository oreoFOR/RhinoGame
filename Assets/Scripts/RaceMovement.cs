using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMovement : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] VariableJoystick joystick;
    [SerializeField] float speed;
    private void Update()
    {
        movement.SetInput(2.5f + joystick.Vertical, joystick.Horizontal *2);
        speed = GetComponent<Rigidbody>().velocity.magnitude;
    }
}
