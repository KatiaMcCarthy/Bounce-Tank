using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    // Start is called before the first frame update
    void Awake()
    {
        this.GetComponent<PropertyHealth>().OnUpdateUI += UpdateUI;
    }

    private void UpdateUI(float ammount)
    {
        if (ammount <= 0)
        {
            ammount = 0;
        }

      
        healthBar.value = ammount / 100;
    }

    private void OnDestroy()
    {
        this.GetComponent<PropertyHealth>().OnUpdateUI += UpdateUI;
    }

}
