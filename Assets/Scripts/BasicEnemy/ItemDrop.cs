using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] GameObject item;
    private void OnDisable()
    {
        item.transform.SetParent(null);
        item.SetActive(true);
    }
}
