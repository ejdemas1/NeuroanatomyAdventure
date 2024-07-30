using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (int prev_val, int new_val) => {
            Debug.Log(OwnerClientId + "; randomNumber: " + randomNumber.Value);
        };
    }

    private void Update() {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            randomNumber.Value = Random.Range(0, 100);
        }

        Vector3 dir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) dir.z = 1f;
        if (Input.GetKey(KeyCode.S)) dir.z = -1f;
        if (Input.GetKey(KeyCode.A)) dir.x = 1f;
        if (Input.GetKey(KeyCode.D)) dir.x = -1f;

        float speed = 3f;
        transform.position += dir * speed * Time.deltaTime;
    }
}
