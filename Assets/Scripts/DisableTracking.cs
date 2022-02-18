using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DisableTracking : MonoBehaviour
{
  public Camera mainCamera;
  private Quaternion rotationLocked;
  private Vector3 positionLocked;
  private bool start;

  private List<InputDevice> devices = new List<InputDevice>();
  private InputDevice headset;

  private InputDevice buttonA;
  private bool prevButtonStateA = false;
  private bool isPositionLocked = false;

  private InputDevice buttonB;
  private bool prevButtonStateB = false;
  private bool isRotationLocked = false;

  void Start()
  {
    // mainCamera = GetComponentInChildren<Camera>();
    InputDevices.GetDevices(devices);
    foreach (var item in devices)
    {
      if (item.TryGetFeatureValue(CommonUsages.centerEyeRotation, out Quaternion headsetRotation))
      {
        headset = item;
      }
      if (item.TryGetFeatureValue(CommonUsages.primaryButton, out bool result))
      {
        buttonA = item;
      }
      if (item.TryGetFeatureValue(CommonUsages.secondaryButton, out result))
      {
        buttonB = item;
      }
    }
  }

  private void Update()
  {
    IsButtonPressed();

    if (isPositionLocked)
    {
      headset.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 headsetPosition);
      this.transform.position = new Vector3(
        -headsetPosition.x + positionLocked.x,
        -headsetPosition.y + positionLocked.y,
        -headsetPosition.z + positionLocked.z
      );
    }
    if (isRotationLocked)
    {
      headset.TryGetFeatureValue(CommonUsages.centerEyeRotation, out Quaternion headsetRotation);
      this.transform.rotation = rotationLocked * (Quaternion.Inverse(headsetRotation) * Quaternion.identity);
    }
  }

  void IsButtonPressed()
  {

    if (buttonA.TryGetFeatureValue(CommonUsages.primaryButton, out bool isButtonAPressed) && isButtonAPressed && !prevButtonStateA)
    {
      if (!isPositionLocked) StartLockPosition();
      isPositionLocked = !isPositionLocked;
    }
    if (buttonB.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isButtonBPressed) && isButtonBPressed && !prevButtonStateB)
    {
      if (!isRotationLocked) StartLockRotation();
      isRotationLocked = !isRotationLocked;
    }

    prevButtonStateA = isButtonAPressed;
    prevButtonStateB = isButtonBPressed;
  }

  void StartLockRotation()
  {
    headset.TryGetFeatureValue(CommonUsages.centerEyeRotation, out Quaternion headsetRotation);
    rotationLocked = headsetRotation;
  }

  void StartLockPosition()
  {
    headset.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 headsetPosition);
    positionLocked = headsetPosition;

  }
}
