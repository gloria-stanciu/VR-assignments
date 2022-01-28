using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerActions : MonoBehaviour
{
    public GameObject outsidePlane;
    public GameObject triggerArea;

    public Light lightSource;
    private bool isWhite = true; //true-switch to red false-switch to white

    public GameObject player;
    private Vector3 initialPosition;

    private InputDevice leftHand;
    private InputDevice rightHand;

    private bool isInside;


    private bool primaryButtonValue;
    private bool previousPrimaryButtonValue;
    private bool secondaryButtonValue;
    private bool previousSecondaryButtonValue;
    private bool triggerButtonValue;
    private bool previousTriggerButtonValue;
    private bool gripButtonValue;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        foreach(InputDevice device in devices) {
            if(device.characteristics.HasFlag(InputDeviceCharacteristics.Left)) {
                leftHand = device;
            } else if(device.characteristics.HasFlag(InputDeviceCharacteristics.Right)) {
                rightHand = device;
            }
        }

        // lightSource = GetComponent<Light>();

        initialPosition = player.transform.position; //player's initial position

        isInside = true; //initially inside the room

    }

    // Update is called once per frame
    void Update()
    {
        if(leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonValue) && primaryButtonValue && !previousPrimaryButtonValue) {
            outsideInside();
        }

        if(rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonValue) && primaryButtonValue && !previousPrimaryButtonValue) {
            outsideInside();
        }

        if(leftHand.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonValue) && secondaryButtonValue && !previousSecondaryButtonValue) {
            triggerZone();
        }

        if(rightHand.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonValue) && secondaryButtonValue && !previousSecondaryButtonValue) {
            triggerZone();
        }

        if(leftHand.TryGetFeatureValue(CommonUsages.gripButton, out gripButtonValue) && gripButtonValue) {
            quitApp();            
        }

        if(rightHand.TryGetFeatureValue(CommonUsages.gripButton, out gripButtonValue) && gripButtonValue) {
            quitApp();
        }

        if(leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue && !previousTriggerButtonValue) {
            switchLight();            
        }

        if(rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue && !previousTriggerButtonValue) {
            switchLight();
        }
        previousPrimaryButtonValue = primaryButtonValue;
        previousSecondaryButtonValue = secondaryButtonValue;
        previousTriggerButtonValue = triggerButtonValue;
    }


    void outsideInside(){
        if(isInside == true){
            player.transform.position = outsidePlane.transform.position;
            isInside = false;
        } else if(isInside == false){
            player.transform.position = initialPosition;
            isInside = true;
        }
    }

    void triggerZone(){
        if(isInside == true){
            player.transform.position = triggerArea.transform.position;
            isInside = false;
        } else if(isInside == false){
            player.transform.position = initialPosition;
            isInside = true;
        }
    }

    void quitApp(){
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #else 
            Application.Quit();
        #endif
    }

    void switchLight(){
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
