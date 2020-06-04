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
    private void Start()
    {

    }
    public override void didScrollToNode(GameObject node, int index)
    {
        this.ItemNameText.text = this.items[index].name;
        this.ItemDescriptionText.text = "武器";
        var item = this.items[index] as MMX.HumanWeaponEquipment;
        this.DamageText.text = "攻击力: " + item.damage;
        this.RangeText.text = "范围: " + item.attackRangeType.getName();
        this.PropertyText.text = "属性: " + "通常";
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("PanelBorderImage", true).GetComponent<RectTransform>());
    }
        public new static EquipmentListViewController Create()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/EquipmentListView"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        return obj.GetComponent<EquipmentListViewController>();
    }
}