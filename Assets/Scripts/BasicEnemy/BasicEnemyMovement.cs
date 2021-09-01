using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour, IHitable
{
    enum direction {right, left, up };
    [SerializeField] private int score;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxDistance;
    [SerializeField] private float timeToHide;
    [SerializeField] direction actualDirection;
    float actualTime = 0;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        //startPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if(actualTime < timeToHide)
        {
            actualTime += Time.deltaTime;
        }
        else
        {
            EnemyMovement();
        }
    }
    public int OnHit()
    {
        Destroy(gameObject);
        return score;
    }
    void EnemyMovement()
    {
        
    }

    void BasicStart()
    {
        if (actualDirection == direction.right)
        {
            transform.position = new Vector3(maxDistance * -1, transform.position.y, transform.position.z);
        }
    }
    void BasicMovement()
    {
        if (actualDirection == direction.right)
        {
            if (transform.position.x < maxDistance)
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
            if (transform.position.x > maxDistance * -1)
            {
                transform.position = new Vector3(transform.position.x - (movementSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            }
            else
            {
                actualDirection = direction.right;
            }
        }
    }
}