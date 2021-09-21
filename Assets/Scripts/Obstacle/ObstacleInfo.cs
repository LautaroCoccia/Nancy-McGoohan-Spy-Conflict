using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInfo : MonoBehaviour
{
    Transform coverPosition;
    [SerializeField] Transform[] shootPosition;
    public Vector3 GetCoverPosition => coverPosition.position;
    public int GetShootPositionLength => shootPosition.Length;
    private void Start()
    {
        coverPosition = GetComponent<Transform>();
    }

    public Vector3 GetShootPosition(int index)
    {
        return shootPosition[index].position;
    }
}
