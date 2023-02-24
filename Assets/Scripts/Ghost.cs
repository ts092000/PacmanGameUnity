using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; }

    public GhostHome ghostHome { get; private set; }
    public GhostScatter ghostScatter { get; private set; }
    public GhostChase ghostChase { get; private set; }
    public GhostFrightened ghostFrightened { get; private set; }

    public GhostBehaviour initialBehaviour;
    public Transform target;

    public int points = 200;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.ghostHome = GetComponent<GhostHome>();
        this.ghostScatter = GetComponent<GhostScatter>();
        this.ghostChase = GetComponent<GhostChase>();
        this.ghostFrightened = GetComponent<GhostFrightened>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.ghostScatter.Enable();
        this.ghostChase.Disable();
        this.ghostFrightened.Disable();

        if (this.ghostHome != this.initialBehaviour)
        {
            this.ghostHome.Disable();
        }

        if (this.initialBehaviour != null)
        {
            this.initialBehaviour.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.ghostFrightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
