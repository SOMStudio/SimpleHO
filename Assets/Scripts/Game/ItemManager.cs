using Base.Utility;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : ExtendedCustomMonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameObject itemObject;
    [SerializeField] private GameObject itemShadow;
    [SerializeField] private GameObject itemClickArea;
    [SerializeField] private Text itemText;

    private bool itemTaked = false;
    private bool onItemMouseDown = false;

    [Header("Items Manager")]
    [SerializeField] private ItemsManager itemsManager;

    public bool IsItemTake => itemTaked;

    public void OnItemMouseDown()
    {
        onItemMouseDown = true;
    }

    public void OnItemMouseExit()
    {
        if (onItemMouseDown)
        {
            onItemMouseDown = false;
        }
    }

    public void OnItemMouseUp()
    {
        if (onItemMouseDown)
        {
            onItemMouseDown = false;

            if (itemsManager.IsControl())
            {
                itemTaked = true;

                itemsManager.TakeItem(this);
            }
        }
    }

    public void ShowItemObject()
    {
        itemObject.SetActive(true);
    }

    public void HideItemObject()
    {
        itemObject.SetActive(false);
    }

    public void ShowItemShadow()
    {
        itemShadow.SetActive(true);
    }

    public void HideItemShadow()
    {
        itemShadow.SetActive(false);
    }

    public void ShowClickArea()
    {
        itemClickArea.SetActive(true);
    }

    public void HideClickArea()
    {
        itemClickArea.SetActive(false);
    }

    public void SetItemText(string value)
    {
        itemText.text = value;
    }
}
