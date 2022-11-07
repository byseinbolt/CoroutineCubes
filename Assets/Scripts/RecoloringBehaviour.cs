using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class RecoloringBehaviour : MonoBehaviour
{ 
        [SerializeField] 
        private CubesSpawner _cubesSpawner;

        [SerializeField]
        private float _recoloringTime = 0.5f;

        [SerializeField] private float _delayBeforeNextCube = 0.2f;
        
        [UsedImplicitly]
        public void ChangeColors()
        {
                StartCoroutine(ChangeCubesColors(_recoloringTime,_delayBeforeNextCube));
        }

        private IEnumerator ChangeCubesColors(float recoloringTime, float delayBeforeNextCube) 
        {
                var randomColor = Random.ColorHSV();
                foreach (var cube in _cubesSpawner.Cubes)
                {
                        var cubeRenderer = cube.GetComponent<Renderer>();

                        StartCoroutine(ChangeCubeColor(cubeRenderer, recoloringTime, randomColor));
                        
                        yield return new WaitForSeconds(delayBeforeNextCube);
                }
        }

        private IEnumerator ChangeCubeColor(Renderer cubeRenderer, float recoloringTime, Color finalColor)
        {
                var startColor = cubeRenderer.material.color;
                var currentTime = 0f;
                
                while (currentTime < recoloringTime)
                {
                        var currentColor = Color.Lerp(startColor, finalColor, currentTime / recoloringTime);
                        cubeRenderer.material.color = currentColor;
                        currentTime += Time.deltaTime;

                        yield return null;
                }
                
                cubeRenderer.material.color = finalColor;
        }
}