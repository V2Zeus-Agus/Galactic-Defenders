using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    void Start()
    {
        SettingsManager.CargarVolumen(audioMixer);
        SettingsManager.AplicarVolumenATodos();
    }

    public void CambiarVolumen(float volumen)
    {
        SettingsManager.GuardarVolumen(volumen, audioMixer);
        SettingsManager.AplicarVolumenATodos();
    }

    public void Volver()
    {
        SceneManager.LoadScene(0);
    }
}