using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateVSBar : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVSValue();

        if (VSSlider.value == 5 || Agent.hp <= 0)
        {
            panelWin.SetActive(true);
            Time.timeScale = 0;
        }
        if (VSSlider.value == -5 || player.hp <= 0)
        {
            panelLose.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void UpdateVSValue()
    {
        VSSlider.value = player.score - Agent.score;
    }

}
