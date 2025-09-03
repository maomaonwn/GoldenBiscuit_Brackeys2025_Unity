using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scirpts.Manager
{
    public class GameManager : SingletonBase<GameManager>
    {
        [Header("UI")]
        public GameObject pausePanel;

        public bool IsPaused { get; private set; }

        public override void Awake()
        {
            // 安全兜底：进入场景时确保时间恢复
            Time.timeScale = 1f;
            IsPaused = false;
            if (pausePanel) pausePanel.SetActive(false);
        }

        void Update()
        {
            // Esc 切换暂停
            if (Input.GetKeyDown(KeyCode.Escape))
                TogglePause();
        }

        public void TogglePause()
        {
            if (IsPaused) Resume();
            else Pause();
        }

        public void Pause()
        {
            IsPaused = true;
            Time.timeScale = 0f;
            if (pausePanel) pausePanel.SetActive(true);
            AudioListener.pause = true;
        }

        public void Resume()
        {
            IsPaused = false;
            Time.timeScale = 1f;
            if (pausePanel) pausePanel.SetActive(false);
            AudioListener.pause = false;
        }

        /// <summary>玩家死亡时调用，立即或延时重启关卡</summary>
        public void OnPlayerDied(float delay = 0.5f)
        {
            Time.timeScale = 1f;
            IsPaused = false;
            if (pausePanel) pausePanel.SetActive(false);
            if (delay <= 0f) RestartLevel();
            else StartCoroutine(RestartAfter(delay));
        }

        IEnumerator RestartAfter(float seconds)
        {
            yield return new WaitForSecondsRealtime(seconds); // 用 Realtime，防止被暂停影响
            RestartLevel();
        }

        public void RestartLevel()
        {
            // 重新加载当前场景
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
            // 加载完成后，可选恢复 UI 状态
            Time.timeScale = 1f;
            IsPaused = false;
            if (pausePanel) pausePanel.SetActive(false);
        }
    }

}
