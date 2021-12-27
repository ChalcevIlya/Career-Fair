using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroungmusic : MonoBehaviour
{
    private static backgroungmusic Backgroungmusic;

    private void Awake()
    {
        if(Backgroungmusic == null)
        {
            Backgroungmusic = this;
            DontDestroyOnLoad(Backgroungmusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
