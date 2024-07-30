using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Myelin : MonoBehaviour
{
    public int idx;
    public bool isConnected;

    [SerializeField] public Material brokenMaterial;
    [SerializeField] public Material fixedMaterial;


    private BoxCollider boxCollider;

    private void Awake()
    {
        //boxCollider = GetComponent<BoxCollider>();
        //boxCollider.enabled = false;
        isConnected = false;
    }

    private void Update()
    {
        if (isConnected)
        {
            gameObject.GetComponent<MeshRenderer>().material = fixedMaterial;
        }
        else {
            gameObject.GetComponent<MeshRenderer>().material = brokenMaterial;
        }
    }
}
