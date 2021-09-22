using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicItem : MonoBehaviour,IHitable
{
    [Tooltip("0 = infinity duration")]
    [SerializeField] float duration;

    void Start()
    {
        if (duration != 0)
        {
            StartCoroutine(DestroyTimer());
        }
    }
    public abstract void OnHit();

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(duration);
    }
    
}
