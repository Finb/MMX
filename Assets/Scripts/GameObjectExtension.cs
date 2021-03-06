using System.Collections.Generic;
using UnityEngine;

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

    public static GameObject FindObject(this GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
    public static T FindComponentByObjectName<T>(this GameObject parent, string name) where T : Component
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                var component = t.GetComponent<T>();
                if (component != null) {
                    return component;
                }
            }
        }
        return null;
    }
}

public static class ExtendGameObject
{
    public static T GetComponentInParentIncludeInactive<T>(this GameObject gameObject)
    {
        for (var transform = gameObject.transform; transform != null; transform = transform.parent)
        {
            T t = transform.GetComponent<T>();
            if (t != null)
            {
                return t;
            }
        }
        return default(T);
    }

    public static void setButtonActive(this GameObject gameObject, bool active)
    {
        foreach (var item in gameObject.GetComponentsInChildren<ButtonController>())
        {
            item.active = active;
        }
    }
    public static bool getButtonActive(this GameObject gameObject)
    {
        foreach (var item in gameObject.GetComponentsInChildren<ButtonController>())
        {
            if (item.active)
            {
                return true;
            }
        }
        return false;
    }
    public static ButtonController getButton(this GameObject gameObject)
    {
        return gameObject.GetComponentInChildren<ButtonController>();
    }
}

public static class ExtenVector2
{
    public static bool containsPoint(this Vector2[] rect, Vector2 point)
    {
        if (rect.Length != 4)
        {
            return false;
        }
        return point.x >= rect[1].x && point.y >= rect[1].y && point.x <= rect[3].x && point.y <= rect[3].y;
    }
}