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
}

public class ListView : MonoBehaviour
{
    public int pageCount = 10;
    public int totalPage = 1;
    private int _currentPage = 0;
    public int currentPage
    {
        set
        {
            if ( value == _currentPage )
            {
                return;
            }
            if (value < 0) {
                value = totalPage - 1;
            }
            if (value >= totalPage){
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
    public void reloadData()
    {   
        if (dataSource == null){
            return;
        }
        var count = dataSource.numberOfNodes();
        totalPage = count/ pageCount + (count % pageCount > 0 ? 1 : 0);
        //清空当前的数据
        int acount = gameObject.transform.childCount;
        for(int i = 0 ; i <  acount; i++){
            DestroyImmediate(gameObject.transform.GetChild(0).gameObject);
        }

        for(int i = pageCount * currentPage; i < Mathf.Min(pageCount * currentPage + pageCount,count); i++){
            addItem(dataSource.nodeForIndex(this,i));
        } 
        
    }
    void addItem(GameObject obj){
        obj.transform.parent = gameObject.transform;
    }

}
