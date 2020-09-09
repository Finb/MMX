using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MMX;
public class BaseScene : MonoBehaviour
{
    public GameObject cinemachine;
    // Start is called before the first frame update
    void Start()
    {

        var cinemachineConfiner = cinemachine.GetComponent<CinemachineConfiner>();
        var shape2D = (PolygonCollider2D)cinemachineConfiner.m_BoundingShape2D;
        Vector2[] arr = {
            new Vector2(14f,9f),
            new Vector2(-15f,9f),
            new Vector2(-15f,-9f),
            new Vector2(14f,-9f)
            };
        shape2D.points = arr;

        //设置边界碰撞
        var obj2 = GameObject.FindWithTag("EdgeCollider");
        var collider = obj2.GetComponent<EdgeCollider2D>();

        List<Vector2> pointsList = new List<Vector2>();
        pointsList.AddRange(shape2D.points);
        pointsList.Add(shape2D.points[0]);
        collider.points = pointsList.ToArray();

        // 加载地图
        new TeleportExecuter().teleport(new TeleportTargetInfo("拉多1", "拉多镇", new Vector2(0.5f, -2.5f), null));

        //加载UI界面
        var mainMenu = Instantiate(Resources.Load<GameObject>("UI/MainMenu"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        mainMenu.name = "MainMenu";
        //强制先算一下布局（否则Virtual layout 需要在 avtice 为true 时 才布局，那时已经晚了，导致要设置手指的位置时取到的按钮postion错误）
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(mainMenu.FindComponentByObjectName<RectTransform>("Image"));
        mainMenu.SetActive(false);

        ItemStorage.shared.addItems<HumanItem>(Resources.Load<TextAsset>("Items/HumanItem").text);
        ItemStorage.shared.addItems<RecoverItem>(Resources.Load<TextAsset>("Items/RecoverItem").text);
        ItemStorage.shared.addItems<FightItem>(Resources.Load<TextAsset>("Items/FightItem").text);
        ItemStorage.shared.addItems<HumanWeaponEquipment>(Resources.Load<TextAsset>("Items/HumanWeaponEquipment").text);
        ItemStorage.shared.addItems<HumanArmorEquipment>(Resources.Load<TextAsset>("Items/HumanArmorEquipment").text);

    }

    // Update is called once per frame
    void Update()
    {

    }
}


public static class Extension
{
    /// <summary>
    /// 获取文本的绘制长度，不同于text的rectTransform.sizeDelta
    /// </summary>
    /// <param name="str">文本</param>
    /// <returns></returns>
    public static int GetFontlen(this string str, int fontSize, string fontName = "Arial")
    {
        var font = Font.CreateDynamicFontFromOSFont(fontName, fontSize);
        int len = 0;
        font.RequestCharactersInTexture(str);
        for (int i = 0; i < str.Length; i++)
        {
            CharacterInfo ch;
            font.GetCharacterInfo(str[i], out ch);
            len += ch.advance;
        }
        return len;
    }
}