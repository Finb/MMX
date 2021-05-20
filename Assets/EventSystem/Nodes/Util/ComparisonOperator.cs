using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ComparisonOperator
{
    greaterThan,
    greaterThanOrEqua,
    lessThan,
    lessThanOrEqual,
    notEqual,
    equal,
}

public static class ComparisonOperatorExtension
{
    public static bool comparison(this ComparisonOperator comparisonOperator, int a, int b)
    {
        switch (comparisonOperator)
        {
            case ComparisonOperator.equal:
                return a == b;
            case ComparisonOperator.greaterThan:
                return a > b;
            case ComparisonOperator.greaterThanOrEqua:
                return a >= b;
            case ComparisonOperator.lessThan:
                return a < b;
            case ComparisonOperator.lessThanOrEqual:
                return a <= b;
            case ComparisonOperator.notEqual:
                return a != b;
        }
        return false;
    }
}