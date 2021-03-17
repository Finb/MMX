using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MMX;
class EquipmentListViewController : ItemsListViewController
{
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
    private void Start()
    {

    }
    public override void didScrollToNode(GameObject node, int index)
    {
        var item = this.items[index];
        this.ItemNameText.text = this.items[index].name;
        if (item is HumanWeaponEquipment)
        {
            WeaponPanel.SetActive(true);
            ArmorPanel.SetActive(false);
            this.ItemDescriptionText.text = "武器";
            var weaponItem = this.items[index] as MMX.HumanWeaponEquipment;
            this.DamageText.text = "攻击力: " + weaponItem.attack;
            this.RangeText.text = "范围: " + weaponItem.attackRangeType.getName();
            this.PropertyText.text = "属性: " + weaponItem.attackProperty.getName();
        }
        else
        {
            WeaponPanel.SetActive(false);
            ArmorPanel.SetActive(true);

            var armorItem = this.items[index] as MMX.HumanArmorEquipment;
            this.ItemDescriptionText.text = armorItem.type.getName();
            this.ArmorDamageText.text = "攻击力: " + armorItem.attack;
            this.DefenseText.text = "防御力: " + armorItem.defense;
            this.VelocityText.text = "速度值: " + armorItem.speed;
            this.MachoText.text = "男人味: " + armorItem.manliness;

            propertyTexts[0].text = "" + armorItem.fireResistance;
            propertyTexts[1].text = "" + armorItem.iceResistance;
            propertyTexts[2].text = "" + armorItem.electricResistance;
            propertyTexts[3].text = "" + armorItem.sonicResistance;
            propertyTexts[4].text = "" + armorItem.gasResistance;
            propertyTexts[5].text = "" + armorItem.laserResistance;
        }
    }
    public new static EquipmentListViewController Create()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/EquipmentListView"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        return obj.GetComponent<EquipmentListViewController>();
    }
}