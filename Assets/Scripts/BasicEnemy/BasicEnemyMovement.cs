using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour, IHitable
{
    enum direction {right, left };
    
    [SerializeField] private float movementSpeed;
    [SerializeField] direction actualDirection;
    [SerializeField] private float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        maxDistance = Mathf.Abs(maxDistance);
        if(actualDirection == direction.right)
        {
            transform.position = new Vector3(maxDistance * -1, transform.position.y, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(actualDirection == direction.right)
        {
            if(transform.position.x <maxDistance)
            {
                transform.position = new Vector3(transform.position.x + (movementSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            }
            else
            {
                actualDirection = direction.left;
            }
        }
        else if (actualDirection == direction.left)
        {
            if (transform.position.x > maxDistance*-1)
            {
                transform.position = new Vector3(transform.position.x - (movementSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            }
            else
            {
                actualDirection = direction.right;
            }
        }
    }
    public void OnHit()
    {
        Destroy(gameObject);
    }
}
