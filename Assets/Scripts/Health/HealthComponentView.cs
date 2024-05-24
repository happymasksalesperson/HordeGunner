using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponentView : MonoBehaviour
{
    public HealthComponent HP;

    public Slider HPBar;

    void OnEnable()
    {
        HP = GetComponentInParent<HealthComponent>();
        HP.AnnounceMaxHealth += SetHealth;
        HP.AnnounceChangeHealth += ChangeHealth;
    }

    private void SetHealth(int input)
    {
        HPBar.maxValue = input;
        HPBar.value = input;
    }

    private void ChangeHealth(int input)
    {
        HPBar.value = input;
    }

    void OnDisable()
    {
        HP.AnnounceMaxHealth -= SetHealth;
        HP.AnnounceChangeHealth -= ChangeHealth;
    }
}
