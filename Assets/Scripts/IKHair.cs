using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHair : MonoBehaviour
{
    [SerializeField] Vector2 localAnchor;
    [SerializeField] Transform toFollow;
    float length;
    void Start()
    {
        length = Vector2.Distance(transform.position, localAnchor);
    }

    void Update()
    {
        localAnchor = toFollow.position;
        Vector2 vec = (transform.position - toFollow.position).normalized * length;
        transform.position = vec + localAnchor;

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
       // Gizmos.DrawSphere((Vector2)transform.position + localAnchor, 0.1f);
    }
}
