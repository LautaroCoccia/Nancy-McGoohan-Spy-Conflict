using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : TypeOfDamage
{
    [SerializeField] DamageType damageType;
    float initialTime;
    [SerializeField] float explosionTIme = 0.25f;
    [SerializeField] List<Sprite> explosionSprites;
    [SerializeField] SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        initialTime = explosionTIme;
        spriteRenderer.sprite = explosionSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(explosionTIme>0)
        {
            explosionTIme -= Time.deltaTime;
            if(initialTime/2 > explosionTIme)
            {
                spriteRenderer.sprite = explosionSprites[1];
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<IHitable>().OnHit((int)(damageType));
    }
}
