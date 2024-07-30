using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        laser.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopShooting();
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
