using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class ExperimentSystem : MonoBehaviour
{
    [HideInInspector] public GameObject NewExperiment;
    [HideInInspector] public bool MarkerIsPlaced = false;
    [HideInInspector] public bool ExperimentIsPlaced = false;

    [SerializeField] private GameObject PlaneMarkerPrefab;
    [SerializeField] private GameObject FirstLabObject;
    [SerializeField] private GameObject SecondLabObject;
    [SerializeField] private GameObject ThirdLabObject;

    [SerializeField] private Canvas LabUserUI;

    private ARRaycastManager ARRaycastManagerScript;
    private GameObject Mark;
    private string SelectedLab;

    // Start is called before the first frame update
    void Start()
    {
        ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowMarker();
    }

    void ShowMarker()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (hits.Count > 0 && !ExperimentIsPlaced && MarkerIsPlaced)
        {
            Mark.transform.position = hits[0].pose.position;
            Mark.SetActive(true);
        }
    }

    public void SpawnExperimentSystem()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (!ExperimentIsPlaced && MarkerIsPlaced && SelectedLab == "L1")
        {
            Destroy(Mark);
            NewExperiment = Instantiate(FirstLabObject, hits[0].pose.position, FirstLabObject.transform.rotation);
            ExperimentIsPlaced = true;
            MarkerIsPlaced = false;

            LabUserUI.transform.GetChild(3).gameObject.SetActive(true);
            LabUserUI.transform.GetChild(4).gameObject.SetActive(true);
            LabUserUI.transform.GetChild(5).gameObject.SetActive(true);
            EventSystem.current.currentSelectedGameObject.SetActive(false);
        }

        if (!ExperimentIsPlaced && MarkerIsPlaced && SelectedLab == "L2")
        {
            Destroy(Mark);
            NewExperiment = Instantiate(SecondLabObject, hits[0].pose.position, FirstLabObject.transform.rotation);
            ExperimentIsPlaced = true;
            MarkerIsPlaced = false;

            LabUserUI.transform.GetChild(6).gameObject.SetActive(true);
            LabUserUI.transform.GetChild(7).gameObject.SetActive(true);
            LabUserUI.transform.GetChild(8).gameObject.SetActive(true);
            EventSystem.current.currentSelectedGameObject.SetActive(false);
        }
        /*
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !ExperimentIsPlaced && MarkerIsPlaced && SelectedLab == "L3")
        {
            Destroy(Mark);
            Mark.SetActive(false);
            NewExperiment = Instantiate(ThirdLabObject, hits[0].pose.position, FirstLabObject.transform.rotation);
            ExperimentIsPlaced = true;
            MarkerIsPlaced = false;
        }
        */
    }

    public void SetLab()
    {
        SelectedLab = EventSystem.current.currentSelectedGameObject.name;
        Mark = Instantiate(PlaneMarkerPrefab);
        Mark.SetActive(false);
        MarkerIsPlaced = true;

        LabUserUI.transform.GetChild(0).gameObject.SetActive(true);
        LabUserUI.transform.GetChild(1).gameObject.SetActive(true);
        LabUserUI.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void Clear()
    {
        Destroy(NewExperiment);
        Destroy(Mark);

        ExperimentIsPlaced = false;
        MarkerIsPlaced = false;

        EventSystem.current.currentSelectedGameObject.SetActive(false);
        LabUserUI.transform.GetChild(1).gameObject.SetActive(false);
        LabUserUI.transform.GetChild(2).gameObject.SetActive(true);
        LabUserUI.transform.GetChild(3).gameObject.SetActive(false);
        LabUserUI.transform.GetChild(4).gameObject.SetActive(false);
        LabUserUI.transform.GetChild(5).gameObject.SetActive(false);
        LabUserUI.transform.GetChild(6).gameObject.SetActive(false);
        LabUserUI.transform.GetChild(7).gameObject.SetActive(false);
        LabUserUI.transform.GetChild(8).gameObject.SetActive(false);
    }
}
