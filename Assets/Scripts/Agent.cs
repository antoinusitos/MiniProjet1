using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{
    private Vector2 _direction;
    private float _speed;
    private int _playerNumber;

    public enum role
    {
        Killer,
        Sherif,
        Innocent,
    }

    private role _role;

    public Agent(Vector2 direction, float speed, int playerNumber, role theRole)
    {
        _direction = direction;
        _speed = speed;
        _playerNumber = playerNumber;
        _role = theRole;
    }
}
