using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Pipe;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public Player player;
        public Text scoreText;
        public GameObject gameOverUI;
        public Text scoreBoardCurScore;
        public Text scoreBoardBestScore;
        public PipeSpawner pipeSpawner;
        public AudioClip[] audioClips;

        private AudioSource audioSource;

        private ushort m_currentScore;
        private static ushort m_highScore;
        private bool m_isGameStarted;
        
        [SerializeField, ReadOnly]
        private ushort m_stage = 1;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            pipeSpawner = GetComponent<PipeSpawner>();
            audioSource = GetComponent<AudioSource>();

            player.OnDead += GameOver;
            player.OnScored += AddScore;
            player.OnScored += StageUpdate;

            if (m_isGameStarted) return;
            Time.timeScale = 0;
        }

        private void GameOver()
        {
            audioSource.clip = audioClips?[2];
            audioSource?.Play();
            
            gameOverUI.SetActive(true);
            
            if (m_highScore < m_currentScore)
            {
                m_highScore = m_currentScore;
            }

            scoreBoardCurScore.text = m_currentScore.ToString();
            scoreBoardBestScore.text = m_highScore.ToString();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void StartGame()
        {
            m_isGameStarted = true;
            Time.timeScale = 1;
        }

        private void AddScore()
        {
            m_currentScore++;
            scoreText.text = m_currentScore.ToString();
        }

        private void StageUpdate()
        {
            // 10점 획득 시 마다 스테이지 업그레이드
            if (m_currentScore != 0 && m_currentScore % 10 == 0)
            {
                Debug.Log("스테이지 업!");
                m_stage++;

                switch (m_stage)
                {
                    case 2:
                    {
                        pipeSpawner.CancelInvoke();
                        pipeSpawner.StartCoroutine(nameof(pipeSpawner.SpawnPipeWave));
                        pipeSpawner.spawnDelay = 0.8f;
                        pipeSpawner.degreeValue = 10;
                        pipeSpawner.waveGrade = 9;
                    }
                    break;
                    case 4:
                    {
                        pipeSpawner.degreeValue = 15;
                        pipeSpawner.waveGrade = 6;
                    }
                    break;
                    case 6:
                    {
                        pipeSpawner.spawnDelay = 0.5f;
                        pipeSpawner.degreeValue = 30;
                        pipeSpawner.waveGrade = 3;
                    }
                    break;
                    case 10:
                    {
                        pipeSpawner.isRandomize = true;
                        pipeSpawner.spawnDelay = 0.3f;
                    }
                    break;
                    default:
                    {
                    }
                    break;
                }
            }
        }
    }
}
