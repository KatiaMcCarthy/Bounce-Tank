using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] private Text healthPercentText;
    [SerializeField] private Image healthBar;

    // Start is called before the first frame update
    void Awake()
    {
        this.GetComponent<PropertyHealth>().OnUpdateUI += UpdateUI;
    }

    private void UpdateUI(float ammount)
    {
        if(ammount <= 0)
        {
            ammount = 0;
        }

        healthPercentText.text = ammount + "%";
        healthBar.fillAmount = ammount/100;
    }

    private void OnDestroy()
    {
        this.GetComponent<PropertyHealth>().OnUpdateUI += UpdateUI;
    }
  
}
