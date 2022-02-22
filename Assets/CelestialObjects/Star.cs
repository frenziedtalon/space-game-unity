using Assets.CelestialObjects;
using Assets.CelestialObjects.Interfaces;
using Assets.Factories;
using Assets.Measurements;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Star : CelestialObjectBase, ISphere
{
    private Distance _radius;
    public Distance Radius => _radius;

    public void SetRadius(Distance radius)
    {
        _radius = radius;
    }

    public override void Start()
    {
        base.Start();

        SetupMass();
        SetupInitialPosition();
        SetupAppearance();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

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
        float radius = (float)Radius.ScaledUnits;
        gameObject.transform.localScale = new Vector3(radius, radius, radius);

        var mesh = gameObject.GetComponent<MeshFilter>();
        mesh.mesh = MeshFactory.GetUnityPrimitiveMesh(PrimitiveType.Sphere);

        var meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material.color = Color.white;

        Shader shader = Shader.Find("Diffuse");
        meshRenderer.material.shader = shader;
    }
}
