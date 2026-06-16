using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class MainSystem
{
    public static int sheep = 200;
    public static int land = 10000;
    public static double money = 20.00;

    public static int rizz = 0;
    public static int aura = 0;
    public static int hax = 0;

    private static int kindness = 0;

    public static int day = 3;

    public static string name;

    public static int scenePos = 1;
    private static List<string> scenes = new List<string> { "Bedroom", "Classroom", "Throne", "Outside"};
    
    public static void NoonChoice(int s, int l, double m, int k)
    {
        sheep += s;
        land += l;
        money += m;
        kindness += k;
    }

    public static string GetScene()
    {
        string s = scenes[scenePos];
        scenePos += 1;
        scenePos %= 4;
        if (scenePos == 1)
        {
            day += 1;
        }
        return s;
    }
}