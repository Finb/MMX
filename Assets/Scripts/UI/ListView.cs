using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public interface IListViewDataSource
{
    int numberOfNodes();
    GameObject nodeForIndex(ListView listView, int index);
}
public interface IListViewDelegate
{
    void didSelectedNode(int index);
    void didScrollToNode(GameObject node, int index);
}

public class ListView : MonoBehaviour, InputButtonEventInterface
{
    public int pageCount = 10;
    public int totalPage = 1;
    private int _currentPage = 0;
    public int currentPage
    {
        set
        {
            if (value == _currentPage)
            {
                return;
            }
            if (value < 0)
            {
                value = totalPage - 1;
            }
            if (value >= totalPage)
            {
                value = 0;
            }
            _currentPage = value;
            reloadData();
        }
        get
        {
            return _currentPage;
        }
    }
    public IListViewDataSource dataSource;
    public IListViewDelegate listViewDelegate;
    private List<GameObject> nodes = new List<GameObject>();
    public void reloadData()
    {
        if (dataSource == null)
        {
            return;
        }
        var count = dataSource.numberOfNodes();
        totalPage = count / pageCount + (count % pageCount > 0 ? 1 : 0);
        //清空当前的数据
        int acount = gameObject.transform.childCount;
        for (int i = 0; i < acount; i++)
        {
            DestroyImmediate(gameObject.transform.GetChild(0).gameObject);
        }
        nodes.RemoveRange(0, nodes.Count);

        for (int i = pageCount * currentPage; i < Mathf.Min(pageCount * currentPage + pageCount, count); i++)
        {
            var node = dataSource.nodeForIndex(this, i);
            nodes.Add(node);
            addItem(node);
        }

    }
    void addItem(GameObject obj)
    {
        obj.transform.SetParent(gameObject.transform);
    }

    public void selectedButtonChanged()
    {
        var index = nodes.IndexOf(EventSystem.current.currentSelectedGameObject);
        if (index >= 0){
            listViewDelegate.didScrollToNode(EventSystem.current.currentSelectedGameObject,_currentPage * pageCount + index);
        }
    }
}
