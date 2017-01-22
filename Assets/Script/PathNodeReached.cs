using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNodeReached : MonoBehaviour
{
    public int _mobSpawnNumber;
    public int _pathNodeNumber;
    public Transform _pathNode;

    void Start() {
        _pathNodeNumber = 1;
        _pathNode = gameObject.transform.GetChild(0);
    }
}
