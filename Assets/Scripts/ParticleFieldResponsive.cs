using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


    public class ParticleFieldResponsive : MonoBehaviour
    {
        public MeshFilter meshFilter;

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneStart;
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            Camera camera = Camera.main;
            Mesh mesh = meshFilter.mesh;

            Vector2 min = camera.ViewportToWorldPoint(new Vector2(0f, 0f));
            Vector2 max = camera.ViewportToWorldPoint(new Vector2(1f, 1f));

            float width = max.x - min.x;
            float height = max.y - min.y;

            float widthScaleFactor = (width / mesh.bounds.size.x);
            float heightScaleFactor = (height / mesh.bounds.size.y);

            // Debug.Log(widthScaleFactor + ":" + heightScaleFactor);

            Vector3 scale = transform.localScale;
            scale.x *= widthScaleFactor;
            scale.y *= heightScaleFactor;

            ParticleSystem ps = GetComponent<ParticleSystem>();
            var sh = ps.shape;
            sh.scale = scale;

            ps.Play();
        }

        private void ResponsiveScale()
        {
            Camera camera = Camera.main;
            Mesh mesh = meshFilter.mesh;

            Vector2 min = camera.ViewportToWorldPoint(new Vector2(0f, 0f));
            Vector2 max = camera.ViewportToWorldPoint(new Vector2(1f, 1f));

            float width = max.x - min.x;
            float height = max.y - min.y;

            float widthScaleFactor = (width / mesh.bounds.size.x);
            float heightScaleFactor = (height / mesh.bounds.size.y);

            // Debug.Log(widthScaleFactor + ":" + heightScaleFactor);

            Vector3 scale = transform.localScale;
            scale.x *= widthScaleFactor;
            scale.y *= heightScaleFactor;

            ParticleSystem ps = GetComponent<ParticleSystem>();
            var sh = ps.shape;
            sh.scale = scale;
        }

        private void OnSceneStart(Scene scene, LoadSceneMode mode)
        {
            // ResponsiveScale();
        }
    }
