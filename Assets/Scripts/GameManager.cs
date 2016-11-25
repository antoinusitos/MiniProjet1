using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    List<IA> mobs;

    public int nbMobs;

    public GameObject IAPrefab;

    public Vector2 levelLimit = new Vector2(8,5);
    
    void Start()
    {
        mobs = new List<IA>();

        for(int i = 0; i < nbMobs; i++)
        {
            GameObject gO = GameObject.Instantiate(IAPrefab);

            Vector2 randomPos = new Vector2(Random.Range(-levelLimit.x, levelLimit.x), 
                                            Random.Range(-levelLimit.y, levelLimit.y));

            gO.transform.position = randomPos;

            mobs.Add(gO.GetComponent<IA>());
        }
    }

}
