﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGun : Weapon
{
    //[SerializeField] DamageType damageType;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GetPause())
        {
            if (Input.GetMouseButton(0) && fireTime >= fireRate && !isReloading)
            {
          //  Shoot((int)(damageType));
            }
            //Reload();
            if (fireTime < fireRate)
            {
                fireTime += Time.deltaTime;
            }
        }
    }
}
