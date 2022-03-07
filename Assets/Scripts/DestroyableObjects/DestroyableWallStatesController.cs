using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DestroyableWallStatesController : MonoBehaviour
{
    [SerializeField] List<GameObject> wallStates;
    public static Action<Transform> DeleteFromObjectList;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        DestroyableWall.setNewWallState += SetActiveWallState;
    }
    void SetActiveWallState(GameObject childWallState)
    {
        bool isMyWallState = false;
        for(int i = 0; i < wallStates.Count;i++)
        {
            if(wallStates[i].gameObject == childWallState)
            {
                isMyWallState = true;
            }
        }
        if (isMyWallState == false) return;
        wallStates[index].SetActive(false);
        
        if (index < wallStates.Count -1)
        {
            AkSoundEngine.SetSwitch("cover", "cover_break", gameObject);
            AkSoundEngine.PostEvent("cover", gameObject);
            index++;
            if(wallStates[index] != null)
            {
                wallStates[index].SetActive(true);
            }
        }
        else
        {
            AkSoundEngine.SetSwitch("cover", "cover_destroy", gameObject);
            AkSoundEngine.PostEvent("cover", gameObject);

            DeleteFromObjectList?.Invoke(transform);
            Destroy(gameObject);
        }
       
    }
    private void OnDisable()
    {
        DestroyableWall.setNewWallState -= SetActiveWallState;
    }
}
