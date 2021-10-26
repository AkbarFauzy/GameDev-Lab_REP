using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Equipment")]
public class Equipment : ScriptableObject
{
    public string equipmentName;
    public EquipmentType Type;
    public int ATKBonus;
    public int MAGBonus;
    public int DEFBonus;
    public int RESBonus;

    public int STR;
    public int INT;
    public int VIT;
    public int FOC;
    public int SPD;
}
