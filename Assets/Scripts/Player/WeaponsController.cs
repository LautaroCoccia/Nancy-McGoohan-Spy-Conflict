using System;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] int ammo;
    [SerializeField] int maxAmmo;
    [SerializeField] int targetLayer;
    [SerializeField] float fireRate;
    float fireTime;
    Camera mainCamera;
    public static Action<float> UpdateUIAmmo;
    public static Action ResetUIAmmo;
    public static Action<int> UpdateUIScore;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        Shot();
        Reload();
        fireTime += Time.deltaTime;
    }
    void Shot()
    {
        if (Input.GetMouseButton(0) && ammo > 0 && fireTime >= fireRate)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);
            
            if (hit.collider != null && hit.transform.gameObject.layer == targetLayer)
            {
                score += hit.transform.gameObject.GetComponent<IHitable>().OnHit();
                UpdateUIScore?.Invoke(score);
            }
            ammo--;
            fireTime = 0;
            UpdateUIAmmo?.Invoke(maxAmmo);
        }
    }
    void Reload()
    {
        if(ammo == 0 && Input.GetKeyDown(KeyCode.R))
        {
            ammo = maxAmmo;
            ResetUIAmmo?.Invoke();
        }
    }
}