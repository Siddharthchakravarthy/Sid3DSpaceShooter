using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniformCircularMotion : MonoBehaviour
{
    
    public float TimePeriod = 3.0f;
    public float Radius = 2.0f;
    public float angularVelocity;
    public float li = 0.0f;
    // Update is called once per frame
    void Update()
    {
        li = li + Time.deltaTime;
        int seconds = Mathf.FloorToInt(li % 60);
        angularVelocity = 2 * Mathf.PI/TimePeriod;
        Vector3 moveX = Vector3.right * Mathf.Cos(li * angularVelocity) * Radius;
        Vector3 moveY = Vector3.up * Mathf.Sin(li * angularVelocity) * Radius;
        transform.position = moveX + moveY;
            
    }
}
