using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] GameObject item;
    public void Drop()
    {
        item.SetActive(true);
        item.transform.SetParent(null);

    }
}
