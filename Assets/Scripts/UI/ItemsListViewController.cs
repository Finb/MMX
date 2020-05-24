using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemsListViewController : BaseUIInputController, IListViewDataSource
{
    // Start is called before the first frame update
    public ListView listView;
    public override void Awake()
    {
        base.Awake();

        inputs.UI.A.performed += ctx =>
        {

        };
        inputs.UI.B.performed += ctx => hide();
        inputs.UI.Left.performed += ctx =>
        {
            listView.currentPage -= 1;
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("ListView", true).GetComponent<RectTransform>());
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("BorderImage", true).GetComponent<RectTransform>());
            WaifForEndOfFrameAction(() =>
            {
                EventSystem.current.SetSelectedGameObject(gameObject.GetComponentInChildren<UnityEngine.UI.Button>().gameObject);
            });
        };
        inputs.UI.Right.performed += ctx =>
        {
            listView.currentPage += 1;
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("ListView", true).GetComponent<RectTransform>());
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("BorderImage", true).GetComponent<RectTransform>());
            WaifForEndOfFrameAction(() =>
            {
                EventSystem.current.SetSelectedGameObject(gameObject.GetComponentInChildren<UnityEngine.UI.Button>().gameObject);
            });
        };
        listView.pageCount = 10;
        listView.dataSource = this;
        listView.reloadData();
    }
    public int numberOfNodes()
    {
        return 96;
    }
    public GameObject nodeForIndex(ListView listView, int index)
    {
        var button = Resources.Load<GameObject>("UI/prefabs/IconButton");
        button.GetComponentInChildren<UnityEngine.UI.Text>().text = "test " + index;
        button.GetComponent<UnityEngine.UI.LayoutElement>().preferredWidth = 200;
        button.GetComponent<UnityEngine.UI.LayoutElement>().preferredHeight = 25;
        return Instantiate(button, Vector3.zero, Quaternion.identity, listView.gameObject.transform);
    }

    public static ItemsListViewController Create()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/ItemsListView"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        return obj.GetComponent<ItemsListViewController>();
    }
    public void show()
    {
        MMX.GameManager.Input.pushTarget(gameObject);
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("ListView", true).GetComponent<RectTransform>());
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("BorderImage", true).GetComponent<RectTransform>());
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("Canvas", true).GetComponent<RectTransform>());
        WaifForEndOfFrameAction(() =>
        {
            EventSystem.current.SetSelectedGameObject(gameObject.GetComponentInChildren<UnityEngine.UI.Button>().gameObject);
        });
        // EventSystem.current.SetSelectedGameObject(gameObject.GetComponentInChildren<UnityEngine.UI.Button>().gameObject);
    }
    public void hide()
    {
        MMX.GameManager.Input.popTarget();
        Destroy(gameObject);
    }
}
