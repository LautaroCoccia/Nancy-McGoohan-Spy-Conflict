using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] int ammo;
    [SerializeField] int maxAmmo;
    Camera mainCamera;
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
    }

    void Shot()
    {
        if (Input.GetMouseButtonDown(0) && ammo>0)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);
            if (hit.collider != null)
            {
                hit.transform.gameObject.GetComponent<IHitable>().OnHit();
            }
            ammo--;
        }
    }

    void Reload()
    {
        if(ammo == 0 && Input.GetKeyDown(KeyCode.R))
        {
            ammo = maxAmmo;
        }
    }
}
