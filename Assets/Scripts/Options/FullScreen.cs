using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FullScreen : MonoBehaviour
{
    public Toggle toggle;
    public TMP_Dropdown dropdownResolution;
    Resolution[] resolutions;

    void Start()
    {
        toggle.isOn = SettingsManager.CargarFullscreen();
        CheckResolution();
    }

    public void ActivateFullScreeen(bool fullscreen)
    {
        SettingsManager.GuardarFullscreen(fullscreen);
    }

    public void CheckResolution()
    {
        resolutions = Screen.resolutions;
        dropdownResolution.ClearOptions();

        List<string> options = new List<string>();
        List<Resolution> filtradas = new List<Resolution>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (!options.Contains(option))
            {
                options.Add(option);
                filtradas.Add(resolutions[i]);
            }
        }

        resolutions = filtradas.ToArray();
        dropdownResolution.AddOptions(options);
        dropdownResolution.value = SettingsManager.CargarResolucion();
        dropdownResolution.RefreshShownValue();
    }

    public void ChangeResolution(int indice)
    {
        SettingsManager.GuardarResolucion(indice, resolutions);
    }
}