using UnityEngine;
using System.Collections;

public class IA : Agent 
{
    Vector2 _destination;

    Vector2 _limits;

    public IA(Vector2 direction, float speed, int playerNumber, role theRole) : base(direction, speed, playerNumber, theRole)
    {
        
    }

    void Awake()
    {
        _speed = 5f;

        _destination = transform.position;

        _limits = FindObjectOfType<GameManager>().levelLimit;

        InvokeRepeating("Move", 0f, .5f);
    }
    
    void Update()
    {
        float step = _speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, _destination, step);
    }

    void Move()
    {
        int moveType = Random.Range(0, 4);

        Vector2 move = new Vector2();
        float rotation = 0f;

        switch (moveType)
        {
            case 0:
                move = new Vector2(5, 0);
                rotation = 180;
                break;
            case 1:
                move = new Vector2(-5, 0);
                rotation = 0;
                break;
            case 2:
                move = new Vector2(0, 5);
                rotation = 270;
                break;
            case 3:
                move = new Vector2(0, -5);
                rotation = 90;
                break;
        }

        Vector3 newDest = transform.position + (Vector3)move;

        if (!(newDest.x > _limits.x || newDest.x < -_limits.x 
            || newDest.y < -_limits.y || newDest.y > _limits.y))
        {
            _destination = newDest;
            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }

    }


}
