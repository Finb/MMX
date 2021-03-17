using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MMX;
using System;
public enum ItemsListType
{
    humanItems = 0,
    recoverItems,
    fightItem,
    equipment,
}
public class ItemsListViewController : BaseUIInputController, IListViewDataSource, IListViewDelegate
{

    public ItemsListType type = ItemsListType.humanItems;
    // Start is called before the first frame update
    public ListView listView;
    public UnityEngine.UI.Text pageText;
    public UnityEngine.UI.Image titlePanelImage;
    public UnityEngine.UI.Text titleText;
    public UnityEngine.UI.Text ItemNameText;
    public UnityEngine.UI.Text ItemDescriptionText;

    protected List<MMX.Item> items;

    //点击item时，执行的事件，默认是弹出使用道具对话框
    public Action<MMX.Item> clickAction;

    public override void Awake()
    {
        base.Awake();

        inputs.UI.A.performed += ctx =>
        {
            if (clickAction != null)
            {
                var index = currentSelectedGameObject.GetComponent<ButtonController>()?.viewTag;
                if (index != null)
                {
                    clickAction(items[index.Value]);
                }
                hide();
            }
        };
        inputs.UI.B.performed += ctx => hide();
        inputs.UI.Left.performed += ctx =>
        {
            listView.currentPage -= 1;
            refreshView();
            EventSystem.current.SetSelectedGameObject(gameObject.GetComponentInChildren<UnityEngine.UI.Button>().gameObject);
        };
        inputs.UI.Right.performed += ctx =>
        {
            listView.currentPage += 1;
            refreshView();
            EventSystem.current.SetSelectedGameObject(gameObject.GetComponentInChildren<UnityEngine.UI.Button>().gameObject);
        };

        listView.pageCount = 10;
        listView.dataSource = this;
        listView.listViewDelegate = this;
    }
    private void Start()
    {
        var colors = new[]{
            new UnityEngine.Color(0f/255, 49f/255, 97f/255, 230f/255),
            new UnityEngine.Color(0f/255, 97f/255, 7f/255, 230f/255),
            new UnityEngine.Color(97f/255, 19f/255, 0f/255, 230f/255)
            };
        var titles = new[]{
            "人类用道具",
            "回复用道具",
            "战斗用道具"};

        titleText.text = titles[(int)type];
        titlePanelImage.color = colors[(int)type];
    }
    private void refreshView()
    {
        pageText.text = (listView.currentPage + 1) + "/" + listView.totalPage;
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindComponentByObjectName<RectTransform>("ListView"));
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindComponentByObjectName<RectTransform>("BorderImage"));
    }

    public int numberOfNodes()
    {
        return items.Count;

    }
    public GameObject nodeForIndex(ListView listView, int index)
    {
        var button = Instantiate(Resources.Load<GameObject>("UI/prefabs/IconButton2"), Vector3.zero, Quaternion.identity, listView.gameObject.transform);
        button.GetComponentInChildren<UnityEngine.UI.Text>().text = this.items[index].name;
        button.GetComponent<UnityEngine.UI.LayoutElement>().preferredWidth = 200;
        button.GetComponent<UnityEngine.UI.LayoutElement>().preferredHeight = 30;
        button.FindComponentByObjectName<UnityEngine.UI.Text>("RightText").text = "" + (this.items[index] as MMX.HumanItem).count;
        button.FindComponentByObjectName<UnityEngine.UI.Image>("Image").sprite = Resources.Load<UnityEngine.Sprite>("Icon/Item");
        button.GetComponent<ButtonController>().viewTag = index;
        return button;
    }
    public void didSelectedNode(int index)
    {

    }
    public virtual void didScrollToNode(GameObject node, int index)
    {
        this.ItemNameText.text = this.items[index].name;
        this.ItemDescriptionText.text = this.items[index].desc;
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("PanelBorderImage").GetComponent<RectTransform>());
    }
    public static ItemsListViewController Create()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/ItemsListView"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        return obj.GetComponent<ItemsListViewController>();
    }
    public void show()
    {
        MMX.GameManager.Input.pushTarget(gameObject);
        switch (type)
        {
            case ItemsListType.humanItems: items = new List<MMX.Item>(ItemPack.shared.toolItems); break;
            case ItemsListType.recoverItems: items = new List<MMX.Item>(ItemPack.shared.medicineItems); break;
            case ItemsListType.fightItem: items = new List<MMX.Item>(ItemPack.shared.battleItems); break;
            case ItemsListType.equipment: items = new List<MMX.Item>(ItemPack.shared.equipmentItems); break;
        }
        listView.reloadData();
        refreshView();
        EventSystem.current.SetSelectedGameObject(gameObject.GetComponentInChildren<UnityEngine.UI.Button>().gameObject);
    }
    public void hide()
    {
        MMX.GameManager.Input.popTarget();
        Destroy(gameObject);
    }
}

