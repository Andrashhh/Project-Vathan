using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hud : MonoBehaviour
{
    UIDocument m_Hud;
    Health m_Health;

    ProgressBar m_HealthBar, m_StaminaBar;

    void Awake() {
        m_Hud = FindObjectOfType<UIDocument>();
        m_Health = GetComponent<Health>();

        m_HealthBar = m_Hud.rootVisualElement.Q("HealthBar") as ProgressBar;

    }

    void Update() {
        m_Health.OnHealthChange += UpdateHealthBar;
    }

    void UpdateHealthBar(float a) {
        m_HealthBar.value = a;
    }
}
