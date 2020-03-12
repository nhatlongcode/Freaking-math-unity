using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Connexion.Utility
{
    public class Theme : MonoBehaviour
    {
        public string themeName;
        public GameObject background;
        public Color32[] colorScheme = new Color32[6];
        public Color32 UIScheme;
        public Color32 lightTextColor;
        public Color32 darkTextColor;
        public AudioClip backgroundMusic;

        private void Start()
        {
            SceneManager.sceneLoaded += OnStartScene;
            //Persistent.Instance.audioController.music.PlayBGM();
        }

        private void OnStartScene(Scene scene, LoadSceneMode mode)
        {
            transform.position = Vector2.zero;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnStartScene;
        }
    }
}