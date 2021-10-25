using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : BaseDestroyableObject, IHitable
{
    [SerializeField] GameObject explotionPrefab;
    public void OnHit(TypeOfDamage.DamageType typeOfDamage)
    {
        lives--;
        lvlManager.AddScore(score);
        if (lives == 0)
        {
            GameObject obj = Instantiate(explotionPrefab);
            obj.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
