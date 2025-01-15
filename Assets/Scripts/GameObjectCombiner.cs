using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Csg;

public class GameObjectCombiner : MonoBehaviour
{
	//private List<GameObject> sourceGameObjects;
	[SerializeField] private List<GameObject> sourceGameObjects;

	void Start()
	{
		//sourceGameObjects = new List<GameObject>();
		//GetAllChild();
		//GetChildRecursive(gameObject);
		CombineObjects();
	}
	/*
	private void GetChildRecursive(GameObject obj)
	{
		if (null == obj)
			return;

		foreach (Transform child in obj.transform)
		{
			if (null == child)
				continue;
			//child.gameobject contains the current child you can do whatever you want like add it to an array
			sourceGameObjects.Add(child.gameObject);
			GetChildRecursive(child.gameObject);
		}
	}
	*/
	private void GetAllChild()
	{
		sourceGameObjects = new List<GameObject>();

		for (int i = 0; i < transform.childCount; i++)
			sourceGameObjects.Add(transform.GetChild(i).gameObject);
	}

	private void CombineObjects()
	{
		Model result = new Model(sourceGameObjects[0]);

		GameObject tmp = new GameObject();
		tmp.AddComponent<MeshFilter>().sharedMesh = result.mesh;
		tmp.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();

		string msg = "sourceGameObjects.Count " + sourceGameObjects.Count;
		Debug.Log(msg);
		//msg = "Count 0 " + 0 + " sourceGameObjects[i].name " + sourceGameObjects[0].name;
		//Debug.Log(msg);

		for (int i = 1; i < sourceGameObjects.Count; i++)
		{
			/*
			msg = "Count i " + i + " sourceGameObjects[i].name " + sourceGameObjects[i].name;
			Debug.Log(msg);
			if (i + 1 < sourceGameObjects.Count)
			{
				msg = "Count i + 1 " + (i + 1) + " sourceGameObjects[i].name " + sourceGameObjects[i + 1].name;
				Debug.Log(msg);
			}
			GameObject tmpI = sourceGameObjects[i];
			Model tmpResult = new Model(tmpI);
			tmpResult = CSG.Union(tmp, tmpI);
			*/
			Model tmpResult = CSG.Union(tmp, sourceGameObjects[i]);
			tmp.GetComponent<MeshFilter>().sharedMesh = tmpResult.mesh;

			//System.Threading.Thread.Sleep(3000);
			//tmp.GetComponent<MeshRenderer>().sharedMaterials = tmpResult.materials.ToArray();
			//result = CSG.Union(sourceGameObjects[i], sourceGameObjects[i + 1]);
		}
		
		//var composite = new GameObject();
		//composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
		//composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
		
		//result = CSG.Subtract(sourceGameObjects[0], sourceGameObjects[1]);

		//var composite = new GameObject();
		//composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
		//composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();

		result = new Model(tmp);

		float volume = MeshVolume(result.mesh);
		msg = "The volume of the object is " + volume + " cube units. " + gameObject.name;
		Debug.Log(msg);
	}

	///////////////////////////////////////

	private float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
	{
		float v321 = p3.x * 1000 * p2.y * 1000 * p1.z * 1000;
		float v231 = p2.x * 1000 * p3.y * 1000 * p1.z * 1000;
		float v312 = p3.x * 1000 * p1.y * 1000 * p2.z * 1000;
		float v132 = p1.x * 1000 * p3.y * 1000 * p2.z * 1000;
		float v213 = p2.x * 1000 * p1.y * 1000 * p3.z * 1000;
		float v123 = p1.x * 1000 * p2.y * 1000 * p3.z * 1000;

		//return Mathf.Round((1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123) * 10000000f) / 10000000f;
		return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
	}

	private float MeshVolume(Mesh mesh)
	{
		float volume = 0;

		Vector3[] vertices = mesh.vertices;
		int[] triangles = mesh.triangles;

		for (int i = 0; i < triangles.Length; i += 3)
		{
			Vector3 p1 = vertices[triangles[i + 0]];
			Vector3 p2 = vertices[triangles[i + 1]];
			Vector3 p3 = vertices[triangles[i + 2]];
			volume += SignedVolumeOfTriangle(p1, p2, p3);			
		}
		volume *= this.transform.localScale.x * this.transform.localScale.y * this.transform.localScale.z;
		//volume = Mathf.Round(volume * 10000000f) / 10000000f;
		return Mathf.Abs(volume);
	}
}
