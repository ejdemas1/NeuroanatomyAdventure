using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class MyelinFixer : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode fixKey = KeyCode.Mouse0;

    [Header("Notifcation")]
    [SerializeField] private NotificationManager notificationManager;

    // public NerveShader nerveShaderManager;
    public Camera cam;

    [SerializeField] private Transform gunTransform;
    public Vector3 gunRotationOffset = new Vector3(-90, -90, 0);

    public LayerMask myelinMask;
    public float distance;

    [Header("Energy")]
    public float energy = 100;
    public Image[] energyPoints;

    private void Start()
    {
        UpdateEnergyDisplay();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        AimGun();

        if (Physics.Raycast(ray, out hit, distance, myelinMask))
        {
            Transform objectHit = hit.transform;

            if (objectHit.gameObject.layer == LayerMask.NameToLayer("Myelin"))
            {
                Myelin myelin = objectHit.GetComponent<Myelin>();
                if (Input.GetKeyDown(fixKey))
                {
                    if (myelin.isConnected == false)
                    {
                        //nerveShaderManager.FixMyelin(objectHit.GetComponent<Myelin>().idx);
                        myelin.isConnected = true;
                        energy -= 10;
                        UpdateEnergyDisplay();
                        //Debug.Log("Shot " + objectHit.name + myelin.isConnected);
                        notificationManager.TriggerMyelinFixNotification();
                    }
                }
            }

        }
    }

    private void UpdateEnergyDisplay()
    {
        for (int i = 0; i < energyPoints.Length; i++)
        {
            energyPoints[i].enabled = i * 10 < energy;
        }
    }

    private void AimGun()
    {
        // Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        // Vector3 direction = ray.direction;

        // // Apply the rotation to the gunTransform
        // gunTransform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(gunRotationOffset);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Get the point where the ray hits
            Vector3 targetPoint = hit.point;

            // Calculate the direction from the gun's position to the target point
            Vector3 directionToTarget = (targetPoint - gunTransform.position).normalized;

            // Get the camera's forward direction
            Vector3 cameraForward = cam.transform.forward;

            // Calculate a blended direction: the gun aims towards the target and follows the camera's up/down direction
            Vector3 blendedDirection = Vector3.Lerp(directionToTarget, cameraForward, 0.5f);

            // Apply the rotation to the gunTransform
            gunTransform.rotation = Quaternion.LookRotation(blendedDirection) * Quaternion.Euler(gunRotationOffset);
        }
        else
        {
            // If the raycast didn't hit anything, just use the camera's forward direction
            Vector3 direction = cam.transform.forward;
            gunTransform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(gunRotationOffset);
        }

    }
}
