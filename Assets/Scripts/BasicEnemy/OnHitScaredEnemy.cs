using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemDrop))]
public class OnHitScaredEnemy : MonoBehaviour,IHitable
{
    ItemDrop itemDrop;
    [SerializeField] int score;
    LevelManager lvlManager = LevelManager.Get();
    private void Start()
    {
        itemDrop = GetComponent<ItemDrop>();
    }
    public void OnHit()
    {
        itemDrop.Drop();
        lvlManager.AddScore(score);
        lvlManager.AddKill();
        Destroy(gameObject);
    }
}
