using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    public static GameObject player;
    private Controls controls = null;
    public GameObject headset, bullet, engine;
    public Transform bulletSpawnLocation;
    private UnityEngine.XR.InputDevice leftcontroller, rightcontroller;

    public float timeSinceFired = 10f, timeBetweenShots = .1f, engineMaxVolume=.1f;
        
    private void Awake()
    {
        controls = new Controls();
    }
    private void OnEnable()
    {
        controls.custom.Enable();
    }
    private void OnDisable()
    {
        controls.custom.Disable();
    }

    private void Start()
    {
        player = this.gameObject;
        var leftinputdevices = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftinputdevices);
        if (leftinputdevices.Count > 0)
        {
            leftcontroller = leftinputdevices[0];
        }
        else
        {
            print("Left controller not found");
        }

        var rightinputdevices = new List<UnityEngine.XR.InputDevice>();
        desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightinputdevices);
        if (leftinputdevices.Count > 0)
        {
            rightcontroller = rightinputdevices[0];
        }
        else
        {
            print("Right controller not found");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("IsPlaying") != 0)
        {
            timeSinceFired += Time.deltaTime;
            MoveWithLeftStick();
            PitchAndYawWithRightStick();
            FireWeaponsIfTriggered();
        }
    }

    public void MoveWithLeftStick()
    {
        Vector2 movementInput;
        if (leftcontroller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out movementInput) && movementInput.magnitude > .1f)
        {
            var movespeed = 6f;
            var rotateSpeed = 80f;

            var movement = transform.forward * movementInput.y;
            var movementActual = Vector3.ProjectOnPlane(movement, transform.up);
            engine.GetComponent<AudioSource>().volume = engineMaxVolume * movement.magnitude;
            transform.position += movementActual * movespeed * Time.deltaTime;

            transform.Rotate(0, 0, -movementInput.x * Time.deltaTime * rotateSpeed);
        }
    }

    public void PitchAndYawWithRightStick()
    {
        Vector2 movementInput;
        if (rightcontroller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out movementInput) && movementInput.magnitude > .1f)
        {
            var rotateSpeed = 80f;

            transform.Rotate(movementInput.y * Time.deltaTime * rotateSpeed, 0, 0);
        }
    }

    public void FireWeaponsIfTriggered()
    {
        float weaponsFiredLeft = 0f, weaponsFiredRight = 0f;
        if (rightcontroller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out weaponsFiredLeft) || 
                leftcontroller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out weaponsFiredRight))
        {
            if ( (weaponsFiredLeft > .1f || weaponsFiredRight > .1f) && timeSinceFired >= timeBetweenShots)
            {
                FireWeapons();
                timeSinceFired = 0f;
            }
        }
    }

    public void FireWeapons()
    {
        var bulletInstance = Instantiate(bullet);
        bulletInstance.transform.position = bulletSpawnLocation.position;
        bulletInstance.transform.rotation = bulletSpawnLocation.rotation;
    }
}

