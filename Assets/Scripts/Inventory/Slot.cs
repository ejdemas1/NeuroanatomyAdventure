using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] List<Sprite> icons;
    public bool isHovered;
    public TMP_Text text;
    private Item item;
    private Image image;
    private Color opqaue = new Color(1,1,1,1);
    private Color transparent = new Color(1,1,1,0);

    private void Update() {
        if (isHovered && item != null && Input.GetMouseButtonDown(0))
        {
            description.text = item.description;
            title.text = item.item_name;
        }
    }

    public void InitializeSlot()
    {
        image = gameObject.GetComponent<Image>();
        image.sprite = null;
        image.color = transparent;
        SetItem(null);
    }

    public void SetItem(Item item_)
    {
        item = item_;

        if (item != null)
        {
            image.sprite = item.icon;
            image.color = opqaue;
            updateData();
        }
        else
        {
            image.sprite = null;
            image.color = transparent;
            updateData();
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public int GetItemCount()
    {
        return item.quantity;
    }

    public string GetItemName()
    {
        return item.item_name;
    }

    public void ReduceItem(int quantity)
    {
        item.quantity -= quantity;
    }

    public void updateData()
    {
        if (item != null)
            text.text = item.quantity.ToString();
        else
            text.text = "";
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        isHovered = false;
    }
}
