using UnityEngine;
using System.Collections;

public class Player : Agent
{
    private const float DEAD_ZONE = 0.8f;
    private bool _hasWeapon = true;
    private bool _hasFire = false;
    private float _currentCooldownFire = 0.0f;
    private float _cooldownFire = 1.0f;
    private float _distanceFire = 10.0f;

    public override void InitRole()
    {
        if (_role == role.Killer || _role == role.Sherif)
        {
            _hasWeapon = true;
        }
    }

    void Update()
    {
        if (!_isAlive) return;

        bool move = false;
        if(Input.GetAxis("Horizontal_" + _playerNumber) > DEAD_ZONE)
        {
            // va à droite
            _direction = new Vector2(1.0f, 0.0f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            move = true;
        }
        else if (Input.GetAxis("Horizontal_" + _playerNumber) < -DEAD_ZONE)
        {
            // va à gauche
            _direction = new Vector2(-1.0f, 0.0f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            move = true;
        }
        else if (Input.GetAxis("Vertical_" + _playerNumber) > DEAD_ZONE)
        {
            // va en bas
            _direction = new Vector2(0.0f, -1.0f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
            move = true;
        }
        else if (Input.GetAxis("Vertical_" + _playerNumber) < -DEAD_ZONE)
        {
            // va en haut
            _direction = new Vector2(0.0f, 1.0f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 270.0f);
            move = true;
        }

        if (move)
            transform.position += new Vector3(_direction.x * _speed * Time.deltaTime, _direction.y * _speed * Time.deltaTime);

        // pour le sherif et le killer
        if (_hasWeapon)
        {
            if(Input.GetButton("Fire_" + _playerNumber) && !_hasFire)
            {
                Debug.Log("Tire");
                // tire
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(_direction.x, _direction.y), _direction, _distanceFire);
                if (hit.collider != null)
                {
                    if(hit.collider.gameObject.GetComponent<Player>())
                    {
                        hit.collider.gameObject.GetComponent<Player>().TakeDamage(_role);
                        // ajouter le check
                    }
                    else if (hit.collider.gameObject.GetComponent<IA>())
                    {
                        // perdu
                    }
                }
            }
            else
            {
                // rechargement
                _currentCooldownFire += Time.deltaTime;
                if(_currentCooldownFire >= _cooldownFire)
                {
                    _hasFire = false;
                    _currentCooldownFire = 0.0f;
                }
            }
        }
    }

    void TakeDamage(role senderRole)
    {
        _isAlive = false;
        GetComponent<SpriteRenderer>().sprite = deadTexture;
        if(_role == role.Innocent)
        {
            if(senderRole == role.Killer)
            {
                // killer marque un point

            }
            else if (senderRole == role.Sherif)
            {
                // sherif perd
            }
        }
        else if (_role == role.Sherif)
        {
            if (senderRole == role.Killer)
            {
                // killer marque un point

            }
        }
        else if (_role == role.Killer)
        {
            if (senderRole == role.Sherif)
            {
                // sherif marque un point

            }
        }
    }
}
