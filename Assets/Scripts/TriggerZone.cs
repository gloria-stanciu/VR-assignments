using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class TriggerZone : MonoBehaviour
{
    private Collider zone;
    public GameObject spheres;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider) {
        spheres.AddComponent<Rigidbody>();
    }
    // private void OnTriggerExit(Collider collider) {
    //     Destroy(spheres.GetComponent<Rigidbody>());
    // }
}
