using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VariableOperation
{
    set,
    add,
    sub,
    mul,
    div,
    mod,
}


public static class VariableOperationExtension
{
    public static int operation(this VariableOperation variableOperation, int a, int b)
    {
        switch (variableOperation)
        {
            case VariableOperation.set:
                return b;
            case VariableOperation.add:
                return a + b;
            case VariableOperation.sub:
                return a - b;
            case VariableOperation.mul:
                return a * b;
            case VariableOperation.div:
                return a / b;
            case VariableOperation.mod:
                return a % b;
        }
        return 0;
    }
}