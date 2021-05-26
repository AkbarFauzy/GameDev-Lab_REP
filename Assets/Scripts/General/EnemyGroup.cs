using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Groups", menuName = "Enemy Group")]
public class EnemyGroup : ScriptableObject
{
    [SerializeField] public List<GameObject> prefab;
    [SerializeField] public List<int> number;
}
