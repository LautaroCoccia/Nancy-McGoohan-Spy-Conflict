using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float initialTime;
    [SerializeField] float explosionTime = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        initialTime = explosionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(explosionTime>0)
        {
            explosionTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<IHitable>().OnHit(Weapon.DamageInfo.strong);
    }
}
