using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWallStatesController : MonoBehaviour
{
    [SerializeField] List<GameObject> wallStates;

    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        DestroyableWall.setNewWallState += SetActiveWallState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetActiveWallState()
    {
        wallStates[index].SetActive(false);
        
        if (index < wallStates.Count -1)
        {
            index++;
            if(wallStates[index] != null)
            {
                wallStates[index].SetActive(true);
            }
        }
        else
        {
            Destroy(gameObject);
        }
       
    }
    private void OnDisable()
    {
        DestroyableWall.setNewWallState -= SetActiveWallState;
    }
}
