using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAnimations : MonoBehaviour
{
    [SerializeField] Animator enemyAnims;

    // Start is called before the first frame update
    void Start()
    {
        SetIdleAnim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetIdleAnim()
    {
        enemyAnims.SetTrigger("IdleAnim");
    }
    void SetShootAnim()
    {
        enemyAnims.SetTrigger("ShootAnim");
    }
}
