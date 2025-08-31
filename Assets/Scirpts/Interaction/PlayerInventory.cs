using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInventory : MonoBehaviour
{
    public int Cookies { get; private set; }
    public event Action<int> OnCookiesChanged;  //event that trigger on cookie amount change

    public void AddCookies(int amount)
    {
        Cookies += amount;
        
        //Trigger event
        OnCookiesChanged?.Invoke(Cookies);
        
        Debug.Log($"Cookies: {Cookies}");
    }
}
