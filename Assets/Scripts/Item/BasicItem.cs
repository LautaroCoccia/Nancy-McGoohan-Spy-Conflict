using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicItem : MonoBehaviour,IHitable
{
    [Tooltip("0 = infinity duration")]
    [SerializeField] float duration;
    float posZ = -1;
    void Start()
    {
        if (duration != 0)
        {
            StartCoroutine(DestroyTimer());
        }
    }
    private void OnEnable()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, posZ);
    }
    public abstract void OnHit(Weapon.DamageInfo damageInfo);

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(duration);
    }
    
}
