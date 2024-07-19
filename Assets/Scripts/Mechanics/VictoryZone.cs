using System.Security.Cryptography;
using Platformer.Gameplay;
using Platformer.UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        public int nextScene = 0;
        public VideoClip videoClip;

        [SerializeField] private VideoPlayer vp;

        [SerializeField] private bool playedOnce = false;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //play video
                if (Camera.main != null && vp == null && !playedOnce)
                {
                    Camera.main.gameObject.AddComponent<VideoPlayer>();
                    vp = Camera.main.GetComponent<VideoPlayer>();
                    vp.renderMode = VideoRenderMode.CameraNearPlane;
                    vp.clip = videoClip;
                    vp.loopPointReached += OnVideoFinished;
                    
                    vp.Play();
                }
            }
            
            if (Input.GetMouseButtonDown(1))
            {
                // stop play video
                if (Camera.main != null && vp != null)
                {
                    vp.Stop();
                    Destroy(gameObject);
                }
            }
        }
        
        public void OnVideoFinished(VideoPlayer thisPlay)
        {
            // SceneManager.LoadScene(nextScene);
            /*var gameController = GameObject.Find("GameController").GetComponent<MetaGameController>();
            gameController.ToggleMainMenu(true);
            myPlayer.SetActive(true); 
            startButton.SetActive(false);*/
            playedOnce = true;
            vp.enabled = false;
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.enabled = false;
            this.enabled = false;

        }
        
    }
    
}