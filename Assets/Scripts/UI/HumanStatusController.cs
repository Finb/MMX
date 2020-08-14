using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HumanStatusController : BaseUIInputController
{
    [System.Serializable]
    public class HumanStatusControllerLeftPanelFields
    {
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

    

    public override void Awake()
    {
        base.Awake();
        inputs.UI.B.performed += ctx => hide();

    }
    public static HumanStatusController Create()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/HumanStatusView"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        return obj.GetComponent<HumanStatusController>();
    }
    public void show()
    {
        MMX.GameManager.Input.pushTarget(gameObject);
    }
    public void hide()
    {
        MMX.GameManager.Input.popTarget();
        Destroy(gameObject);
    }
}
