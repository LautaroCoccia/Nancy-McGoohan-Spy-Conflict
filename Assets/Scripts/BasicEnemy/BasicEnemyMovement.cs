using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
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
            transform.position = new Vector3(maxDistance * -1, 0,0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(actualDirection == direction.right)
        {
            if(transform.position.x <maxDistance)
            {
                transform.position = new Vector3(transform.position.x + (movementSpeed * Time.deltaTime), transform.position.y, 0);
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
                transform.position = new Vector3(transform.position.x - (movementSpeed * Time.deltaTime), transform.position.y, 0);
            }
            else
            {
                actualDirection = direction.right;
            }
        }
    }
}
