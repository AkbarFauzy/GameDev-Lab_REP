using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_EquipmentDetails : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI EquipmentNameText;
    [SerializeField] private TextMeshProUGUI ATKBonusNumber;
    [SerializeField] private TextMeshProUGUI MAGBonusNumber;
    [SerializeField] private TextMeshProUGUI DEFBonusNumber;
    [SerializeField] private TextMeshProUGUI RESBonusNumber;
    [SerializeField] private TextMeshProUGUI BonusStasText;

    public void UpdateVisual(Equipment equipment)
    {
        EquipmentNameText.text = equipment.equipmentName;
        ATKBonusNumber.text = equipment.ATKBonus.ToString();
        MAGBonusNumber.text = equipment.MAGBonus.ToString();
        DEFBonusNumber.text = equipment.DEFBonus.ToString();
        RESBonusNumber.text = equipment.RESBonus.ToString();
    }

}