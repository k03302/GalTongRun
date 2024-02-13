using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HambergerControl : MonoBehaviour
{
    public Transform Hamberger;
    public Transform Maratang;

    public static string ItemType = "Hamberger";

    public static void SetItemType(string itemType)
    {
        ItemType = itemType;
    }


    public static string GetItemType()
    {
        return ItemType;
    }


    private void Start()
    {
        switch (ItemType)
        {
            case "Hamberger":
                Hamberger.gameObject.SetActive(true);
                Maratang.gameObject.SetActive(false);
                break;
            case "Maratang":
                Hamberger.gameObject.SetActive(false);
                Maratang.gameObject.SetActive(true);
                break;
        }
    }
}
