using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;

    // Can have sub function in PowerPellet and overide it
    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().PelletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D cld)
    {
        if (cld.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
