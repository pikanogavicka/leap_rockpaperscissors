using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
  public GameObject object_to_explode;
  private int cubesPerAxis = 8;
  public bool buttonActive = true;
  public Material sharedMaterial;

  private float explotionTime = float.PositiveInfinity;
  private List<GameObject> smallCubes = new List<GameObject>();
  private float delay = 1f;
  private float force = 5f;
  private float radius = 3.0f;

  public void Update()
  {
    //after 1s destroy the cubes created by explotion
    if (smallCubes.Count > 0 && Time.time - explotionTime > 2.0f) {
      Debug.Log("destroy");
      for (int i = 0; i < smallCubes.Count; i++) {
        Destroy(smallCubes[i]);
      }
      smallCubes = new List<GameObject>();
    }

    /*else if (smallCubes.Count > 0 && Time.time - explotionTime > 0.01f)
    {
      Debug.Log("decrease alpha");
      float alpha_decrease_const = .3f;
      //for (int i = 0; i < smallCubes.Count; i++)
      //{
      //because all the little cubes have a shared material changing one affects all of them
      float alpha = smallCubes[0].GetComponent<MeshRenderer>().sharedMaterial.color.a;
      alpha *= alpha_decrease_const;
      smallCubes[0].GetComponent<MeshRenderer>().sharedMaterial.color = new Color(1.0f, 0.0f, 1.0f, alpha);
      //}
    }*/
  }

  /*public void Start()
  {
    sharedMaterial = 
  }*/

  public void Main()
  {
    explotionTime = Time.time;
    for (int x = 0; x < cubesPerAxis; x++) {
      for (int y = 0; y < cubesPerAxis; y++) {
        for (int z = 0; z < cubesPerAxis; z++){
          CreateCube(new Vector3(x, y, z));
        }

      }
    }
  }

  void CreateCube(Vector3 coordinates)
  {
    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    Renderer rd = cube.GetComponent<Renderer>();
    //rd.material = object_to_explode.GetComponent<Renderer>().material;
    rd.material = sharedMaterial;

    cube.transform.localScale = object_to_explode.transform.localScale / cubesPerAxis;

    Vector3 firstCube = object_to_explode.transform.position - object_to_explode.transform.localScale * .5f + cube.transform.localScale * .5f;
    cube.transform.position = firstCube + Vector3.Scale(coordinates, cube.transform.localScale);

    Rigidbody rb = cube.AddComponent<Rigidbody>();
    rb.AddExplosionForce(force, object_to_explode.transform.position, radius);

    smallCubes.Add(cube);

  }

}
