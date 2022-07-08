using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateVSBar2 : MonoBehaviour
{
    public StateController player;
    public StateController Agent;
    [SerializeField]
    private Slider VSSlider;
    [SerializeField]
    private GameObject panelWin;
    [SerializeField]
    private GameObject panelLose;


    private void Start()
    {
        VSSlider = GetComponent<Slider>();
        VSSlider.value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVSValue();

        if (VSSlider.value == 1)
        {
            panelWin.SetActive(true);
            Time.timeScale = 0;
        }
        if (VSSlider.value == 0)
        {
            panelLose.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void UpdateVSValue()
    {
        VSSlider.value = (float)player.hp / (float)(player.hp + Agent.hp);
    }
}
