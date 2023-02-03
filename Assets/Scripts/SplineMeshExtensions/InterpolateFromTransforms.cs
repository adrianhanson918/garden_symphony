using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using SplineMesh;

namespace SplineMeshExtensions {
    
    
    [ExecuteInEditMode]
    [RequireComponent(typeof(Spline))]
    public class InterpolateFromTransforms
        : MonoBehaviour
    {
        public List<Transform> splineTransforms = new List<Transform>();
        
        private float distance;
        private float _deltaTime;
        private Spline _spline;


        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _spline = GetComponent<Spline>();
            if (splineTransforms.Count != _spline.nodes.Count)
            {
                _spline.nodes.Clear();
                foreach (var splineTransform in splineTransforms)
                {
                    _spline.nodes.Add(new SplineNode(splineTransform.position, splineTransform.forward));
                }
            }
        }

        private void OnEnable() {
            Init();
#if UNITY_EDITOR
            EditorApplication.update += EditorUpdate;
#endif
        }

        void OnDisable() {
#if UNITY_EDITOR
            EditorApplication.update -= EditorUpdate;
#endif
        }

        private void Update()
        {
            CalculateSpline();
        }

        void EditorUpdate()
        {
            CalculateSpline();
        }

        private void CalculateSpline()
        {
            SetSplinePositions();
        }

        private void SetSplinePositions()
        {
            for (int i = 0; i < _spline.nodes.Count; i++)
            {
                _spline.nodes[i].Position = splineTransforms[i].localPosition;
            }
        }
    }
}