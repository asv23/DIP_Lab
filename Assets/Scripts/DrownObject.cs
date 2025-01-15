using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrownObject : MonoBehaviour
{
    [SerializeField] private InputField OutputField;
    [SerializeField] private Dropdown SelectedObject;
    [SerializeField] private Button DrownButton;

    private ExperimentSystem ObjectToDrown;
    private int SpawnedObjectNum = 0;
    private bool IsDrowned = false;

    // Start is called before the first frame update
    void Start()
    {
        ObjectToDrown = FindObjectOfType<ExperimentSystem>();
        SelectedObject.onValueChanged.AddListener(SpawnSelectedObject);
    }

    public void SpawnSelectedObject(int ObjectToSpawnNum)
    {
        SpawnedObjectNum = ObjectToSpawnNum;

        if(IsDrowned)
        {
            DrownSpawnedObject();
        }

        if (SpawnedObjectNum == 0)
        {
            ObjectToDrown.NewExperiment.transform.GetChild(0).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(1).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(2).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(3).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(5).gameObject.SetActive(true);
            ObjectToDrown.NewExperiment.transform.GetChild(6).gameObject.SetActive(true);
        }
        else if (SpawnedObjectNum == 1)
        {
            ObjectToDrown.NewExperiment.transform.GetChild(0).gameObject.SetActive(true);
            ObjectToDrown.NewExperiment.transform.GetChild(1).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(2).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(3).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(5).gameObject.SetActive(true);
            ObjectToDrown.NewExperiment.transform.GetChild(6).gameObject.SetActive(true);
        }
        else if (SpawnedObjectNum == 2)
        {
            ObjectToDrown.NewExperiment.transform.GetChild(0).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(1).gameObject.SetActive(true);
            ObjectToDrown.NewExperiment.transform.GetChild(2).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(3).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(5).gameObject.SetActive(true);
            ObjectToDrown.NewExperiment.transform.GetChild(6).gameObject.SetActive(true);
        }
        else if (SpawnedObjectNum == 3)
        {
            ObjectToDrown.NewExperiment.transform.GetChild(0).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(1).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(2).gameObject.SetActive(true);
            ObjectToDrown.NewExperiment.transform.GetChild(3).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(5).gameObject.SetActive(true);
            ObjectToDrown.NewExperiment.transform.GetChild(6).gameObject.SetActive(true);
        }
        else
        {
            ObjectToDrown.NewExperiment.transform.GetChild(0).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(1).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(2).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(3).gameObject.SetActive(true);
            ObjectToDrown.NewExperiment.transform.GetChild(5).gameObject.SetActive(false);
            ObjectToDrown.NewExperiment.transform.GetChild(6).gameObject.SetActive(false);
        }
    }

    public void DrownSpawnedObject()
    {
        switch (SpawnedObjectNum)
        {
            case 1:
                {
                    if (IsDrowned)
                    {
                        ObjectToDrown.NewExperiment.transform.GetChild(4).transform.localPosition = new Vector3(0f, 0.0375f, 0f);
                        ObjectToDrown.NewExperiment.transform.GetChild(4).transform.localScale = new Vector3(0.0922f, 0.0375f, 0.0922f);
                        ObjectToDrown.NewExperiment.transform.GetChild(0).transform.localPosition = new Vector3(0f, 0.23f, 0f);
                        IsDrowned = false;
                        DrownButton.GetComponentInChildren<Text>().text = "Drown object";
                    }
                    else
                    {
                        ObjectToDrown.NewExperiment.transform.GetChild(4).transform.localPosition = new Vector3(0f, 0.0463f, 0f);
                        ObjectToDrown.NewExperiment.transform.GetChild(4).transform.localScale = new Vector3(0.0922f, 0.0463f, 0.0922f);
                        ObjectToDrown.NewExperiment.transform.GetChild(0).transform.localPosition = new Vector3(0f, 0.06f, 0f);
                        IsDrowned = true;
                        DrownButton.GetComponentInChildren<Text>().text = "Raise object";
                    }
                    break;
                }

            case 2:
                {
                    if (IsDrowned)
                    {
                        ObjectToDrown.NewExperiment.transform.GetChild(4).transform.localPosition = new Vector3(0f, 0.0375f, 0f);
                        ObjectToDrown.NewExperiment.transform.GetChild(4).transform.localScale = new Vector3(0.0922f, 0.0375f, 0.0922f);
                        ObjectToDrown.NewExperiment.transform.GetChild(1).transform.localPosition = new Vector3(0f, 0.23f, 0f);
                        IsDrowned = false;
                        DrownButton.GetComponentInChildren<Text>().text = "Drown object";
                    }
                    else
                    {
                        ObjectToDrown.NewExperiment.transform.GetChild(4).transform.localPosition = new Vector3(0f, 0.0451f, 0f);
                        ObjectToDrown.NewExperiment.transform.GetChild(4).transform.localScale = new Vector3(0.0922f, 0.0451f, 0.0922f);
                        ObjectToDrown.NewExperiment.transform.GetChild(1).transform.localPosition = new Vector3(0f, 0.045f, 0f);
                        IsDrowned = true;
                        DrownButton.GetComponentInChildren<Text>().text = "Raise object";
                    }
                    break;
                }

            case 3:
                {
                    if (IsDrowned)
                    {
                        ObjectToDrown.NewExperiment.transform.GetChild(4).transform.localPosition = new Vector3(0f, 0.0375f, 0f);
                        ObjectToDrown.NewExperiment.transform.GetChild(4).transform.localScale = new Vector3(0.0922f, 0.0375f, 0.0922f);
                        ObjectToDrown.NewExperiment.transform.GetChild(2).transform.localPosition = new Vector3(0f, 0.2f, 0f);
                        IsDrowned = false;
                        DrownButton.GetComponentInChildren<Text>().text = "Drown object";
                    }
                    else
                    {
                        ObjectToDrown.NewExperiment.transform.GetChild(4).transform.localPosition = new Vector3(0f, 0.044f, 0f);
                        ObjectToDrown.NewExperiment.transform.GetChild(4).transform.localScale = new Vector3(0.0922f, 0.044f, 0.0922f);
                        ObjectToDrown.NewExperiment.transform.GetChild(2).transform.localPosition = new Vector3(0f, 0.015f, 0f);
                        IsDrowned = true;
                        DrownButton.GetComponentInChildren<Text>().text = "Raise object";
                    }
                    break;
                }
            case 4:
                {
                    if (IsDrowned)
                    {
                        IsDrowned = false;
                        DrownButton.GetComponentInChildren<Text>().text = "Drown object";
                    }
                    else
                    {
                        IsDrowned = true;
                        DrownButton.GetComponentInChildren<Text>().text = "Raise object";
                    }
                    break;
                }
        }
    }
}
