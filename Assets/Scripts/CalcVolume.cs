using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CalcVolume : MonoBehaviour
{
    [SerializeField] private InputField OutputField;
    [SerializeField] private Dropdown SelectedObject;
    [SerializeField] private Button DrownButton;
    
    private int SpawnedObjectNum = 0;
    private bool IsDrowned = false;

    // Start is called before the first frame update
    void Start()
    {
        OutputField.text = "144.264";
        //DrownButton.onClick.AddListener(CalcVolumeButtonClick);
        //SelectedObject.onValueChanged.AddListener(CalcVolumeNewObject);
    }

    void CalcVolumeButtonClick()
    {
        float volume = 0f;
        IsDrowned = !IsDrowned;

        if(!IsDrowned)
        {
            volume = 0.0461f * 0.0461f * 0.075f * (Mathf.Round(Mathf.PI * 100f) / 100f) * 1000f;
            volume = Mathf.Round(volume * 1000f) / 1000f;
            OutputField.text = volume.ToString();
            return;
        }

        switch (SpawnedObjectNum)
        {
            case 1:
                {
                    volume = ((0.0461f * 0.0461f * 0.075f * (Mathf.Round(Mathf.PI * 100f) / 100f)) +
                        (Mathf.PI * 0.02f * 0.02f * ((4f / 3f) * 0.025f + 0.06f))) * 1000f;
                    volume = Mathf.Round(volume * 1000f) / 1000f;
                    OutputField.text = volume.ToString();
                    break;
                }

            case 2:
                {
                    volume = ((0.0461f * 0.0461f * 0.075f * (Mathf.Round(Mathf.PI * 100f) / 100f)) +
                        (0.004f * 0.012f * 0.003f * 2f) +
                        (0.004f * 0.009f * 0.003f * 10f) +
                        (0.05f * 0.03f * 0.003f) +
                        (0.04f * 0.12f * 0.02f)) * 1000f;
                    volume = Mathf.Round(volume * 1000f) / 1000f;
                    OutputField.text = volume.ToString();
                    break;
                }

            case 3:
                {
                    volume = ((0.0461f * 0.0461f * 0.075f * (Mathf.Round(Mathf.PI * 100f) / 100f)) +
                        (0.03f * 0.03f * 0.03f * 6f / 3f) +
                        (0.03f * 0.03f * 0.03f)) * 1000f;
                    volume = Mathf.Round(volume * 1000f) / 1000f;
                    OutputField.text = volume.ToString();
                    break;
                }
            
            case 4:
                {
                    volume = 144.264f;
                    OutputField.text = volume.ToString();
                    break;
                }
        }
    }

    void CalcVolumeNewObject(int ObjectToSpawnNum)
    {
        float volume;

        SpawnedObjectNum = SelectedObject.value;

        volume = 0.0461f * 0.0461f * 0.075f * (Mathf.Round(Mathf.PI * 100f) / 100f) * 1000f;
        volume = Mathf.Round(volume * 1000f) / 1000f;
        OutputField.text = volume.ToString();

        IsDrowned = false;
    }
}
