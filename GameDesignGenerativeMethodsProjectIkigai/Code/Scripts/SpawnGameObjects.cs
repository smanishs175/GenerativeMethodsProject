using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObjects : MonoBehaviour
{
    public GameObject spawnPrefab;
	public float minSecondsBetweenSpawning = 8.0f;
	public float maxSecondsBetweenSpawning = 12.0f;
	public float minOffsetX = 2.0f;
	public float minOffsetY = 1.5f;
	public float minOffsetZ = 2.30f;
	public float maxOffsetX = 4.20f;
	public float maxOffsetY = 4.30f;
	public float maxOffsetZ = 4.450f;
	private float savedTime;
	private float secondsBetweenSpawning;

	// Use this for initialization
	void Start()
	{
		savedTime = Time.time;
		secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
		MakeThingToSpawn();
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time - savedTime >= secondsBetweenSpawning) // is it time to spawn again?
		{
			
			savedTime = Time.time; // store for next spawn
			secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
		}
	}

	void MakeThingToSpawn()
	{
		// create a new gameObject
		Vector3 currentPosition = transform.position;
		//will spawn objects in and around the spawner object withing a particular range
		currentPosition.x = currentPosition.x + Random.Range(minOffsetX, maxOffsetX);
		currentPosition.y = currentPosition.y + Random.Range(minOffsetY, maxOffsetY);
		currentPosition.z = currentPosition.z + Random.Range(minOffsetZ, maxOffsetZ);
		transform.position = currentPosition;
		GameObject clone = Instantiate(spawnPrefab, transform.position , transform.rotation) as GameObject;
	}
}
