using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MMX;

public class HumanStatusController : BaseUIInputController, InputButtonEventInterface
{
    [System.Serializable]
    public class HumanStatusControllerLeftPanelFields
    {
        public GameObject IntroducePanel;
        public UnityEngine.UI.Text ItemNameText;
        public UnityEngine.UI.Text ItemDescriptionText;

        public UnityEngine.UI.Text DamageText;
        public UnityEngine.UI.Text RangeText;
        public UnityEngine.UI.Text PropertyText;

        public UnityEngine.UI.Text ArmorDamageText;
        public UnityEngine.UI.Text DefenseText;
        public UnityEngine.UI.Text VelocityText;
        public UnityEngine.UI.Text MachoText;
        public UnityEngine.UI.Text[] propertyTexts;

        public GameObject WeaponPanel;
        public GameObject ArmorPanel;
        public GameObject DetailPanel;
    }
    public HumanStatusControllerLeftPanelFields leftPanelFields;

    //昵称
    public UnityEngine.UI.Text nickText;
    //HP
    public UnityEngine.UI.Text hpText;
    //经验
    public UnityEngine.UI.Text expText;
    //武器数组
    public UnityEngine.UI.Text[] weapons;
    //防具数组
    public UnityEngine.UI.Text[] armors;

    public GameObject equipmentPanel;
    public GameObject skillsPanel;
    public GameObject selectButtonPanel;

    public HumanInfo currentHuman
    {
        get
        {
            return TeamQueue.shared.humans[currentHumanIndex].GetComponent<HumanInfo>();
        }
    }
    public int currentHumanIndex = 0;

    public override void Awake()
    {
        base.Awake();
        inputs.UI.B.performed += ctx => hide();
        inputs.UI.A.performed += ctx => buttonClick();
        inputs.UI.L.performed += ctx =>
        {
            currentHumanIndex -= 1;
            if (currentHumanIndex < 0)
            {
                currentHumanIndex = TeamQueue.shared.humans.Count - 1;
            }
            refreshEquipment();
            selectedButtonChanged();
        };
        inputs.UI.R.performed += ctx =>
        {
            currentHumanIndex += 1;
            if (currentHumanIndex > TeamQueue.shared.humans.Count - 1)
            {
                currentHumanIndex = 0;
            }
            refreshEquipment();
            selectedButtonChanged();
        };

        //装备按钮
        gameObject.FindComponentByObjectName<ButtonController>("EquipmentSelectedButton").clickEvent = () =>
        {
            equipmentPanel.SetActive(true);
            skillsPanel.SetActive(false);
            layoutMiddlePanel();
            gameObject.FindComponentByObjectName<UnityEngine.UI.Text>("SelectedButtonText").text = "装备";
        };
        //技能按钮
        gameObject.FindComponentByObjectName<ButtonController>("SkillsSelectedButton").clickEvent = () =>
        {

            equipmentPanel.SetActive(false);
            skillsPanel.SetActive(true);
            layoutMiddlePanel();
            gameObject.FindComponentByObjectName<UnityEngine.UI.Text>("SelectedButtonText").text = "技能";
        };
    }
    public static HumanStatusController Create()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/HumanStatusView"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        return obj.GetComponent<HumanStatusController>();
    }
    public void show()
    {
        MMX.GameManager.Input.pushTarget(gameObject);

        equipmentPanel.setButtonActive(false);
        skillsPanel.setButtonActive(false);
        refreshEquipment();
    }
    public void hide()
    {
        if (selectButtonPanel.getButtonActive())
        {
            MMX.GameManager.Input.popTarget();
            Destroy(gameObject);
        }
        else
        {
            enterSelecteButtonPanel();
        }

    }


    private void layoutMiddlePanel()
    {
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(equipmentPanel.GetComponent<RectTransform>());
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(skillsPanel.GetComponent<RectTransform>());
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindComponentByObjectName<RectTransform>("MiddlePanel"));
    }


    public void selectedButtonChanged()
    {
        if (selectButtonPanel.getButtonActive())
        {
            currentSelectedGameObject.GetComponent<ButtonController>().clickEvent();
        }
        else if (equipmentPanel.getButtonActive())
        {

            var index = currentSelectedGameObject.GetComponent<ButtonController>().siblingButtonIndex;

            MMX.EquipmentItem item = null;
            if (index <= 2)
            {
                item = currentHuman.equipments.weapons[index];
            }
            else
            {
                if (currentHuman.equipments.armors.ContainsKey((MMX.HumanArmorEquipmentType)(index - 3)))
                {
                    item = currentHuman.equipments.armors[(MMX.HumanArmorEquipmentType)(index - 3)];
                }
            }
            if (item == null)
            {
                leftPanelFields.IntroducePanel.GetComponent<UnityEngine.CanvasGroup>().alpha = 0;
                return;
            }
            else
            {
                this.leftPanelFields.ItemNameText.text = item.name;
                if (item is MMX.HumanWeaponEquipment)
                {
                    leftPanelFields.WeaponPanel.SetActive(true);
                    leftPanelFields.ArmorPanel.SetActive(false);
                    this.leftPanelFields.ItemDescriptionText.text = "武器";
                    var weaponItem = item as MMX.HumanWeaponEquipment;
                    this.leftPanelFields.DamageText.text = "攻击力: " + weaponItem.damage;
                    this.leftPanelFields.RangeText.text = "范围: " + weaponItem.attackRangeType.getName();
                    this.leftPanelFields.PropertyText.text = "属性: " + weaponItem.attackProperty.getName();
                    UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(leftPanelFields.IntroducePanel.GetComponent<RectTransform>());
                }
                else
                {
                    leftPanelFields.WeaponPanel.SetActive(false);
                    leftPanelFields.ArmorPanel.SetActive(true);

                    var armorItem = item as MMX.HumanArmorEquipment;
                    this.leftPanelFields.ItemDescriptionText.text = armorItem.type.getName();
                    this.leftPanelFields.ArmorDamageText.text = "攻击力: " + armorItem.damage;
                    this.leftPanelFields.DefenseText.text = "防御力: " + armorItem.defense;
                    this.leftPanelFields.VelocityText.text = "速度值: " + armorItem.velocity;
                    this.leftPanelFields.MachoText.text = "男人味: " + armorItem.macho;

                    leftPanelFields.propertyTexts[0].text = "" + armorItem.fireResistance;
                    leftPanelFields.propertyTexts[1].text = "" + armorItem.iceResistance;
                    leftPanelFields.propertyTexts[2].text = "" + armorItem.electricResistance;
                    leftPanelFields.propertyTexts[3].text = "" + armorItem.sonicResistance;
                    leftPanelFields.propertyTexts[4].text = "" + armorItem.gasResistance;
                    leftPanelFields.propertyTexts[5].text = "" + armorItem.laserResistance;
                    UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(leftPanelFields.IntroducePanel.GetComponent<RectTransform>());
                }
                leftPanelFields.IntroducePanel.GetComponent<UnityEngine.CanvasGroup>().alpha = 1;
            }


        }

    }
    private void buttonClick()
    {
        if (selectButtonPanel.getButtonActive())
        {
            if (currentSelectedGameObject.name == "EquipmentSelectedButton")
            {
                //装备按钮
                enterEquipmentPanel();
            }
            else if (currentSelectedGameObject.name == "SkillsSelectedButton")
            {
                //技能按钮
                enterSkillsPanel();
            }
        }
        else if (equipmentPanel.getButtonActive())
        {
            var index = currentSelectedGameObject.getButton().siblingButtonIndex;

            var controller = EquipmentListViewController.Create();
            controller.type = ItemsListType.equipment;
            controller.clickAction = (MMX.Item item) =>
            {
                Debug.Log(index);
                setEquipment(item as MMX.EquipmentItem, index);
            };
            controller.show();
        }

    }

    private void setEquipment(MMX.EquipmentItem item, int index)
    {
        if (index <= 2)
        {
            currentHuman.equipments.weapons[index] = item as MMX.HumanWeaponEquipment;
        }
        else
        {
            currentHuman.equipments.armors[(MMX.HumanArmorEquipmentType)(index - 3)] = item as MMX.HumanArmorEquipment;
        }
        refreshEquipment();
    }
    private void refreshEquipment()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (currentHuman.equipments.weapons[i] != null)
            {
                weapons[i].text = currentHuman.equipments.weapons[i].name;
            }
            else
            {
                weapons[i].text = "无装备";
            }
        }
        for (int i = 0; i < armors.Length; i++)
        {
            if (currentHuman.equipments.armors.ContainsKey((MMX.HumanArmorEquipmentType)i))
            {
                armors[i].text = currentHuman.equipments.armors[(MMX.HumanArmorEquipmentType)i].name;
            }
            else
            {
                armors[i].text = "无装备";
            }
        }
    }

    //进入装备
    private void enterEquipmentPanel()
    {
        equipmentPanel.setButtonActive(true);
        skillsPanel.setButtonActive(false);
        selectButtonPanel.setButtonActive(false);
        EventSystem.current.SetSelectedGameObject(equipmentPanel.GetComponentInChildren<ButtonController>()?.gameObject);
    }
    //进入技能
    private void enterSkillsPanel()
    {
        equipmentPanel.setButtonActive(false);
        skillsPanel.setButtonActive(true);
        selectButtonPanel.setButtonActive(false);
        leftPanelFields.IntroducePanel.GetComponent<UnityEngine.CanvasGroup>().alpha = 0;
        EventSystem.current.SetSelectedGameObject(skillsPanel.GetComponentInChildren<ButtonController>()?.gameObject);
    }
    //进入到选择按钮
    private void enterSelecteButtonPanel()
    {
        equipmentPanel.setButtonActive(false);
        skillsPanel.setButtonActive(false);
        selectButtonPanel.setButtonActive(true);
        leftPanelFields.IntroducePanel.GetComponent<UnityEngine.CanvasGroup>().alpha = 0;
        EventSystem.current.SetSelectedGameObject(selectButtonPanel.GetComponentInChildren<ButtonController>()?.gameObject);
    }
}
