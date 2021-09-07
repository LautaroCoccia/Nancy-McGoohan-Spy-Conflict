using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int killCounter;
    int score;
    public int ammo;
    public int maxAmmo;
    public int targetLayer;
    public float fireRate;
    public float fireTime;
    public Camera mainCamera;

    public static Action<float> UpdateUIAmmo;
    public static Action ResetUIAmmo;
    public static Action<int> UpdateUIScore;
    public static Action<int> UpdateUIKillCounter;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        killCounter = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shot()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

        if (hit.collider != null && hit.transform.gameObject.layer == targetLayer && hit.transform.tag != "EnemyShield")
        {
            killCounter++;
            score += hit.transform.gameObject.GetComponent<IHitable>().OnHit();
            UpdateUIScore?.Invoke(score);
            UpdateUIKillCounter?.Invoke(killCounter);
        }
        ammo--;
        fireTime = 0;
        UpdateUIAmmo?.Invoke(maxAmmo);
    }
    public void ShotShotgun()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

        if (hit.collider != null && hit.transform.gameObject.layer == targetLayer)
        {
            if(hit.transform.gameObject.GetComponent<IHitable>().OnHit() != 25)
            {
                killCounter++;
                UpdateUIKillCounter?.Invoke(killCounter);
            }
            score += hit.transform.gameObject.GetComponent<IHitable>().OnHit();
            UpdateUIScore?.Invoke(score);
        }
        ammo--;
        fireTime = 0;
        UpdateUIAmmo?.Invoke(maxAmmo);
    }
    public void Reload()
    {
        if (ammo == 0 && Input.GetKeyDown(KeyCode.R))
        {
            ammo = maxAmmo;
            ResetUIAmmo?.Invoke();
        }
    }
}