using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connection;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Vector3 currentPosition = other.transform.position;
        currentPosition.x = this.connection.position.x;
        currentPosition.y = this.connection.position.y;

        other.transform.position = currentPosition;
    }
}
