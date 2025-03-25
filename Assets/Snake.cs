using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    public float speed = 0.4f;
    public Vector2 direction = Vector2.right;
    public GameObject bodyPrefab;
    private List<Transform> bodyParts = new List<Transform>(); // Список сегментов

    public void Start()
    {
        InvokeRepeating("Move", speed, speed);
    }

    public void Move()
    {


        Vector2 newPos = (Vector2)transform.position + direction;
        transform.position = newPos;

        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            bodyParts[i].position = bodyParts[i - 1].position;
        }

        if (bodyParts.Count > 0)
            bodyParts[0].position = newPos;

        CheckBounds(); // Проверяем выход за границы
    }

    public void ChangeDirection(string newDirection)
    {
        if (newDirection == "Up" && direction != Vector2.down) direction = Vector2.up;
        if (newDirection == "Down" && direction != Vector2.up) direction = Vector2.down;
        if (newDirection == "Left" && direction != Vector2.right) direction = Vector2.left;
        if (newDirection == "Right" && direction != Vector2.left) direction = Vector2.right;
    }

    public void Grow()
    {
        GameObject newBody = Instantiate(bodyPrefab);
        newBody.transform.position = transform.position;
        bodyParts.Add(newBody.transform);
        // Если есть сегменты, добавляем новый за последний
        if (bodyParts.Count > 0)
        {
            newBody.transform.position = bodyParts[bodyParts.Count - 1].position;
        }
        else
        {
            newBody.transform.position = transform.position;
        }

        bodyParts.Add(newBody.transform);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Grow();
            Destroy(other.gameObject);
        }
    }


    public void CheckBounds()
    {
        // Получаем границы экрана в мировых координатах
        float screenLeft = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        float screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        float screenBottom = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        float screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

        Vector3 pos = transform.position;

        // Проверяем выход за границы и телепортируем на противоположную сторону
        if (pos.x < screenLeft) pos.x = screenRight;
        else if (pos.x > screenRight) pos.x = screenLeft;

        if (pos.y < screenBottom) pos.y = screenTop;
        else if (pos.y > screenTop) pos.y = screenBottom;

        transform.position = pos;
    }

  

}