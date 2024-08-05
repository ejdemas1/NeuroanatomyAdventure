using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Laser : MonoBehaviour
{

    [Header("Keybinds")]
    public KeyCode shoot = KeyCode.Mouse0;

    [Header("Energy")]
    public float energy = 100;
    public Image[] energyPoints;

    public float distance;
    public GameObject laser;
    public bool isActive = false;

    public Camera cam;

    void Awake()
    {
        laser.SetActive(false);
    }
    void Update()
    {
        if (isActive)

        {
            if (isActive)
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                //AimGun();

                if (Physics.Raycast(ray, out hit, distance))
                {
                    Transform objectHit = hit.transform;

                    if (objectHit.gameObject.tag == "Zombie")
                    {
                        Zombie zombie = objectHit.GetComponent<Zombie>();
                        if (Input.GetKeyDown(shoot))
                        {
                            // energy -= 10;
                            // UpdateEnergyDisplay();
                            Debug.Log("Shot " + objectHit.name);
                            zombie.health--;
                        }
                    }

                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopShooting();
            }
        }
    }

    void Shoot()
    {
        laser.SetActive(true);
    }

    void StopShooting()
    {
        laser.SetActive(false);
    }
}