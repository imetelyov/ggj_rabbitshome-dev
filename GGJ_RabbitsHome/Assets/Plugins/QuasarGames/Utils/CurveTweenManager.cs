using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace QuasarGames.Utils
{

    [System.Serializable]
    public class ObjectToTween
    {
        // one second normalized curve
        public AnimationCurve curve;

        public float animationTime;
        public float delayTime;

        public float currentTime;

        public Transform transform;

        public Vector3 startPosition;
        public Vector3 goalPosition;

        public bool activateOnMovement;
        public bool deactivateAfterMovement;
    }

    public class CurveTweenManager : MonoBehaviour
    {
        public static CurveTweenManager Instance;

        [SerializeField]
        private bool isChanging = false;

        [SerializeField]
        private List<ObjectToTween> objectsToTween = new List<ObjectToTween>();

        public void Awake()
        {
            Instance = this;
        }

        public void AddObject(Transform transform, Vector3 goalPosition, float time = 1f, float delay = 0f, string curveName = "Linear", bool activateOnMovement = false, bool deactivateAfterMovement = false)
        {
            ObjectToTween newObject = new ObjectToTween();

            newObject.curve = CurveLibrary.Instance.GetCurve(curveName);

            newObject.animationTime = time;
            newObject.delayTime = delay;

            newObject.currentTime = 0f;

            newObject.transform = transform;

            newObject.startPosition = transform.position;
            newObject.goalPosition = goalPosition;

            newObject.activateOnMovement = activateOnMovement;
            newObject.deactivateAfterMovement = deactivateAfterMovement;

            objectsToTween.Add(newObject);

            isChanging = true;
        }

        public void Reset()
        {
            objectsToTween.Clear();

            isChanging = false;
        }

        void Update()
        {
            float deltaTime = Time.deltaTime;

            if (isChanging)
            {
                if (objectsToTween.Count == 0)
                {
                    isChanging = false;
                    return;
                }

                List<ObjectToTween> objToDelete = new List<ObjectToTween>();

                // Tween
                foreach (ObjectToTween obj in objectsToTween)
                {
                    if (obj.transform == null)
                    {
                        objToDelete.Add(obj);
                        continue;
                    }


                    obj.currentTime += deltaTime;

                    if (obj.currentTime <= obj.delayTime)
                    {
                        // delay fix
                        obj.startPosition = obj.transform.position;
                        continue;
                    }

                    if (obj.currentTime - obj.delayTime <= obj.animationTime)
                    {
                        // TODO ???
                        if (obj.activateOnMovement && !obj.transform.gameObject.activeSelf)
                            obj.transform.gameObject.SetActive(true);
                        
                        obj.transform.position = Vector3.LerpUnclamped(obj.startPosition, obj.goalPosition, obj.curve.Evaluate((obj.currentTime - obj.delayTime) / obj.animationTime));
                    }
                    else
                    {
                        // TODO ???
                        if (obj.activateOnMovement && !obj.transform.gameObject.activeSelf)
                            obj.transform.gameObject.SetActive(true);

                        if (obj.deactivateAfterMovement && obj.transform.gameObject.activeSelf)
                            obj.transform.gameObject.SetActive(false);

                        obj.transform.position = obj.goalPosition;

                        objToDelete.Add(obj);
                    }

                }

                // Remove finished tweens
                int delCount = objToDelete.Count;
                if (delCount >= 1)
                {
                    for (int i = delCount; i > 0; i--)
                    {
                        objectsToTween.Remove(objToDelete[i-1]);
                    }
                }
            }

        }
    }
}
