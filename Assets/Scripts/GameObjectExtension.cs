using System.Collections.Generic;
using UnityEngine;

namespace MMX
{
    public static class GameObjectExtension
    {
        public static bool HasComponent<T>(this GameObject obj) where T : Component
        {
            return obj.TryGetComponent(typeof(T), out _);
        }
        
        public static GameObject SetParent(this GameObject obj, GameObject newParent)
        {
            obj.transform.parent = newParent.transform;
            return obj;
        }
        
        public static GameObject SetParent(this GameObject obj, Component newParent)
        {
            obj.transform.parent = newParent.transform;
            return obj;
        }

        public static T FindObjectByTypeAndName<T>(string name) where T : Component
        {
            var objs = FindObjectsByTypeAndName<T>(name);

            if (objs.Length > 0)
            {
                return objs[0];
            }

            return null;
        }

        public static T[] FindObjectsByTypeAndName<T>(string name) where T : Component
        {
            var objs = GameObject.FindObjectsOfType<T>();
            var filteredObjs = new List<T>();

            foreach (var obj in objs)
            {
                if (obj.name == name)
                {
                    filteredObjs.Add(obj);
                }
            }

            return filteredObjs.ToArray();
        }
    }
}
