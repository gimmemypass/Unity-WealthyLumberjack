using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    private static Dictionary<string, ulong> moneyUnits = new Dictionary<string, ulong>
    {
        [" "] = 1,
        ["K"] = 1000,
        ["M"] = 1000_000,
        ["B"] = 1000_000_000,
        ["T"] = 1000_000_000_000,
        ["Qd"] = 1000_000_000_000_000,
        ["Qt"] = 1000_000_000_000_000_000,
    };
    public static string PriceToString(ulong price)
    {
        if (price == 0) return "0";
        string letter = " ";
        foreach (KeyValuePair<string, ulong> kv in moneyUnits)
        {
            if (price >= kv.Value)
            {
                letter = kv.Key;
                continue;
            }
            break;
        }
        var ret = 1.0 * price / moneyUnits[letter];
        return $"{ret} {letter}";

    }

}
