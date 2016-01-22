using UnityEngine;
using Boomlagoon.JSON;
using System.Collections.Generic;
using System;


public static class Utilities
{

    public static System.Random R = new System.Random();


    public static T GetRandomEnum<T>(int startIndex = 0, int endIndex = 0)
    {
        Array A = Enum.GetValues(typeof(T));
        var V = (T)A.GetValue(UnityEngine.Random.Range(startIndex, A.Length - endIndex));
        return V;
    }

    public static int GetEnumLength<T>()
    {
        Array A = Enum.GetValues(typeof(T));
        return A.Length;
    }

    public static List<T> FindAndRemove<T>(this List<T> lst, Predicate<T> match)
    {
        List<T> ret = lst.FindAll(match);
        lst.RemoveAll(match);
        return ret;
    }

    public static T FindAndRemoveOne<T>(this List<T> lst, Predicate<T> match)
    {
        T ret = lst.Find(match);
        lst.Remove(ret);
        return ret;
    }

    public static float NextFloat(float minValue = 0, float maxValue = 1)
    {
        double range = maxValue - (double)minValue;
        double sample = R.NextDouble();
        double scaled = (sample * range) + minValue;

        return (float)scaled;
    }

    public static void ChangeParent(Transform child, Transform parent)
    {
        child.parent = parent;
        child.localScale = Vector3.one;
        NGUITools.MarkParentAsChanged(child.gameObject);
    }

    public static float EaseOutCubic(float start, float end, float curTime, float duration)
    {
        //curTime--;
        curTime = curTime / duration - 1;
        end -= start;
        //(t=t/d-1)

        return end * (curTime * curTime * curTime + 1) + start;
    }

    public static Vector3 EaseOutCubic(Vector3 start, Vector3 end, float curTime, float duration)
    {
        //curTime--;
        curTime = curTime / duration - 1;
        end -= start;
        //(t=t/d-1)

        return end * (curTime * curTime * curTime + 1) + start;
    }
}
