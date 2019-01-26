using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace QuasarGames
{

    [System.Serializable]
    public class ObjectToMove
    {
        public RectTransform rTransform;
        public Vector2 startPosition;
        public Vector2 currentPosition;
        public Vector2 goalPosition;
    }

    public class CurveBasedMoveTween : MonoBehaviour
    {
        //public GridClass gridM;

        public bool isChanging = false;

        [Range(0.01f, 2.0f)]
        public float animationTime = 1f;

        // one second normalized curve
        public AnimationCurve valueCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);


        [Header("For horizontal/vertical move only")]
        // one second normalized curve
        public AnimationCurve shrinkCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

        // one second normalized curve
        public AnimationCurve flatteningCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);


        private bool fastMode = false;
        private float fastMultiplicator = 5f;

        private float elapsedTime = 0f;

        private List<ObjectToMove> objectsToMove = new List<ObjectToMove>();

        public void Awake()
        {
            //if (gridM == null)
            //{
            //    gridM = GetComponent<GridManager>();

            //}

            fastMode = false;
            fastMultiplicator = 5f;
        }

        public void AddObject(RectTransform newRTransform, Vector2 newGoalPosition)
        {
            ObjectToMove newObject = new ObjectToMove();

            newObject.rTransform = newRTransform;
            newObject.startPosition = newRTransform.anchoredPosition;
            newObject.currentPosition = newRTransform.anchoredPosition;
            newObject.goalPosition = newGoalPosition;

            objectsToMove.Add(newObject);
        }

        public void Reset()
        {
            objectsToMove.Clear();

            isChanging = false;
            elapsedTime = 0f;

            fastMode = false;
            fastMultiplicator = 5f;
        }

        public void MoveToPositionInstantly()
        {

            foreach (ObjectToMove obj in objectsToMove)
            {
                obj.startPosition = obj.goalPosition;
                obj.currentPosition = obj.goalPosition;
                obj.rTransform.anchoredPosition = obj.currentPosition;
            }

            isChanging = false;
            elapsedTime = 0f;
        }

        public void MoveToPosition()
        {
            if (objectsToMove.Count > 0)
            {
                isChanging = true;
                elapsedTime = 0f;
            }
            else
            {
                isChanging = false;
                elapsedTime = 0f;
            }
        }

        public void MoveToPositionFast()
        {
            MoveToPositionFast(5f);
        }

        public void MoveToPositionFast(float newFastMultiplicator)
        {

            fastMode = true;

            fastMultiplicator = (newFastMultiplicator <= 1f) ? 1f : newFastMultiplicator;

            if (objectsToMove.Count > 0)
            {
                isChanging = true;
                elapsedTime = 0f;
            }
            else
            {
                isChanging = false;
                elapsedTime = 0f;
            }
        }


        void Update()
        {
            if (isChanging)
            {

                float animationTimeModified = (fastMode) ? (animationTime / fastMultiplicator) : (animationTime);

                Vector2 cellSize = new Vector2();//gridM.GetCellSize();
                                                 // maximum possible distance
                float gridSizeX = 1f;// gridM.GetGridX() - cellSize.x;

                elapsedTime += Time.deltaTime;

                if ((elapsedTime <= animationTimeModified) && (animationTimeModified >= 0.01f))
                {

                    float multiplicator = valueCurve.Evaluate(elapsedTime / animationTimeModified);

                    float shrinkMultiplicator = shrinkCurve.Evaluate(elapsedTime / animationTimeModified);
                    float flatteningMultiplicator = flatteningCurve.Evaluate(elapsedTime / animationTimeModified);

                    foreach (ObjectToMove obj in objectsToMove)
                    {
                        Vector2 deltaPosition = (obj.goalPosition - obj.startPosition);

                        float displacement = deltaPosition.magnitude;
                        float normalizedDisplacement = displacement / gridSizeX;

                        if (displacement > (cellSize.x / 100))
                        {
                            // POSITION
                            obj.currentPosition = deltaPosition * multiplicator + obj.startPosition;

                            obj.rTransform.anchoredPosition = obj.currentPosition;


                            // SCALE
                            Vector3 scaleVect = new Vector3(1f, 1f, 1f);

                            Vector2 directionVect = deltaPosition.normalized;

                            if ((Mathf.Abs(directionVect.x) == 1f)
                                && (directionVect.y == 0))
                            {
                                scaleVect.x = (shrinkMultiplicator - 1) * normalizedDisplacement + 1;
                                scaleVect.y = (flatteningMultiplicator - 1) * normalizedDisplacement + 1;
                            }
                            else
                            {
                                scaleVect.y = (shrinkMultiplicator - 1) * normalizedDisplacement + 1;
                                scaleVect.x = (flatteningMultiplicator - 1) * normalizedDisplacement + 1;
                            }

                            obj.rTransform.localScale = scaleVect;

                        }

                    }

                }

                else
                {
                    foreach (ObjectToMove obj in objectsToMove)
                    {
                        obj.currentPosition = obj.goalPosition;

                        obj.rTransform.anchoredPosition = obj.currentPosition;

                        obj.rTransform.localScale = new Vector3(1f, 1f, 1f);
                    }

                    isChanging = false;
                }


            }
        }

    }
}