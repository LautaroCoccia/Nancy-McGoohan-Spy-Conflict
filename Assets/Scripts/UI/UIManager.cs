using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI UIScoreNum;
    [SerializeField] List<Image> UIAmmo;
    [SerializeField] List<GameObject> goAmmo;
    int activeWeapon;
    // Start is called before the first frame update
    void Start()
    {
        activeWeapon = 0;
        Weapon.UpdateUIAmmo += UpdateAmmo;
        Weapon.ResetUIAmmo += ResetAmmo;
        Weapon.UpdateUIScore += UpdateScore;
        WeaponsController.UpdateUIAmmoImage += UpdateAmmoImage;
    }
    // Update is called once per frame
    void Update()
    {
        
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
    private void OnDisable()
    {
        Weapon.UpdateUIAmmo -= UpdateAmmo;
        Weapon.ResetUIAmmo -= ResetAmmo;
        Weapon.UpdateUIScore -= UpdateScore;
        WeaponsController.UpdateUIAmmoImage -= UpdateAmmoImage;
    }
}
