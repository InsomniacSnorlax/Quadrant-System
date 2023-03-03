using System.Collections.Generic;
using UnityEngine;

namespace Snorlax.QuadrantSystem
{
    public class QuadTesting : MonoBehaviour
    {
        [Header("Initialized Values")]
        public GameObject InstantiateObject;
        public Material SelectedMaterial;
        public int NumberOfEntities;
        [Header("Dynamic Value")]
        public int Radius;
        List<Transform> list = new List<Transform>();
        public void Start()
        {
            for (int i = 0; i < NumberOfEntities; i++)
            {
                GameObject gameObject = Instantiate(InstantiateObject, new Vector3(UnityEngine.Random.Range(-20, 20), 0, UnityEngine.Random.Range(-20, 20)), Quaternion.identity);
                gameObject.name += (" " + i);
                //QuadSystem.entities.Add(gameObject);
            }
        }

        private void Update()
        {
            QuadSystem.OnUpdate();
        }

        private void OnGUI()
        {
            if(GUILayout.Button("Get Targets"))
            {
                //list = QuadSystem.FindTargets(InstantiateObject.transform, Radius);
            }

            if (GUILayout.Button("Get Closest Target"))
            {
                //QuadSystem.FindTarget(InstantiateObject.transform, Radius);
            }
        }

    }
}
