using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    [SerializeField]
    private GameObject massBreaker;

    [SerializeField]
    private GameObject cellMaker;

    private Laser laser;

    // Start is called before the first frame update
    void Awake ()
    {
        massBreaker.SetActive(true);
        cellMaker.SetActive(false);

        laser = massBreaker.GetComponent<Laser>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // if press 1 set cell maker active
        laser.isActive = true;
        // if press 2 set mass breaker active
    }
}
