using Assets;
using Assets.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    const string tagCelestial = "Celestial";
    readonly float G = 1f; // gravitational constant = 6.67408 x 10^-11 m3 kg-1 s-2

    GameObject[] celestials;

    // Start is called before the first frame update
    void Start()
    {
        CreateSolarSystem();

        celestials = GameObject.FindGameObjectsWithTag(tagCelestial);

        InitialVelocity();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Gravity();
    }

    void Gravity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized *
                        (G * (m1 * m2) / (r * r)));

                }
            }
        }
    }

    void InitialVelocity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform);

                    a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);

                }
            }
        }
    }

    GameObject CreateCelestialObject(
        string name,
        float mass, // 10^24 kg
        float radius, // 10^6 km
        CelestialObjectType type,
        float? semiMajorAxis, // 10^6 km
        float? eccentricity,
        float? inclination,
        float? argumentOfPeriapsis,
        float? longitudeOfAscendingNode,
        float? meanAnomalyZero)
    {
        switch (type)
        {
            case CelestialObjectType.Star:
                return CreateStar(name, mass, radius);
            case CelestialObjectType.Planet:
                return CreatePlanet(name, mass, radius, (float)(semiMajorAxis), (float)eccentricity, (float)inclination, (float)argumentOfPeriapsis, (float)longitudeOfAscendingNode, (float)meanAnomalyZero);
            default:
                throw new NotImplementedException();
        }
    }

    GameObject CreateStar(
        string name,
        float mass, // 10^24 kg
        float radius // 10^6 km
    )
    {
        var gameObject = new GameObject(name, typeof(SphereCollider))
        {
            tag = tagCelestial
        };
        var rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.mass = mass;
        rigidBody.useGravity = false;

        gameObject.transform.position = Vector3.zero; // Sun is rendered at (0, 0, 0)
        gameObject.transform.localScale = new Vector3(radius, radius, radius);

        var mesh = gameObject.AddComponent<MeshFilter>();
        mesh.mesh = MeshFactory.GetUnityPrimitiveMesh(PrimitiveType.Sphere);

        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material.color = Color.white;

        Shader shader = Shader.Find("Diffuse");
        meshRenderer.material.shader = shader;

        return gameObject;
    }

    GameObject CreatePlanet(
        string name,
        float mass, // 10^24 kg
        float radius, // 10^6 km
        float semiMajorAxis, // 10^6 km
        float eccentricity,
        float inclination,
        float argumentOfPeriapsis,
        float longitudeOfAscendingNode,
        float meanAnomalyZero)
    {
        var gameObject = new GameObject(name, typeof(SphereCollider))
        {
            tag = tagCelestial
        };
        var rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.mass = mass;
        rigidBody.useGravity = false;

        gameObject.transform.position = new Vector3(semiMajorAxis, 0, 0);
        gameObject.transform.localScale = new Vector3(radius, radius, radius);

        var mesh = gameObject.AddComponent<MeshFilter>();
        mesh.mesh = MeshFactory.GetUnityPrimitiveMesh(PrimitiveType.Sphere);

        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material.color = Color.white;

        Shader shader = Shader.Find("Diffuse");
        meshRenderer.material.shader = shader;

        return gameObject;
    }

    void CreateSolarSystem()
    {
        var sol = CreateCelestialObject("Sol", 1988550.00f, 0.6957f, CelestialObjectType.Star, null, null, null, null, null, null);
        var mercury = CreateCelestialObject("Mercury", 0.358416f, 0.00244f, CelestialObjectType.Planet, 57.90917567f, 0.205635f, 7.0047f, 29.1241f, 48.3313f, 168.6562f);
        var venus = CreateCelestialObject("Venus", 4.898352f, 0.006052f, CelestialObjectType.Planet, 108.208925506684f, 0.006773f, 3.3946f, 54.891f, 76.6799f, 48.0052f);
        var earth = CreateCelestialObject("Earth", 5.9736f, 0.006371f, CelestialObjectType.Planet, 149.597870691f, 0.016709f, 0f, 282.9404f, 0f, 356.047f);
        var mars = CreateCelestialObject("Mars", 0.657096f, 0.00339f, CelestialObjectType.Planet, 227.93663722813f, 0.093405f, 1.8497f, 286.5016f, 49.5574f, 18.6021f);
        var jupiter = CreateCelestialObject("Jupiter", 1898.41008f, 0.069911f, CelestialObjectType.Planet, 778.412026728313f, 0.048498f, 1.303f, 273.8777f, 100.4542f, 19.895f);
        var saturn = CreateCelestialObject("Saturn", 568.68672f, 0.058232f, CelestialObjectType.Planet, 1426.72541250233f, 0.055546f, 2.4886f, 339.3939f, 113.6634f, 316.967f);
        var uranus = CreateCelestialObject("Uranus", 87.21456f, 0.025362f, CelestialObjectType.Planet, 2870.97221979699f, 0.047318f, 0.7733f, 96.6612f, 74.0005f, 142.5905f);
        var neptune = CreateCelestialObject("Neptune", 102.74592f, 0.024622f, CelestialObjectType.Planet, 4498.25291049344f, 0.008606f, 1.77f, 272.8461f, 131.7806f, 260.2471f);
        var pluto = CreateCelestialObject("Pluto", 0.01314192f, 0.001186f, CelestialObjectType.Planet, 5906.37627208103f, 0.24883f, 0.29914960832f, 1.98548656f, 1.9250982f, 0f);
    }

}
