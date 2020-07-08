﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EconomyManager : MonoBehaviour//simple singleton here
{
    private static EconomyManager _instance;
    public static EconomyManager instance 
    {
        get 
        
        {
            if (_instance == null) Debug.LogError("EconomyManager is null");
            return _instance;
        } 
    }
    
    [SerializeField] private int goldAmount;
    [SerializeField]private Text goldText;
    [SerializeField] int autoGeneratedGold = 2;
    [SerializeField] float goldAddingInterval = 3f;


    private void Awake()
    {
        _instance = this;
        //SaveLoad.Saving += Save;
        //SaveLoad.Loading += Load;
    }
    private void Start()
    {
        
        StartCoroutine(PassiveGoldEarning());
    }
    public int CheckGoldAmount()
    {
        return goldAmount;
    }
    public void GainGold(int amount)
    {
        goldAmount += amount;
    }

    public void UseGold(int amount)
    {
        goldAmount -= amount;
    }
    public void UpdateText()
    {
        goldText.text = ": " + goldAmount.ToString();
    }

    private IEnumerator PassiveGoldEarning()
    {
        while (true)
        {
            goldAmount += autoGeneratedGold;
            UpdateText();
            yield return new WaitForSeconds(goldAddingInterval);
        }
    }
    //void Save()
    //{
    //    SaveLoad.Save(goldAmount, "gold");
    //}
    //void Load()
    //{
    //   if(SaveLoad.SaveAlreadyExists("gold"))
    //   goldAmount= SaveLoad.Load<int>("gold");
    //}
}