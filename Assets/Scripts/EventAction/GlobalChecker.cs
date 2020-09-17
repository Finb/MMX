using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalChecker
{
    public static GlobalChecker shared = new GlobalChecker();
    public Dictionary<string, int> values;
    private GlobalChecker()
    {
        values = new Dictionary<string, int>();
    }
    public int checkValue(string key)
    {
        if (values.ContainsKey(key))
        {
            return values[key];
        }
        return -1;
    }
}
