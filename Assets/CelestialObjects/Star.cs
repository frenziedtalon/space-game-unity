using Assets.CelestialObjects.BaseObjects;
using Assets.CelestialObjects.Interfaces;
using Assets.Factories;
using Assets.Measurements;
using UnityEngine;

namespace Assets.CelestialObjects
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Star : CelestialObjectBase, ISphere
    {
        private bool _initialised { get; set; }

        private Distance _radius;
        public Distance Radius => _radius;

        // Additional scaling to prevent star from overwhelming FOV
        private const float _starRadiusScale = 0.5f;

        public void SetRadius(Distance radius)
        {
            _radius = radius;
        }
        public override void Awake()
        {
            //Debug.Log("Star.Awake() was called");

            base.Awake();
        }

        // TODO: This is a hack. Fix it when implementing orbits
        public void OnEnable()
        {
            //Debug.Log("Star.OnEnable() was called");

            if (!_initialised)
            {
                //Debug.Log("Star - Initialising");

                SetupMass();
                SetupInitialPosition();
                SetupAppearance();
                _initialised = true;
            }
        }

        public override void Start()
        {
            //Debug.Log("Star.Start() was called");

            base.Start();
        }

        //public override void Update()
        //{
        //    base.Update();
        //}

        //public override void FixedUpdate()
        //{
        //    base.FixedUpdate();
        //}

        private void SetupMass()
        {
            var rigidBody = GetComponent<Rigidbody>();
            rigidBody.mass = (float)Mass.ScaledUnits;
            rigidBody.useGravity = false;
        }

        private void SetupInitialPosition()
        {
            gameObject.transform.position = Vector3.zero; // Sun is rendered at (0, 0, 0)
        }

        private void SetupAppearance()
        {
            float radius = (float)Radius.ScaledUnitsRadius * _starRadiusScale;
            gameObject.transform.localScale = new Vector3(radius, radius, radius);

            var mesh = gameObject.GetComponent<MeshFilter>();
            mesh.mesh = MeshFactory.GetUnityPrimitiveMesh(PrimitiveType.Sphere);

            var meshRenderer = gameObject.GetComponent<MeshRenderer>();
            meshRenderer.material.color = Color.white;

            Shader shader = ShaderFactory.Create(ShaderType.URP_Lit);
            meshRenderer.material.shader = shader;
        }
    }
}