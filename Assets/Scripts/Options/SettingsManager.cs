using UnityEngine;
using UnityEngine.Audio;

public static class SettingsManager
{
    // ---- Volumen ----
    public static void GuardarVolumen(float volumen, AudioMixer mixer)
    {
        mixer.SetFloat("Volumen", volumen);
        PlayerPrefs.SetFloat("Volumen", volumen);
        PlayerPrefs.Save();
    }

    public static void CargarVolumen(AudioMixer mixer)
    {
        if (PlayerPrefs.HasKey("Volumen"))
            mixer.SetFloat("Volumen", PlayerPrefs.GetFloat("Volumen"));
    }

    // ---- Fullscreen ----
    public static void GuardarFullscreen(bool fs)
    {
        Screen.fullScreen = fs;
        PlayerPrefs.SetInt("Fullscreen", fs ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool CargarFullscreen()
    {
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            bool fs = PlayerPrefs.GetInt("Fullscreen") == 1;
            Screen.fullScreen = fs;
            return fs;
        }
        return Screen.fullScreen;
    }

    // ---- Resolución ----
    public static void GuardarResolucion(int indice, Resolution[] resolutions)
    {
        PlayerPrefs.SetInt("numberResolution", indice);
        PlayerPrefs.Save();
        Resolution res = resolutions[indice];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public static int CargarResolucion()
    {
        return PlayerPrefs.GetInt("numberResolution", 0);
    }

    // ---- Aplicar volumen a AudioSources sueltos ----
    public static void AplicarVolumenATodos()
    {
        if (PlayerPrefs.HasKey("Volumen"))
        {
            float db = PlayerPrefs.GetFloat("Volumen");
            float lineal = Mathf.Pow(10f, db / 20f);

            AudioSource[] audios = Object.FindObjectsOfType<AudioSource>();
            foreach (AudioSource audio in audios)
            {
                if (audio.outputAudioMixerGroup == null)
                    audio.volume = lineal;
            }
        }
    }
}