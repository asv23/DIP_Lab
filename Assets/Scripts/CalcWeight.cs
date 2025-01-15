using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalcWeight : MonoBehaviour
{
    [SerializeField] private InputField AngleInputField;
    [SerializeField] private InputField MassInputField;
    [SerializeField] private InputField OutputField;

    [SerializeField] private InputField FFOutputField;
    [SerializeField] private InputField WOutputField;

    private string angle = "0";
    private string mass = "0,025";
    private float coeff;

    private ExperimentSystem Experiment;

    // Start is called before the first frame update
    void Start()
    {
        Experiment = FindObjectOfType<ExperimentSystem>();
        AngleInputField.onEndEdit.AddListener(AngleCalcFunc);
        MassInputField.onEndEdit.AddListener(MassCalcFunc);
        coeff = Random.Range(20f, 50f) / 100f;
    }

    void AngleCalcFunc(string NewAngle)
    {
        if(!Experiment.ExperimentIsPlaced)
        {
            return;
        }

        if (NewAngle.Length > 0)
        {
            foreach (char c in NewAngle)
            {
                if (c < '0' || c > '9')
                {
                    return;
                }
            }
        }
        else
        {
            return;
        }


        if (float.Parse(NewAngle) > 90)
        {
            angle = "90";
        }
        else if (float.Parse(NewAngle) < 0)
        {
            angle = "0";
        }
        else
        {
            angle = NewAngle;
        }

        //float RadAngle = (float.Parse(angle) + 90f) * Mathf.Deg2Rad;
        //float weight = Mathf.Cos(RadAngle) * float.Parse(mass) * 9.81f;
        float RadAngle = float.Parse(angle) * Mathf.Deg2Rad;
        float weight = Mathf.Sin(RadAngle) * float.Parse(mass) * 9.81f;
        weight = Mathf.Round(weight * 1000f) / 1000f;

        float FriForce = coeff * Mathf.Cos(RadAngle) * float.Parse(mass) * 9.81f;
        FriForce = Mathf.Round(FriForce * 1000f) / 1000f;

        FFOutputField.text = FriForce.ToString();
        WOutputField.text = weight.ToString();

        if (FriForce >= weight)
        {
            weight = 0;
        }
        else if (float.Parse(angle) != 90 || float.Parse(angle) != 0)
        {
            weight -= FriForce;
            weight = Mathf.Round(weight * 1000f) / 1000f;
        }

        //OutputField.text = weight.ToString();
        OutputField.text = weight.ToString();
    }

    void MassCalcFunc(string NewMass)
    {
        if (!Experiment.ExperimentIsPlaced)
        {
            return;
        }

        if (NewMass.Length > 0)
        {
            NewMass = NewMass.Replace(".", ",");
            foreach (char c in NewMass)
            {
                if (c < '0' || c > '9')
                {
                    if (c != ',')
                    {
                        return;
                    }
                }
            }
        }
        else
        {
            return;
        }

        if (float.Parse(NewMass) < 0.025f)
        {
            NewMass = "0,025";
        }
        else if(float.Parse(NewMass) > 0.4f)
        {
            NewMass = "0,4";
        }

        mass = NewMass;
        //float RadAngle = (float.Parse(angle) + 90f) * Mathf.Deg2Rad;
        //float weight = Mathf.Cos(RadAngle) * float.Parse(mass) * 9.81f;
        float RadAngle = float.Parse(angle) * Mathf.Deg2Rad;
        float weight = Mathf.Sin(RadAngle) * float.Parse(mass) * 9.81f;
        weight = Mathf.Round(weight * 1000f) / 1000f;

        float FriForce = coeff * Mathf.Cos(RadAngle) * float.Parse(mass) * 9.81f;
        FriForce = Mathf.Round(FriForce * 1000f) / 1000f;

        //FFOutputField.text = FriForce.ToString();
        //WOutputField.text = weight.ToString();

        if (FriForce >= weight)
        {
            weight = 0;
        }
        else if (float.Parse(angle) != 90 || float.Parse(angle) != 0)
        {
            weight -= FriForce;
            weight = Mathf.Round(weight * 1000f) / 1000f;
        }

        //OutputField.text = weight.ToString();
        OutputField.text = weight.ToString();
    }
}
