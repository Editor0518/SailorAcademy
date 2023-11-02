using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableHolder : MonoBehaviour
{
    static string varSaveName = "Variable";

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetString(varSaveName, "¾øÀ½"));
    }

    public static int CompareVariableReturnBranch(string cmd)
    {
        string[] str = cmd.Split("==");
        string name = str[0];

        string[] str2 = str[1].Split("?");
        string value = str2[0];
        //vh.ch1crim==hide?24:25
        string[] str3 = str2[1].Split(":");
        Debug.Log(str3[0]);
        Debug.Log(str3[1]);
        int whenTrue = int.Parse(str3[0]);
        int whenFalse = int.Parse(str3[1]);
        if (GetVariable(name) != "") return GetVariable(name).Equals(value) ? whenTrue : whenFalse;
        else return whenTrue;
       
    }

    static string GetVariable(string name)
    {
        string s = PlayerPrefs.GetString(varSaveName, "");
        string[] str = s.Split(",");

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i].Contains(name))
            {
                string[] index = str[i].Split("_");

                return index[1];
            }

        }
        return "";
    }

    public static void SetVariable(string cmd)
    {
        string[] cmds = cmd.Split("=");
        string name = cmds[0], set = cmds[1];

        string s = PlayerPrefs.GetString(varSaveName, "");
        string[] str = s.Split(",");
        string result = "";
        if (s.Contains(name))
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].Contains(name))
                {
                    string[] index = str[i].Split("_");
                    index[1] = set;
                    str[i] = index[0] + "_" + index[1];

                }
                result += str[i];
                if (i < str.Length - 1) result += ",";
            }
        }
        else {
            if (s != "") result = s + ",";
            result+=name + "_" + set;
        }

        Debug.Log("result: " + result);
        PlayerPrefs.SetString(varSaveName, result);

    }

}
