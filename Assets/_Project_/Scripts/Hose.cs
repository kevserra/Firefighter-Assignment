using System;
using UnityEngine;
using UnityEngine.InputSystem;



public class Hose : MonoBehaviour
{
    public float maxPower = 20;
    public float minPower = 5;
    public float changeSpeed = 5;

    public ParticleSystem[] hoseWaterSystems;
    public Renderer systemRenderer;

    private float m_Power;

    private bool mouseDown = false;

    public void OnFire(InputAction.CallbackContext context)
    {
        mouseDown = context.performed;
    }

    private void Update()
    {     
               //one line if else statement, "?",  ":" = if, : = else
        m_Power = Mathf.Lerp(m_Power, mouseDown ? maxPower : minPower, Time.deltaTime * changeSpeed);

        foreach (var system in hoseWaterSystems)
        {
            ParticleSystem.MainModule mainModule = system.main;
            mainModule.startSpeed = m_Power;
            var emission = system.emission;
            emission.enabled = (m_Power > minPower * 1.1f);
        }

        if (mouseDown) GameManager.instance.UseWater();      
    }
}

