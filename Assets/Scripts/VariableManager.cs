using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpecialVariableKey
{
    level,
    hp
}

public static class SpecialVariableKeyEntension
{
    public static int value(this SpecialVariableKey key)
    {
        switch (key)
        {
            case SpecialVariableKey.level:
                return TeamQueue.shared.captain.GetComponent<HumanInfo>().level;
            case SpecialVariableKey.hp:
                return TeamQueue.shared.captain.GetComponent<HumanInfo>().property.hp;
        }
        return -1;
    }
}

public class VariableManager
{
    Dictionary<string, int> values = new Dictionary<string, int>();

    private VariableManager()
    {

    }
    private static readonly Lazy<VariableManager> Instancelock = new Lazy<VariableManager>(() => new VariableManager());
    public static VariableManager shared
    {
        get
        {
            return Instancelock.Value;
        }
    }
    private SpecialVariableKey? getSpecialVariableKey(string key)
    {
        if (key.StartsWith("special."))
        {

        }
        return null;
    }
    public int this[string key]
    {
        get
        {
            if (key == null || key.Length <= 0)
            {
                return -1;
            }
            if (getSpecialVariableKey(key) is var specialKey && specialKey != null)
            {
                //特殊变量，走特殊变量逻辑
                return this[specialKey];
            }

            if (values.ContainsKey(key))
            {
                return values[key];
            }
            return 0;
        }
        set
        {
            if (key == null || key.Length <= 0)
            {
                return;
            }
            if (getSpecialVariableKey(key) is var specialKey && specialKey != null)
            {
                //特殊变量只读，不能赋值
                return;
            }
            values[key] = value;
        }
    }

    public int this[SpecialVariableKey key]
    {
        get
        {
            return key.value();
        }
    }
    public int this[SpecialVariableKey? key]
    {
        get
        {
            if (key != null)
            {
                return this[key!];
            }
            return -1;
        }
    }
}
