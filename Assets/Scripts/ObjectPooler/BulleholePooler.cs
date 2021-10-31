using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulleholePooler : MonoBehaviour
{
    [SerializeField] int listSize = 30;
    [SerializeField] GameObject bulletholeprefab;
    [SerializeField] GameObject bulletholesParent;
    [SerializeField] List<GameObject> bulletholesList;
    int nextToSet = 0;

    private void OnEnable()
    {
        //Weapon.SetBulletholes += SetPosAndLayer;
    }
    private void OnDisable()
    {
        //Weapon.SetBulletholes -= SetPosAndLayer;
    }
    // Start is called before the first frame update
    void Start()
    {
        bulletholesList.Capacity = listSize;
        for (int i = 0; i < listSize; i++)
        {
            
            //GameObject obj = Instantiate(bulletholeprefab);
            //obj.transform.SetParent(bulletholesParent.transform);
            //bulletholesList.Add(obj);
        }
    }

    public void SetPosAndLayer(Vector2 position, int sortingLayer)
    {
        int Slayer = sortingLayer;
        Slayer++;
        bulletholesList[nextToSet].transform.position = position;
        bulletholesList[nextToSet].gameObject.GetComponent<SpriteRenderer>().sortingOrder = Slayer;

        if (nextToSet < bulletholesList.Count - 1)
            nextToSet++;
        else
            nextToSet = 0;
    }
}
