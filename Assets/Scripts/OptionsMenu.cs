using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    #region Data
    [SerializeField] private AudioMixer audioMixer;
    #endregion

    #region Interface
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    #endregion

    #region Methods
    private void Start()
    {
        
        audioMixer.SetFloat("Volume", 0);
    }
    #endregion
}
