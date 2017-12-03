using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public PlayerController Player;
        public float PlayerSpeed;
        public GameObject[] ObstaclesPrefabs;
        public Dictionary<int, List<GameObject>> ObstaclePools = new Dictionary<int, List<GameObject>>();
        public float ObstacleCooldown = 2;

        public static GameManager Instance
        {
            get { return instance ?? (instance = FindObjectOfType<GameManager>()); }
        }

        private static GameManager instance;

        private void Start()
        {
            for (var i = 0; i < ObstaclesPrefabs.Length; i++)
            {
                ObstaclePools[i] = new List<GameObject>();
            }

            StartCoroutine(SpawnObstacleRoutine());
        }

        private void Update()
        {
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
            {
                Player.Tap();
            }
        }

        private GameObject GetObjectFromPool(int id)
        {
            var obj = ObstaclePools[id].FirstOrDefault(gameObj => !gameObj.activeSelf);

            if (obj == null)
            {
                obj = Instantiate(ObstaclesPrefabs[id]);
                ObstaclePools[id].Add(obj);
            }
            else
            {
                obj.SetActive(true);
            }

            return obj;
        }

        private IEnumerator SpawnObstacleRoutine()
        {
            while (true)
            {
                var obj = GetObjectFromPool(Random.Range(0, ObstaclesPrefabs.Length));
                var pos = new Vector3();
                obj.transform.position = new Vector3(12,Random.Range(-3,3),0); 
                yield return new WaitForSeconds(ObstacleCooldown);
            }
        }
    }
}