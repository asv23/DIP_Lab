using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;

public class VolumeOfMesh : MonoBehaviour
{
	private int operationCount;
    private int threads = 12;
    private int runCount = 1;
    void Start()
    {
        Dump();
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        string msg = "Name: " + gameObject.name + " count of triangles is " + mesh.triangles.Length + " threads " + threads;
        UnityEngine.Debug.Log(msg);

        
        double MinValue = 1000000;        
        for (int i = 0; i < runCount; i++)
        {
            operationCount = 0;
            Stopwatch sw = Stopwatch.StartNew();
            double volume = SingleMeshVolume(mesh);
            double elapsed = sw.Elapsed.TotalMilliseconds;
            if (MinValue > elapsed)
            {
                MinValue = elapsed;
                msg = "Single volume of the mesh is " + volume + " cube units. " + gameObject.name + " Operation Count is " + operationCount + " ElapsedTime is " + MinValue;
                UnityEngine.Debug.Log(msg);
            }
        }
        
        MinValue = 1000000;
        for (int i = 0; i < runCount; i++)
        {
            operationCount = 0;
            Stopwatch sw = Stopwatch.StartNew();
            double volume = ParallelMeshVolume(mesh);
            double elapsed = sw.Elapsed.TotalMilliseconds;
            if (MinValue > elapsed)
            {
                MinValue = elapsed;
                msg = "Parallel volume of the mesh is " + volume + " cube units. " + gameObject.name + " Operation Count is " + operationCount + " ElapsedTime is " + MinValue;
                UnityEngine.Debug.Log(msg);
            }
        }
        /*
        operationCount = 0;
        Stopwatch sw = Stopwatch.StartNew();
        double volume = ParallelMeshVolume(mesh);
        msg = "Parallel volume of the mesh is " + volume + " cube units. " + gameObject.name + " Operation Count is " + operationCount + " ElapsedTime is " + sw.Elapsed.TotalMilliseconds;
        UnityEngine.Debug.Log(msg);
        */
    }

    private void Dump()
    {
        Parallel.For(0, threads, new ParallelOptions { MaxDegreeOfParallelism = threads }, (i) =>
        {

        });
    }

	private double SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
	{
		double v321 = p3.x * p2.y * p1.z;
		double v231 = p2.x * p3.y * p1.z;
		double v312 = p3.x * p1.y * p2.z;
		double v132 = p1.x * p3.y * p2.z;
		double v213 = p2.x * p1.y * p3.z;
		double v123 = p1.x * p2.y * p3.z;

		return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
	}
    private double SingleMeshVolumeH(Mesh mesh, int start, int end)
    {
        double volume = 0;

        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        for (int j = start; j < end; j++)
        {
            int i = j * 3;
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }

        string msg = "Single volume of the meshH is " + volume + " cube units.";
        UnityEngine.Debug.Log(msg);

        volume *= this.transform.localScale.x * this.transform.localScale.y * this.transform.localScale.z;
        operationCount = triangles.Length * 7 + 3;

        return Math.Abs(volume);
    }

    private double SingleMeshVolume(Mesh mesh)
	{
        double volume = 0;

		Vector3[] vertices = mesh.vertices;
		int[] triangles = mesh.triangles;

        for (int i = 0; i < triangles.Length; i += 3)
		{
            /*
            string msg = "triangle " + (i / 3);
            UnityEngine.Debug.Log(msg);
            msg = "triangles[" + i + "]: " + mesh.vertices[mesh.triangles[i]];
            UnityEngine.Debug.Log(msg);
            msg = "triangles[" + (i + 1) + "]: " + mesh.vertices[mesh.triangles[i + 1]];
            UnityEngine.Debug.Log(msg);
            msg = "triangles[" + (i + 2) + "]: " + mesh.vertices[mesh.triangles[i + 2]];
            UnityEngine.Debug.Log(msg);
            */
            Vector3 p1 = vertices[triangles[i + 0]];
			Vector3 p2 = vertices[triangles[i + 1]];
			Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }
        volume *= this.transform.localScale.x * this.transform.localScale.y * this.transform.localScale.z;
        operationCount = triangles.Length * 7 + 3;

        return Math.Abs(volume);
	}

    private double ParallelMeshVolume(Mesh mesh)
	{
        double volume = 0;
        int max = mesh.triangles.Length / 3;

        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        double[] results = new double[threads];

        Parallel.For(0, threads, new ParallelOptions { MaxDegreeOfParallelism = threads }, (i) =>
        {
            int start = (max / threads) * i;
            int end = start + (max / threads);
            if (i == (threads - 1))
            {
                end += max % threads;
            }

            if (end > max)
            {
                end = max;
            }

            results[i] = CalcMeshVolume(start, end, vertices, triangles);
            //string msg = "Thread I " + i + " start " + start + " end " + end + " result[i] " + results[i];
            //UnityEngine.Debug.Log(msg);
        });

        for (int i = 0; i < threads; i++)
        {
            volume += results[i];
        }
        volume *= this.transform.localScale.x * this.transform.localScale.y * this.transform.localScale.z;
        operationCount = triangles.Length * 7 + 3;

        return Math.Abs(volume);
    }

    private double CalcMeshVolume(int start, int end, Vector3[] vertices, int[] triangles)
    {
        double volume = 0;

        //string msg = "Start " + start + " end " + end;
        //UnityEngine.Debug.Log(msg);
        for (int j = start; j < end; j++)
        {
            int i = j * 3;
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }
        return volume;
    }
}
