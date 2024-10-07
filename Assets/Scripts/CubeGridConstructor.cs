using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGridConstructor : MonoBehaviour
{
	public GameObject cube;
	public Vector3Int dimensions;
	public float side;
	private Rigidbody[] rbs;
	public float margin;
	public LayerMask exclude;

	private void OnTriggerEnter(Collider other)
	{
		foreach (Rigidbody rb in rbs){
			rb.isKinematic = false;
		}

		Debug.Log(other.gameObject.name);
	}

	private void Start(){
		BoxCollider collider = gameObject.AddComponent<BoxCollider>();
		collider.center = new Vector3(dimensions.x * side / 2 - side / 2, dimensions.y * side / 2 - side / 2, dimensions.z * side /2 - side / 2);
		collider.size = new Vector3(dimensions.x * side + margin * 2, dimensions.y * side + margin * 2, dimensions.z * side + margin * 2);
		collider.isTrigger = true;
		collider.excludeLayers=exclude;
		rbs = new Rigidbody[dimensions.x * dimensions.y * dimensions.z];
		int i = 0;
		for (int x = 0; x < dimensions.x; x++){
			for (int y = 0; y < dimensions.y; y++){
				for (int z = 0; z < dimensions.z; z++) {
					rbs[i] = Instantiate(cube, new Vector3(x * side, y * side + side/2, z * side) + transform.position, transform.rotation, transform).GetComponent<Rigidbody>();
					i++;
				}
			}
		}
	}
}
