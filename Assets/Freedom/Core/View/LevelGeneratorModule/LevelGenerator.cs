using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Freedom.Core.View.LevelGeneratorModule
{
    public class LevelGenerator : MonoBehaviour
    {
        private Vector3 scaleOne = Vector3.one;
        private Quaternion identityRotation = Quaternion.identity;
        private Vector3 movementDirection = Vector3.down;
        
        public const string LEVEL_TRIGGER_TAG = "LevelTrigger";
        
        public LevelGeneratorTrigger generatorTrigger;
        public LevelGeneratorTrigger recycleTrigger;
        public Transform groundContainer;
        public GameObject groundPrefab;
        public float speed;
        public float tileSize;
        public List<Transform> activeTiles;

        private List<Transform> unusedTiles;
        private Vector3 groundPosition;
        private Transform lastTile;
        private bool operating = false;

        private void OnEnable ()
        {
            generatorTrigger.OnLevelGeneratorTriggerEvent = OnGenerationTriggerEventHandler;
            recycleTrigger.OnLevelGeneratorTriggerEvent = OnRecycleTriggerEventHandler;
        }

        private void OnDisable ()
        {
            generatorTrigger.OnLevelGeneratorTriggerEvent = null;
            recycleTrigger.OnLevelGeneratorTriggerEvent = null;
        }

        private void Update ()
        {
            if (operating)
            {
                MoveLevel (Time.deltaTime);
            }
        }

        protected virtual void MoveLevel(float deltaTime)
        {
            groundPosition += (movementDirection.normalized * speed * deltaTime);

            groundContainer.localPosition = groundPosition;
        }

        public void StartLevel ()
        {
            lastTile = activeTiles [activeTiles.Count - 1];
            groundPosition = groundContainer.localPosition;
            unusedTiles = new List<Transform> ();

            operating = true;
        }

        private void OnGenerationTriggerEventHandler (Transform transform)
        {
            if (!operating)
                return;
            
            StartCoroutine (GenerateNextPiece());
        }

        private void OnRecycleTriggerEventHandler (Transform transform)
        {
            if (!operating)
                return;
            
            StartCoroutine (RecyclePiece(transform));
        }

        private IEnumerator RecyclePiece (Transform piece)
        {
            // remove from active
            activeTiles.Remove(piece);

            // disable
            piece.gameObject.SetActive(false);

            // add to recycle
            unusedTiles.Add(piece);

            yield return 0;
        }

        private IEnumerator GenerateNextPiece ()
        {
            Transform pieceTransform = GetPiece ();

            pieceTransform.gameObject.SetActive (true);

            activeTiles.Add (pieceTransform);

            lastTile = pieceTransform;

            yield return 0;
        }

        private Transform GetPiece ()
        {
            Transform pieceTransform = null;

            if (unusedTiles.Count > 0)
            {
                pieceTransform = unusedTiles [unusedTiles.Count - 1];
                unusedTiles.RemoveAt (unusedTiles.Count - 1);
            }
            else
            {
                pieceTransform = Instantiate<GameObject> (groundPrefab).GetComponent<Transform> ();
                pieceTransform.gameObject.SetActive (false);
                pieceTransform.SetParent (groundContainer);
                pieceTransform.localScale = scaleOne;
                pieceTransform.localRotation = identityRotation;
            }

            pieceTransform.localPosition = new Vector3 (0, lastTile.localPosition.y + tileSize, 0);

            return pieceTransform;
        }
    }
}