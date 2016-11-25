using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{
    protected Vector2 _direction;
    protected float _speed;
    protected int _playerNumber;

    public enum role
    {
        Killer,
        Sherif,
        Innocent,
        None,
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
