using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerStat : MonoBehaviour
{
    static bool multiMode = false;

    static public void enableMultiMode()
    {
        multiMode = true;
    }

    static public void disableMultiMode()
    {
        multiMode = false;
    }

    static public bool isMultiMode()
    {
        return multiMode;
    }
}
