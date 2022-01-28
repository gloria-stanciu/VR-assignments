using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingMoon : MonoBehaviour
{
    public float degreesPerSecond = 2.0f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0);
    }
}
