using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Common
{
    public class ResourceScanner_2 : MonoBehaviour
    {
        [SerializeField] private float _scanRadius;

        private Collider[] _gameObjects;

        private void Start()
        {
            _gameObjects = new Collider[100];
        }


        public IEnumerable<GameObject> Scan()
        {
            List<Collider> result = new List<Collider>();
            Physics.OverlapSphereNonAlloc(transform.position, _scanRadius, _gameObjects);
            foreach (Collider col in _gameObjects)
            {
                Debug.Log("Рядом объект: " + col.gameObject.name);
            }

            foreach (Collider o in _gameObjects)
            {
                Base block = o.GetComponent<Base>();
                if (block == null)
                {
                    result.Add(o);
                }
            }

            return result.Select(h => h.gameObject);
        }
    }
}