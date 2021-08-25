using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] Image UIAmmo;
    [SerializeField] TextMeshProUGUI UIScoreNum;
    // Start is called before the first frame update
    void Start()
    {
        WeaponsController.UpdateUIAmmo += UpdateAmmo;
        WeaponsController.ResetUIAmmo += ResetAmmo;
        WeaponsController.UpdateUIScore += UpdateScore;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateAmmo(float actualAmmo)
    {
        float ammo = 1 / actualAmmo;
        UIAmmo.fillAmount -= ammo;
    }
    void UpdateScore(int score)
    {
        UIScoreNum.text = score.ToString();
    }
    void ResetAmmo()
    {
        UIAmmo.fillAmount = 1;
    }
    private void OnDisable()
    {
        WeaponsController.UpdateUIAmmo -= UpdateAmmo;
        WeaponsController.ResetUIAmmo -= ResetAmmo;
        WeaponsController.UpdateUIScore -= UpdateScore;
    }
}
