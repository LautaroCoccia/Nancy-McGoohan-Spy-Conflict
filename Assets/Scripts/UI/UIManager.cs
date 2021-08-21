using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] Image UIAmmo;
    // Start is called before the first frame update
    void Start()
    {
        WeaponsController.UpdateUIAmmo += UpdateAmmo;
        WeaponsController.ResetUIAmmo += ResetAmmo;
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
    void ResetAmmo()
    {
        UIAmmo.fillAmount = 1;
    }
    private void OnDisable()
    {
        WeaponsController.UpdateUIAmmo -= UpdateAmmo;
        WeaponsController.ResetUIAmmo -= ResetAmmo;
    }
}
