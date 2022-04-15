using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public Player player;
        public Text scoreText;
        public GameObject gameOverUI;
        public Text scoreBoardCurScore;
        public Text scoreBoardBestScore;

        private ushort m_currentScore;
        private static ushort m_highScore;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            player.OnDead += GameOver;
            player.OnScored += AddScore;
        }

        private void GameOver()
        {
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

        private void AddScore()
        {
            m_currentScore++;
            scoreText.text = m_currentScore.ToString();
        }
    }
}
