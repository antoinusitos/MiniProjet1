using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    List<IA> mobs;

    public int nbMobs;

    public GameObject IAPrefab;
    public GameObject PlayerPrefab;

    public Vector2 levelLimit = new Vector2(8,5);

    public GameObject j0role;
    public GameObject j1role;
    public GameObject j2role;

    public GameObject j0Score;
    public GameObject j1Score;
    public GameObject j2Score;

    public int scoreJ0 = 0;
    public int scoreJ1 = 0;
    public int scoreJ2 = 0;

    public Sprite sherif;
    public Sprite killer;
    public Sprite innoncent;
    public Sprite dead;

    List<GameObject> IAs;
    List<Player> players;

    private float _timeToEnd;
    private float _timeToShow;
    public GameObject timer;
    private bool _hasShown;

    public int scoreMax = 10;

    void Start()
    {
        SpawnEveryThing();
    }

    void Update()
    {
        _timeToEnd -= Time.deltaTime;
        float toShow = Mathf.Round(_timeToEnd);
        timer.GetComponent<Text>().text = toShow.ToString();

        if(_timeToEnd <= _timeToShow && !_hasShown)
        {
            _hasShown = true;
            foreach (Player go in players)
            {
                if (go.IsAlive())
                {
                    go.ShowPlayer();
                }
            }
       }

        if(toShow == 0.0f)
        {
            foreach (Player go in players)
            {
                if(go.GetRole() == Agent.role.Innocent)
                {
                    Score(go._playerNumber, 1);
                    _timeToEnd = 60.0f;
                    break;
                }
            }
        }
    }

    public void SpawnEveryThing()
    {
        IAs = new List<GameObject>();
        players = new List<Player>();

        mobs = new List<IA>();

        _timeToEnd = 60.0f;
        _hasShown = false;
        _timeToShow = Random.Range(10.0f, 30.0f);

        for (int i = 0; i < nbMobs; i++)
        {
            GameObject gO = GameObject.Instantiate(IAPrefab);

            Vector2 randomPos = new Vector2(Random.Range(-levelLimit.x, levelLimit.x),
                                            Random.Range(-levelLimit.y, levelLimit.y));

            gO.transform.position = randomPos;

            mobs.Add(gO.GetComponent<IA>());

            IAs.Add(gO);
        }

        int[] rand = { 0, 1, 2 };

        for (int i = 0; i < 3; i++)
        {
            int random = Random.Range(0, 2);
            int temp = rand[random];
            rand[random] = rand[i];
            rand[i] = temp;
        }

        for (int i = 0; i < 3; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(-levelLimit.x, levelLimit.x),
                                            Random.Range(-levelLimit.y, levelLimit.y));
            GameObject go = (GameObject)Instantiate(PlayerPrefab, new Vector3(randomPos.x, randomPos.y), Quaternion.identity);
            Agent.role theRole = Agent.role.None;
            if (rand[i] == 0)
            {
                go.GetComponent<Player>().Init(new Vector2(-1.0f, 0.0f), 5.0f, i, Agent.role.Innocent, this);
                theRole = Agent.role.Innocent;
            }
            else if (rand[i] == 1)
            {
                go.GetComponent<Player>().Init(new Vector2(-1.0f, 0.0f), 5.0f, i, Agent.role.Killer, this);
                theRole = Agent.role.Killer;
            }
            else if (rand[i] == 2)
            {
                go.GetComponent<Player>().Init(new Vector2(-1.0f, 0.0f), 5.0f, i, Agent.role.Sherif, this);
                theRole = Agent.role.Sherif;
            }

            players.Add(go.GetComponent<Player>());

            if (i == 0)
            {
                if (theRole == Agent.role.Sherif)
                {
                    j0role.GetComponent<Image>().sprite = sherif;
                }
                else if (theRole == Agent.role.Killer)
                {
                    j0role.GetComponent<Image>().sprite = killer;
                }
                else if (theRole == Agent.role.Innocent)
                {
                    j0role.GetComponent<Image>().sprite = innoncent;
                }
            }
            else if (i == 1)
            {
                if (theRole == Agent.role.Sherif)
                {
                    j1role.GetComponent<Image>().sprite = sherif;
                }
                else if (theRole == Agent.role.Killer)
                {
                    j1role.GetComponent<Image>().sprite = killer;
                }
                else if (theRole == Agent.role.Innocent)
                {
                    j1role.GetComponent<Image>().sprite = innoncent;
                }
            }
            else if (i == 2)
            {
                if (theRole == Agent.role.Sherif)
                {
                    j2role.GetComponent<Image>().sprite = sherif;
                }
                else if (theRole == Agent.role.Killer)
                {
                    j2role.GetComponent<Image>().sprite = killer;
                }
                else if (theRole == Agent.role.Innocent)
                {
                    j2role.GetComponent<Image>().sprite = innoncent;
                }
            }
        }
    }

    public void SetDeath(int id, Agent.role theRole)
    {
        if(id == 0)
        {
            j0role.GetComponent<Image>().sprite = dead;
        }
        else if (id == 1)
        {
            j1role.GetComponent<Image>().sprite = dead;
        }
        else if (id == 2)
        {
            j2role.GetComponent<Image>().sprite = dead;
        }

        if(theRole == Agent.role.Killer)
        {
            Reset();
        }
        else
        {
            bool sherifAlive = true;
            bool InnocentAlive = true;

            foreach (Player go in players)
            {
                if (go.GetRole() == Agent.role.Innocent && !go.IsAlive())
                {
                    InnocentAlive = false;
                }
                else if (go.GetRole() == Agent.role.Sherif && !go.IsAlive())
                {
                    sherifAlive = false;
                }
            }

            if (!sherifAlive && !InnocentAlive)
            {
                Reset();
            }
        }
    }

    public void Reset()
    {
        if (scoreJ0 == scoreMax || scoreJ1 == scoreMax || scoreJ2 == scoreMax) return;

        foreach(Player go in players)
        {
            Destroy(go.gameObject);
        }
        foreach (GameObject go in IAs)
        {
            Destroy(go.gameObject);
        }

        SpawnEveryThing();
    }

    public void Score(int id, int score)
    {
        if (id == 0)
        {
            scoreJ0 += score;
            j0Score.GetComponent<Text>().text = scoreJ0.ToString();
        }
        else if (id == 1)
        {
            scoreJ1 += score;
            j1Score.GetComponent<Text>().text = scoreJ1.ToString();
        }
        else if (id == 2)
        {
            scoreJ2 += score;
            j2Score.GetComponent<Text>().text = scoreJ2.ToString();
        }
    }

}
