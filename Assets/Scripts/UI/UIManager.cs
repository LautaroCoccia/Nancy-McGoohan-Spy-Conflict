using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [Serializable]
    public class CrosshairByWeapon
    {
        public WeaponsController.WeaponType weaponType;
        public Sprite normal;
        public Sprite enemyHit;
        public Sprite outOfAmmo;
    }

    public List<CrosshairByWeapon> crossHairs;

    public CrosshairByWeapon currentCrosshair;

    [SerializeField] Image crosshairImage;

    [SerializeField] TextMeshProUGUI UIScoreNum;
    [SerializeField] TextMeshProUGUI UICounterNum;
    [SerializeField] TextMeshProUGUI UITimer;
    [SerializeField] Image UIHealth;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] List<Image> UIAmmo;
    [SerializeField] List<GameObject> goAmmo;
    [SerializeField] List<Image> UICrosshair;
    int activeWeapon;
    float timeToChange = 2.0f;
    [SerializeField] MenuManager mySceneChanger;
    // Start is called before the first frame update
    private void OnEnable()
    {
        activeWeapon = 0;
        Weapon.UpdateUIAmmo += UpdateAmmo;
        Weapon.ResetUIAmmo += ResetAmmo;
        Weapon.OutOfAmmoCrosshair += OnOutOfAmmo;
        Weapon.HitCrosshair += OnEnemyHit;
        Weapon.NormalCrosshair += OnNormal;
        LevelManager.UpdateUIScore += UpdateScore;
        LevelManager.UpdateUIKillCounter += UpdateKillCounter;
        LevelManager.UpdateUITimer += UpdateTimer;
        LevelManager.UpdateUIHealth += UpdateHealth;
        LevelManager.LoseCondition += Lose;
        WeaponsController.OnWeaponChanged += WeaponChanged;
    }
    private void OnDisable()
    {
        Weapon.UpdateUIAmmo -= UpdateAmmo;
        Weapon.ResetUIAmmo -= ResetAmmo;
        Weapon.OutOfAmmoCrosshair -= OnOutOfAmmo;
        Weapon.HitCrosshair -= OnEnemyHit;
        Weapon.NormalCrosshair -= OnNormal;
        LevelManager.UpdateUIScore -= UpdateScore;
        LevelManager.UpdateUIKillCounter -= UpdateKillCounter;
        LevelManager.UpdateUITimer -= UpdateTimer;
        LevelManager.UpdateUIHealth -= UpdateHealth;
        LevelManager.LoseCondition -= Lose;
        WeaponsController.OnWeaponChanged -= WeaponChanged;
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
    void WeaponChanged(WeaponsController.WeaponType newValue)
    {
        for (int i = 0; i < crossHairs.Count; i++)
        {
            if (newValue == crossHairs[i].weaponType)
                currentCrosshair = crossHairs[i];
                
        }

        crosshairImage.sprite = currentCrosshair.normal;

        activeWeapon = (int) newValue;
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

    void OnEnemyHit()
    {
        crosshairImage.sprite = currentCrosshair.enemyHit;
    }

    void OnOutOfAmmo()
    {
        crosshairImage.sprite = currentCrosshair.outOfAmmo;
    }
    void OnNormal()
    {
        crosshairImage.sprite = currentCrosshair.normal;
    }

    void UpdateKillCounter(int killCounter)
    {
        UICounterNum.text = killCounter.ToString() + "/10";
        if(killCounter == 10)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0;
            StartCoroutine(ChangeToMenu());
        }
    }
    void UpdateTimer(float timer)
    {
        UITimer.text = Mathf.Round(timer).ToString();
    }
    void UpdateHealth(int health,int maxHealth)
    {
        UIHealth.fillAmount =  ((float)health) / maxHealth;
    }
    void Lose()
    {
        StartCoroutine(ChangeToMenu());
    }
    IEnumerator ChangeToMenu()
    {
        yield return new WaitUntil(() => !ScreenShake.isShaking);
        Time.timeScale = 0;
        loseScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(timeToChange);
        mySceneChanger.ChangeScene();
    }
}