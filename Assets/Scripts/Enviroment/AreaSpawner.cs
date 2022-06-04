using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class AreaSpawner : MonoBehaviour
{
   [SerializeField] private GameObject spawnObject;
   [SerializeField] private Vector3 objectSize;
   [SerializeField] private int objectsCount;
   [SerializeField] private int spawnTryCount = 100;
   [SerializeField] private Vector3 minScale;
   [SerializeField] private Vector3 maxScale;
   [SerializeField] private Vector3 minEulerAngle;
   [SerializeField] private Vector3 maxEulerAngle;
   [SerializeField] private float minDistanceBetweenObjects;
   [SerializeField] private Vector2 spawnArea;
   [SerializeField] private Color gizmoColor = Color.white;

   [ContextMenu("Spawn Objects")]
   public void Spawn()
   {
      if (transform.childCount > 0)
         DeleteChildren();

      int currentTryCount = 0;
      List<GameObject> spawnedObjects = new List<GameObject>(objectsCount);
      for (int i = 0; i < objectsCount; i++)
      {
         currentTryCount = 0;
         while (currentTryCount < spawnTryCount)
         {
            Vector3 randomPosition = RandomPosition();
            if (IsValidPosition(spawnedObjects, randomPosition))
            {
               spawnedObjects.Add(SpawnObject(randomPosition));
               break;
            }

            currentTryCount++;
         }
      }
   }

   private Vector3 RandomPosition()
   {
      float randomXPosition = Random.Range(transform.position.x - spawnArea.x * objectSize.x, transform.position.x + spawnArea.x * objectSize.x);
      float randomZPosition = Random.Range(transform.position.z - spawnArea.y * objectSize.z, transform.position.z + spawnArea.y * objectSize.z);
      return new Vector3(randomXPosition, transform.position.y, randomZPosition);
   }

   private bool IsValidPosition(List<GameObject> spawnedObjects, Vector3 randomPosition)
   {
      float calculatedDistance;
      for (int i = 0; i < spawnedObjects.Count; i++)
      {
         calculatedDistance = Vector3.Distance(spawnedObjects[i].transform.position, randomPosition);
         if (calculatedDistance < minDistanceBetweenObjects)
            return false;
      }

      return true;
   }

   private GameObject SpawnObject(Vector3 randomPosition)
   {
      GameObject spawnedObject = Instantiate(spawnObject, randomPosition, Quaternion.identity, transform);
      spawnedObject.transform.localScale = RandomScale();
      spawnedObject.transform.eulerAngles = RandomEulerAngle();
      return spawnedObject;
   }

   private Vector3 RandomScale()
   {
      float xScale = Random.Range(minScale.x, maxScale.x);
      float yScale = Random.Range(minScale.y, maxScale.y);
      float zScale = Random.Range(minScale.z, maxScale.z);
      return new Vector3(xScale, yScale, zScale);
   }

   private Vector3 RandomEulerAngle()
   {
      float xEuler = Random.Range(minEulerAngle.x, maxEulerAngle.x);
      float yEuler = Random.Range(minEulerAngle.y, maxEulerAngle.y);
      float zEuler = Random.Range(minEulerAngle.z, maxEulerAngle.z);
      return new Vector3(xEuler, yEuler, zEuler);
   }

   private void DeleteChildren()
   {
      Transform[] children = transform.GetComponentsInChildren<Transform>(true);
      for (int i = 0; i < children.Length; i++)
      {
         if (transform != children[i].transform)
            DestroyImmediate(children[i].gameObject);
      }
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = gizmoColor;
      Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea.x, 0.001f, spawnArea.y));
   }
}
