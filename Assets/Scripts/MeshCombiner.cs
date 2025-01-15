using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Csg;

public class MeshCombiner : MonoBehaviour
{
    [SerializeField] private List<MeshFilter> sourceMeshFilters;
    [SerializeField] private MeshFilter targetMeshFilters;

    // Start is called before the first frame update
    void Start()
    {
        CombineMeshes();
    }

    private void CombineMeshes()
    {
        var combine = new CombineInstance[sourceMeshFilters.Count];

        for(int i = 0; i < sourceMeshFilters.Count; i++)
        {
            combine[i].mesh = sourceMeshFilters[i].sharedMesh;
            combine[i].transform = sourceMeshFilters[i].transform.localToWorldMatrix;
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        targetMeshFilters.mesh = mesh;

		var composite = new GameObject();
		//composite.AddComponent<MeshFilter>().sharedMesh = mesh;
		//composite.AddComponent<MeshRenderer>().sharedMaterials = gameObject.GetComponent<MeshRenderer>().sharedMaterials;

		////////////////////
		/*
		// Initialize two new meshes in the scene
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.localScale = Vector3.one * 1.3f;

		// Perform boolean operation
		Model result = CSG.Subtract(cube, sphere);

		// Create a gameObject to render the result
		var composite = new GameObject();
		composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
		composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
		////////////////////
		*/

		float volume = MeshVolume(mesh);
		string msg = "The volume of the mesh is " + volume + " cube units. " + gameObject.name;
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
		/*
		float v321 = p3.x * p2.y * p1.z;
		float v231 = p2.x * p3.y * p1.z;
		float v312 = p3.x * p1.y * p2.z;
		float v132 = p1.x * p3.y * p2.z;
		float v213 = p2.x * p1.y * p3.z;
		float v123 = p1.x * p2.y * p3.z;
		*/
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
