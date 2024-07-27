using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hud : MonoBehaviour
{
    UIDocument m_Hud;
    Health m_Health;
    Stamina m_Stamina;

    ProgressBar m_HealthBar, m_StaminaBar;

    void Awake() {
        m_Hud = FindObjectOfType<UIDocument>();
        m_Health = GetComponent<Health>();
        m_Stamina = GetComponent<Stamina>();

        m_HealthBar = m_Hud.rootVisualElement.Q("HealthBar") as ProgressBar;
        m_StaminaBar = m_Hud.rootVisualElement.Q("StaminaBar") as ProgressBar;
    }


    void Update() {
        m_Health.OnHealthChange += UpdateHealthBar;
        m_Stamina.OnStaminaChange += UpdateStaminaBar;

    }

    void UpdateHealthBar(float a) {
        m_HealthBar.value = a;
    }

    void UpdateStaminaBar(float a) {
        m_StaminaBar.value = a;
    }
}
