using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NumpadButton : MonoBehaviour
{

    public static TMPro.TMP_InputField previous = null;
    public static bool typing = false;

    public static string value = "";
    private static bool negative = false;
    private static int dec = 0;
    private static int len = 0;

    public void addChar(string s)
    {
        if (previous != null)
        {
            typing = true;
            char c = s[0];
            if (len < 9)
            {
                len++;
                value += c;
            }
        }
    }

    public void decPressed()
    {
        if (previous != null)
        {
            typing = true;
            if (len == 0)
            {
                len = 2;
                dec = 2;
                value += "0.";
            }
            else if (len < 9 && dec == 0)
            {
                len++;
                dec = len;
                value += '.';
            }
        }
    }

    public void negPressed()
    {
        if (previous != null)
        {
            typing = true;
            if (!negative)
            {
                value = "-" + value;
                negative = true;
            }
            else
            {
                value = value.Substring(1, len + 1);
                negative = false;
            }
        }
    }

    public void backspace()
    {
        if (previous != null)
        {
            typing = true;
            if (len > 0)
            {
                if (len == dec)
                {
                    dec = 0;
                }
                len--;
                value = negative ? value.Substring(0, len + 1) : value.Substring(0, len);
            }
            else if (negative)
            {
                value = "";
                negative = false;
            }
        }
    }

    public void enter()
    {
        previous.onEndEdit.Invoke(value);
        clear();
    }

    public void clear()
    {
        previous = null;
        resetInput();
    }

    public static void resetInput()
    {
        typing = false;
        value = "";
        negative = false;
        dec = 0;
        len = 0;
    }

}
