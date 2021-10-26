using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }
    public static float buttonCD = 0.3f;

    [SerializeField] private Button defaultMainBtn;
    [SerializeField] private Button defaultMainMenuBtn;
    private Button currentBtn;
    private Button MainBtn;
    private Button MainMenuBtn;

    [Space(10)]
    [Header("========================= MAIN MENU ========================= ")]
    private Animator PauseMenuAnim;
    private List<PartyBox> partyBox = new List<PartyBox>();

    [SerializeField]private List<Transform> PartyBoxPosition;

    [Space(10)]
    [Header("========================= CHARACTER DETAILS ========================= ")]
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private GameObject characterSprite;
    [SerializeField] private TextMeshProUGUI lvlNumber;

    [SerializeField] private BarScript HPBar;
    [SerializeField] private TextMeshProUGUI currentHPNumber;
    [SerializeField] private TextMeshProUGUI maxHPNumber;
    [SerializeField] private BarScript MPBar;
    [SerializeField] private TextMeshProUGUI currentMPNumber;
    [SerializeField] private TextMeshProUGUI maxMPNumber;
    [SerializeField] private BarScript overDriveBar;

    [SerializeField] private TextMeshProUGUI atkNumber;
    [SerializeField] private TextMeshProUGUI magNumber;
    [SerializeField] private TextMeshProUGUI defNumber;
    [SerializeField] private TextMeshProUGUI resNumber;
    [SerializeField] private TextMeshProUGUI spdNumber;
                                               
    [SerializeField] private TextMeshProUGUI strNumber;
    [SerializeField] private TextMeshProUGUI intNumber;
    [SerializeField] private TextMeshProUGUI focNumber;
    [SerializeField] private TextMeshProUGUI vitNumber;
    [SerializeField] private TextMeshProUGUI agiNumber;
    [SerializeField] private TextMeshProUGUI lucNumber;

    [SerializeField] private UI_StatsChart StatsChart;

    [Space(10)]
    [Header("========================= EQUIPMENT DETAILS ========================= ")]

    [SerializeField] private TextMeshProUGUI equippedWeapon;
    [SerializeField] private TextMeshProUGUI equippedHeadgear;
    [SerializeField] private TextMeshProUGUI equippedArmor;
    [SerializeField] private TextMeshProUGUI equippedBoots;
    [SerializeField] private TextMeshProUGUI equippedAccessory;

    [SerializeField] private UI_EquipmentDetails WeaponEquipmentBox;
    [SerializeField] private UI_EquipmentDetails HeadgearEquipmentBox;
    [SerializeField] private UI_EquipmentDetails ArmorEquipmentBox;
    [SerializeField] private UI_EquipmentDetails BootsEquipmentBox;
    [SerializeField] private UI_EquipmentDetails AccessoryEquipmentBox;

    [Space(10)]
    [Header("========================= INVENTORY ========================= ")]
    [SerializeField] private GameObject InventoryPanel;

    private int characterIndex;
    public int CharacterIndex { get => characterIndex; set => characterIndex = value; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else {
            Destroy(this);
        }

        this.gameObject.SetActive(true);
        for (int i = 0; i < GameManager.Instance.activeCharacter.Length; i++)
        {
            GameObject partyBoxObject = PrefabUtility.InstantiatePrefab(GameManager.Instance.activeCharacter[i].GetComponent<Player>().characterBoxPrefab, PartyBoxPosition[i]) as GameObject;
            partyBoxObject.GetComponent<PartyBox>().HPBar.SetMaxValue(GameManager.Instance.activeCharacter[i].GetComponent<Player>().CurrentHp, GameManager.Instance.activeCharacter[i].GetComponent<Player>().MaxHp);
            partyBoxObject.GetComponent<PartyBox>().currentHPNumber.text = GameManager.Instance.activeCharacter[i].GetComponent<Player>().CurrentHp.ToString();
            partyBoxObject.GetComponent<PartyBox>().maxHPNumber.text = GameManager.Instance.activeCharacter[i].GetComponent<Player>().MaxHp.ToString();

            partyBoxObject.GetComponent<PartyBox>().MPBar.SetMaxValue(GameManager.Instance.activeCharacter[i].GetComponent<Player>().CurrentMp, GameManager.Instance.activeCharacter[i].GetComponent<Player>().MaxMp);
            partyBoxObject.GetComponent<PartyBox>().currentMPNumber.text = GameManager.Instance.activeCharacter[i].GetComponent<Player>().CurrentMp.ToString();
            partyBoxObject.GetComponent<PartyBox>().maxMPNumber.text = GameManager.Instance.activeCharacter[i].GetComponent<Player>().MaxMp.ToString();
            partyBox.Add(partyBoxObject.GetComponent<PartyBox>());      
        }
        PauseMenuAnim = GetComponent<Animator>();
        this.gameObject.SetActive(false);
        Debug.Log("asw");
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(defaultMainBtn.gameObject,null);
        currentBtn = defaultMainBtn;
        for (int i = 0; i < GameManager.Instance.activeCharacter.Length; i++)
        {
            partyBox[i].HPBar.SetMaxValue(GameManager.Instance.activeCharacter[i].GetComponent<Player>().CurrentHp, GameManager.Instance.activeCharacter[i].GetComponent<Player>().MaxHp);
            partyBox[i].currentHPNumber.text = GameManager.Instance.activeCharacter[i].GetComponent<Player>().CurrentHp.ToString();
            partyBox[i].maxHPNumber.text = GameManager.Instance.activeCharacter[i].GetComponent<Player>().MaxHp.ToString();

            partyBox[i].MPBar.SetMaxValue(GameManager.Instance.activeCharacter[i].GetComponent<Player>().CurrentMp, GameManager.Instance.activeCharacter[i].GetComponent<Player>().MaxMp);
            partyBox[i].currentMPNumber.text = GameManager.Instance.activeCharacter[i].GetComponent<Player>().CurrentMp.ToString();
            partyBox[i].maxMPNumber.text = GameManager.Instance.activeCharacter[i].GetComponent<Player>().MaxMp.ToString();
        }
    }

    public void OpenMainMenu() {
        GameManager.Instance.SetState(new MainMenuState(GameManager.Instance, Instance), true);
        EventSystem.current.SetSelectedGameObject(defaultMainMenuBtn.gameObject,null);
        MainBtn = currentBtn;
        currentBtn = defaultMainMenuBtn;
    }

    public void CloseMainMenu() {
        GameManager.Instance.SetToPreviousState();
        EventSystem.current.SetSelectedGameObject(MainBtn.gameObject,null);
        currentBtn = MainBtn;
    }

    public void SelectCharacter() {
        characterIndex = 0;
        GameManager.Instance.SetState(new UIMenuCharacterSelect(GameManager.Instance, Instance), true);
    }

    public void PartyBoxSelected() {
        //partyBox[characterIndex].GetComponent<Animator>().SetBool("isSelected", true);
    }

    public void PartyBoxDeselected() {
        //partyBox[characterIndex].GetComponent<Animator>().SetBool("isSelected", false);
    }

    public void UpdateCharacterDetails(Player character)
    {
        characterNameText.text = character.characterName;

        lvlNumber.text = character.LVL.ToString();

        HPBar.SetMaxValue(character.CurrentHp, character.MaxHp);
        currentHPNumber.text = character.CurrentHp.ToString();
        maxHPNumber.text = character.MaxHp.ToString();

        MPBar.SetMaxValue(character.CurrentMp, character.MaxMp);
        currentMPNumber.text = character.CurrentMp.ToString();
        maxMPNumber.text = character.MaxMp.ToString();

        atkNumber.text = character.ATK.ToString();
        magNumber.text = character.MAG.ToString();
        defNumber.text = character.DEF.ToString();
        resNumber.text = character.RES.ToString();
        spdNumber.text = character.SPD.ToString();
           
        strNumber.text = character.STR.ToString();
        intNumber.text = character.INT.ToString();
        focNumber.text = character.FOC.ToString();
        vitNumber.text = character.VIT.ToString();
        agiNumber.text = character.AGI.ToString();
        lucNumber.text = character.LUC.ToString();

        StartCoroutine(StatsChart.UpdateChart(character.ATK, character.MAG, character.DEF, character.RES, character.SPD));
    }

    public void OpenCharacterDetails(int index) {
        PauseMenuAnim.SetTrigger("isCharacterDetails");
    }

    public void ChangeCharacterDetails(int index) {
       UpdateCharacterDetails(GameManager.Instance.activeCharacter[index].GetComponent<Player>());    
    }

    public void OpenEquipmentDetailsPanel() {
        PauseMenuAnim.SetTrigger("isEquipment");

        var player = GameManager.Instance.activeCharacter[characterIndex].GetComponent<Player>();

        equippedWeapon.text = player.equipment[(int)EquipmentType.WEAPON].equipmentName;
        equippedHeadgear.text = player.equipment[(int)EquipmentType.HEADGER].equipmentName;
        equippedArmor.text = player.equipment[(int)EquipmentType.ARMOR].equipmentName;
        equippedBoots.text = player.equipment[(int)EquipmentType.BOOTS].equipmentName;
        equippedAccessory.text = player.equipment[(int)EquipmentType.ACCESSORY].equipmentName;

        WeaponEquipmentBox.UpdateVisual(player.equipment[(int)EquipmentType.WEAPON]);
        HeadgearEquipmentBox.UpdateVisual(player.equipment[(int)EquipmentType.HEADGER]);
        ArmorEquipmentBox.UpdateVisual(player.equipment[(int)EquipmentType.ARMOR]);
        BootsEquipmentBox.UpdateVisual(player.equipment[(int)EquipmentType.BOOTS]);
        AccessoryEquipmentBox.UpdateVisual(player.equipment[(int)EquipmentType.ACCESSORY]);

    }

    public void OpenInventory() { 
    
    }

    public void SelectRight() {
        EventSystem.current.SetSelectedGameObject(currentBtn.navigation.selectOnRight.gameObject, null);
        currentBtn = currentBtn.navigation.selectOnRight.GetComponent<Button>();
        currentBtn.GetComponent<Animator>().SetTrigger("Selected");
    }

    public void SelectLeft()
    {
        EventSystem.current.SetSelectedGameObject(currentBtn.navigation.selectOnLeft.gameObject, null);
        currentBtn = currentBtn.navigation.selectOnLeft.GetComponent<Button>();
        currentBtn.GetComponent<Animator>().SetTrigger("Selected");
    }

    public void SelectDown()
    {
        EventSystem.current.SetSelectedGameObject(currentBtn.navigation.selectOnDown.gameObject, null);
        currentBtn = currentBtn.navigation.selectOnDown.GetComponent<Button>();
        currentBtn.GetComponent<Animator>().SetTrigger("Selected");
    }

    public void SelectUp() {
        EventSystem.current.SetSelectedGameObject(currentBtn.navigation.selectOnUp.gameObject, null);
        currentBtn = currentBtn.navigation.selectOnUp.GetComponent<Button>();
        currentBtn.GetComponent<Animator>().SetTrigger("Selected");
    }

}
