using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour {

    [SerializeField] Text moneyText;
    [SerializeField] float startAmount;
    private float amount;

    // Use this for initialization
    void Start()
    {
        amount = startAmount;
        moneyText.text = "Money: " + amount.ToString();
    }

    public void AddMoney(float value)
    {
        amount += value;
        moneyText.text = "Money: " + amount.ToString();
    }

    public void PaidMoney(float value)
    {
        amount -= value;
        moneyText.text = "Money: " + amount.ToString();
    }

    public float GetMoney()
    {
        return amount;
    }
}
