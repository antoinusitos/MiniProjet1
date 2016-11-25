using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{
    protected Vector2 _direction;
    protected float _speed = 5.0f;
    public int _playerNumber = 0;
    protected bool _isAlive = true;

    public enum role
    {
        Killer,
        Sherif,
        Innocent,
    }

    protected role _role;

    public Sprite deadTexture;

    public void Init(Vector2 direction, float speed, int playerNumber, role theRole)
    {
        _direction = direction;
        _speed = speed;
        _playerNumber = playerNumber;
        _role = theRole;
        _isAlive = true;
    }

    public virtual void InitRole()
    {

    }
}
