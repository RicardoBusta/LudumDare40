using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public PlayerController Player;
        public float PlayerSpeed;
        public GameObject[] ObstaclesPrefabs;
        public Dictionary<int, List<GameObject>> ObstaclePools = new Dictionary<int, List<GameObject>>();
        public float ObstacleCooldown = 2;

        public GameObject GameOverScreen;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();
                }
                return instance;;
            }
        }

        private static GameManager instance;

        public Text Score;
        public Text HighScore;

        private const string SCORE_TEXT = "Score: {0} pts";
        private const string HIGH_SCORE_TEXT = "High Score: {0} pts";

        private float currentScore;

        public bool Pause = false;

        private void Start()
        {
            GameOverScreen.SetActive(false);
            
            for (var i = 0; i < ObstaclesPrefabs.Length; i++)
            {
                ObstaclePools[i] = new List<GameObject>();
            }

            StartCoroutine(SpawnObstacleRoutine());

            HighScore.text = string.Format(HIGH_SCORE_TEXT, PlayerPrefs.GetInt("HighScore", 0).ToString("000000000"));
        }

        private void Update()
        {
            if (Pause) return;

            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
            {
                Player.Tap();
            }

            currentScore += 10 * Time.deltaTime * Player.Multiplier;
            Score.text = string.Format(SCORE_TEXT, currentScore.ToString("000000000"));
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
                foreach (Transform child in obj.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }

            return obj;
        }

        private IEnumerator SpawnObstacleRoutine()
        {
            while (true)
            {
                var obj = GetObjectFromPool(Random.Range(0, ObstaclesPrefabs.Length));
                var pos = new Vector3();
                obj.transform.position = new Vector3(15, Random.Range(-4, 4), -1);
                yield return new WaitForSeconds(ObstacleCooldown*Random.Range(1.0f,2.0f));
            }
        }

        public void GameOver()
        {
            GameOverScreen.SetActive(true);
            if (PlayerPrefs.GetFloat("HighScore", 0) < currentScore)
            {
                PlayerPrefs.SetFloat("HighScore", currentScore);
            }
            Pause = true;
        }
    }
}