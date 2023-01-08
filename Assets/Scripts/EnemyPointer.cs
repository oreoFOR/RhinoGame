using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointer : MonoBehaviour
{
    Transform target;
    [SerializeField] RectTransform arrow;
    private void Update()
    {
        Vector3 toPos = target.position;
    }
}
