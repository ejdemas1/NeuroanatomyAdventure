using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NotifcationTriggerEvent : MonoBehaviour
{

    [Header("UI Content")]
    [SerializeField] private TextMeshProUGUI notifcationTextUI;
    [SerializeField] private Image imageIconUI;

    [Header("Message")]
    [SerializeField] public Sprite yourIcon;
    [SerializeField][TextArea] public string NotificationMessage;

    [Header("Notification Removal")]
    //[SerializeField] private bool removeAfterExist = false;
    [SerializeField] private bool disableAfterTime = false;
    [SerializeField] float disableTimer = 3.0f;

    [Header("Notifcation Animation")]
    [SerializeField] private Animator notifcationAnim;
    private BoxCollider boxCollider;

    private void Awake()
    {
        object collider = gameObject.GetComponent<BoxCollider>();
    }
    public IEnumerator EnableNotification()
    {
        notifcationAnim.Play("NotificationAnimationIn");
        notifcationTextUI.text = NotificationMessage;
        imageIconUI.sprite = yourIcon;

        if (disableAfterTime)
        {
            yield return new WaitForSeconds(disableTimer);
            RemoveNotification();
        }
    }

    private void RemoveNotification()
    {
        notifcationAnim.Play("NotificationAnimationOut");
        //gameObject.SetActive(false);
    }
}
