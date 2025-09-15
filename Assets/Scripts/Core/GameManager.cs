using UnityEngine;

namespace GameCore
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public enum GameState { Playing, Paused, Victory, Defeat }
        public GameState CurrentState { get; private set; } = GameState.Playing;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetGameState(GameState newState)
        {
            if (CurrentState == newState) return;
            CurrentState = newState;
            switch (newState)
            {
                case GameState.Victory:
                    UIManager.Instance.ShowVictoryScreen();
                    break;
                case GameState.Defeat:
                    UIManager.Instance.ShowDefeatScreen();
                    break;
                case GameState.Paused:
                    Time.timeScale = 0;
                    break;
                case GameState.Playing:
                    Time.timeScale = 1;
                    break;
            }
        }

        public void OnBaseDestroyed()
        {
            SetGameState(GameState.Defeat);
        }

        public void OnVictory()
        {
            SetGameState(GameState.Victory);
        }
    }
}