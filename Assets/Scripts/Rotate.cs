using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour
{
    [SerializeField] private InputField AngleInputField;

    private ExperimentSystem Experiment;

    // Start is called before the first frame update
    void Start()
    {
        Experiment = FindObjectOfType<ExperimentSystem>();
        AngleInputField.onEndEdit.AddListener(RotateFunc);
    }

    void RotateFunc(string NewAngle)
    {
        if(Experiment.ExperimentIsPlaced == false)
        {
            return;
        }

        float ZAxis;

        if (NewAngle.Length > 0)
        {
            foreach (char c in NewAngle)
            {
                if (c < '0' || c > '9')
                {
                    ZAxis = 10.0f;
                    return;
                }
            }

            ZAxis = float.Parse(NewAngle);

            if (ZAxis > 90)
            {
                ZAxis = 90.0f;
            }
            else if (ZAxis < 0)
            {
                ZAxis = 0.0f;
            }

            Experiment.NewExperiment.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0, 0, ZAxis);
        }
    }

}
