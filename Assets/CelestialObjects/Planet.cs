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
    public class Planet : OrbitingCelestialObjectBase, ISphere
    {
        private bool _initialised { get; set; }

        private Distance _radius;
        public Distance Radius => _radius;

        public void SetRadius(Distance radius)
        {
            _radius = radius;
        }

        public override void Awake()
        {
            //Debug.Log("Planet.Awake() was called");

            base.Awake();
        }

        // TODO: This is a hack. Fix it when implementing orbits
        public void OnEnable()
        {
            //Debug.Log("Planet.OnEnable() was called");

            if (!_initialised)
            {
                //Debug.Log("Planet - Initialising");

                SetupMass();
                SetupInitialPosition();
                SetupAppearance();
                _initialised = true;
            }
        }

        public override void Start()
        {
            //Debug.Log("Planet.Start() was called");

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
            gameObject.transform.position = new Vector3((float)Orbit.SemiMajorAxis.ScaledUnitsOrbit, 0, 0);
        }

        private void SetupAppearance()
        {
            float radius = (float)Radius.ScaledUnitsRadius;
            gameObject.transform.localScale = new Vector3(radius, radius, radius);

            var mesh = gameObject.GetComponent<MeshFilter>();
            mesh.mesh = MeshFactory.GetUnityPrimitiveMesh(PrimitiveType.Sphere);

            var meshRenderer = gameObject.GetComponent<MeshRenderer>();
            meshRenderer.material.color = Color.white;

            Shader shader = Shader.Find("Diffuse");
            meshRenderer.material.shader = shader;
        }
    }
}
