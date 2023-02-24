using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostBehaviour
{
    public Transform insideTransform;
    public Transform outsideTransform;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (this.gameObject.activeInHierarchy)
        {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) 
        {
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.rb.isKinematic = true;
        this.ghost.enabled = false;

        Vector3 currentPosition = this.transform.position;

        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(currentPosition, this.insideTransform.position, elapsed / duration);
            newPosition.z = currentPosition.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.insideTransform.position, this.outsideTransform.position, elapsed / duration);
            newPosition.z = currentPosition.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        this.ghost.movement.rb.isKinematic = false;
        this.ghost.enabled = true;
    }
}
