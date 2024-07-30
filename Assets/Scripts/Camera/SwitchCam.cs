using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchCam : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private Canvas aimCanvas;
    private InputAction aimAction;
    private CinemachineVirtualCamera vcam;
    private void Awake() {
        aimAction = playerInput.actions["Aim"];
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable() {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => EndAim();
    }

    private void OnDisable() {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => EndAim();
    }

    private void StartAim()
    {
        if (Time.timeScale != 0)
        {
            vcam.Priority += 10;
            aimCanvas.enabled = true;
        }
    }

    private void EndAim()
    {
        vcam.Priority -= 10;
        aimCanvas.enabled = false;
    }
}
