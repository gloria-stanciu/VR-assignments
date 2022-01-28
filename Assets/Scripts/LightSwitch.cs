using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light lightSource;
    public bool isWhite = true; //true-switch to red false-switch to white
    // Start is called before the first frame update
    void Start()
    {
        lightSource = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            if(isWhite == true){
                lightSource.color = new Color(0.9f, 0.2f, 0.2f);
                isWhite = false;
            }
            else if(isWhite == false){
                lightSource.color = new Color(0f, 0f, 0f);
                isWhite = true;
            }
        }
    }
}
