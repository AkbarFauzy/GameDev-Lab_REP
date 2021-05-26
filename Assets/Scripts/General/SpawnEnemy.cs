using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public EnemyGroup EnemyList;
    Vector3 spawnLocation;
    
    int enemySize;
    int enemyIndexBegin;
    int enemyIndexEnd;

    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = GetComponent<Transform>().position;
        StartCoroutine(spawn(spawnLocation));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawn(Vector3 spawnRadius)
    {
        //GameObject enemyInstance = Instantiate(EnemyList.enemyMember[0], spawnLocation , Quaternion.identity);
      
        yield return null;
    }

}
