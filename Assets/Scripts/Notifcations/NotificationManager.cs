using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] public NotifcationTriggerEvent notificationTrigger;
    [SerializeField] public Sprite myelinFixedIcon;

    public void TriggerMyelinFixNotification()
    {
        notificationTrigger.NotificationMessage = "Myelin fixed!";
        notificationTrigger.yourIcon = myelinFixedIcon;
        StartCoroutine(notificationTrigger.EnableNotification());
    }

}
