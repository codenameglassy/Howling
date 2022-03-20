using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WolfMovement : MonoBehaviour
{
    [SerializeField] private AIPath aIPath;
    [SerializeField] private Seeker seeker;
    [SerializeField] private AIDestinationSetter destinationSetter;

    public Vector3 target;
    public Transform target_;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target_.position = target;
            destinationSetter.SetTarget(target_);
        }
    }
}
