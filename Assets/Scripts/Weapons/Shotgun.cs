using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ammo > 0 && fireTime >= fireRate)
        {
            ShotShotgun();
        }
        Reload();
        if (fireTime < fireRate)
        {
            fireTime += Time.deltaTime;
        }
    }
}
