using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleBehaviour : MonoBehaviour
{
    Enemy GeneralScript;
    CharacterStats stat;

    // Start is called before the first frame update
    void Start()
    {
        GeneralScript = GetComponent<Enemy>();
        stat = GeneralScript.GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }



}
