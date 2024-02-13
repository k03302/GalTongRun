using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealWormControl : MonoBehaviour
{
    public Transform MealWorm;
    public Transform CuteWorm;
    public Transform Broccoli;

    public static string ItemType = "CuteWorm";

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
            case "MealWorm":
                MealWorm.gameObject.SetActive(true);
                CuteWorm.gameObject.SetActive(false);
                Broccoli.gameObject.SetActive(false);
                break;
            case "CuteWorm":
                MealWorm.gameObject.SetActive(false);
                CuteWorm.gameObject.SetActive(true);
                Broccoli.gameObject.SetActive(false);
                break;
            case "Broccoli":
                MealWorm.gameObject.SetActive(false);
                CuteWorm.gameObject.SetActive(false);
                Broccoli.gameObject.SetActive(true);
                break;
        }
    }


}
