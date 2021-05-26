using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleQueue :  MonoBehaviour
{

    public GameObject queuePrefab;

    [SerializeField]
    public Queue<battleAddress> battleQueue;

    [SerializeField]
    public struct battleAddress
    {
        public battleAddress (int _a, int _b, ActionType _c, bool _d) {
            character = _a;
            enemy = _b;
            actionType = _c;
            isPlayer = _d; 
        }

        public int character;
        public int enemy;
        public ActionType actionType;
        public bool isPlayer;

    }
    public void Init()
    {
        battleQueue = new Queue<battleAddress>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void addActionQueue(int characterIndex, int enemyIndex, ActionType actionType, bool isPlayer)
    {
        battleQueue.Enqueue(new battleAddress(characterIndex, enemyIndex, actionType, isPlayer));
        queuePrefab.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = nameof(actionType);
        Instantiate(queuePrefab, this.transform);
    }

    public void deleteActionQueue() {
        battleQueue.Dequeue();
        Destroy(this.transform.GetChild(0).gameObject);
    }
}
