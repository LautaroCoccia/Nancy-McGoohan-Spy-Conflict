﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI UIScoreNum;
    [SerializeField] TextMeshProUGUI UICounterNum;
    [SerializeField] TextMeshProUGUI UITimer;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] List<Image> UIAmmo;
    [SerializeField] List<GameObject> goAmmo;
    [SerializeField] List<Image> UICrosshair;
    [SerializeField] float timeToLose = 20;
    int activeWeapon;
    // Start is called before the first frame update
    void Start()
    {
        activeWeapon = 0;
        Weapon.UpdateUIAmmo += UpdateAmmo;
        Weapon.ResetUIAmmo += ResetAmmo;
        LevelManager.UpdateUIScore += UpdateScore;
        LevelManager.UpdateUIKillCounter += UpdateKillCounter;
        LevelManager.UpdateUITimer += UpdateTimer;
        WeaponsController.UpdateUIAmmoImage += UpdateAmmoImage;
    }
    // Update is called once per frame
    void Update()
    {
        if(timeToLose>0)
        {
            timeToLose -= Time.deltaTime;
        }
        else
        {
            loseScreen.SetActive(true);

        }
    }
    void UpdateAmmo(float actualAmmo)
    {
        float ammo = 1 / actualAmmo;
        UIAmmo[activeWeapon].fillAmount -= ammo;
    }
    void UpdateScore(int score)
    {
        UIScoreNum.text = score.ToString();
    }
    void ResetAmmo()
    {
        UIAmmo[activeWeapon].fillAmount = 1;
    }
    void UpdateAmmoImage(int newValue)
    {
        activeWeapon = newValue;
        for (int i = 0; i < goAmmo.Count; i++)
        {
            if (goAmmo[activeWeapon] != goAmmo[i])
            {
                goAmmo[i].SetActive(false);
            }
            else
            {
                goAmmo[i].SetActive(true);
            }
        }
    }
    void UpdateKillCounter(int killCounter)
    {
        UICounterNum.text = killCounter.ToString();
        if(killCounter == 5)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
    void UpdateTimer(float timer)
    {
        UITimer.text = Mathf.Round(timer).ToString();
    }
    private void OnDisable()
    {
        WeaponsController.UpdateUIAmmoImage -= UpdateAmmoImage;
        LevelManager.UpdateUITimer -= UpdateTimer;
        LevelManager.UpdateUIKillCounter -= UpdateKillCounter;
        LevelManager.UpdateUIScore -= UpdateScore;
        Weapon.ResetUIAmmo -= ResetAmmo;
        Weapon.UpdateUIAmmo -= UpdateAmmo;
    }
}