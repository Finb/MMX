using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MMX
{
    public class ItemStorage
    {
        static public ItemStorage shared = new ItemStorage();

        public Dictionary<string, Item> items = new Dictionary<string, Item>();

        private ItemStorage()
        {
            //加载人类道具json
            // var item = JsonUtility.FromJson<MMX.HumanItem>("",List<MMX.HumanItem>);

            Debug.Log(items.Count);
        }
        public void addItems<T>(string json) where T : Item
        {
            var itemArray = JsonConvert.DeserializeObject<List<T>>(json);
            foreach (var item in itemArray)
            {
                if (items.ContainsKey(item.id))
                {
                    Debug.LogError("重复的Item");
                }
                item.orderNumber = items.Count;
                items.Add(item.id, item);
            }
        }
    }
}