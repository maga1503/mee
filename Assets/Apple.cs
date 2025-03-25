using UnityEngine;

public class Apple : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 randomPos = new Vector2(Random.Range(-8, 8), Random.Range(-4, 4));
            transform.position = randomPos;
        }
    }
}