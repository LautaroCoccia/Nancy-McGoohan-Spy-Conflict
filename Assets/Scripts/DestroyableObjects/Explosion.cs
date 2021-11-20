using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] bool destroy;
    [SerializeField] float explosionTime = 0.25f;

    // Update is called once per frame
    void Update()
    {
        if(destroy)
        {
            explosionTime -= Time.deltaTime;
            if(explosionTime <= 0)
            {
                Destroy(gameObject);
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<IHitable>().OnHit(Weapon.DamageInfo.strong);
    }
    
}
