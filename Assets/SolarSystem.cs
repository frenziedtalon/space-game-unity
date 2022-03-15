using Assets.CelestialObjects;
using Assets.CelestialObjects.Classes;
using Assets.CelestialObjects.Factories;
using Assets.Measurements;
using System;
using System.Text;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    const string tagCelestial = "Celestial";
    readonly float G = 1f; // gravitational constant = 6.67408 x 10^-11 m3 kg-1 s-2

    GameObject[] celestials;

    private void Awake()
    {
        //Debug.Log("SolarSystem.Awake() was called");

        CreateSkyBox();
        CreateSolarSystem();

        celestials = GameObject.FindGameObjectsWithTag(tagCelestial);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("SolarSystem.Start() was called");

        InitialVelocity();
        PrintStats();
    }

    private void FixedUpdate()
    {
        //Debug.Log("SolarSystem.FixedUpdate() was called");

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

    // TODO: Move this to a factory
    void CreateSolarSystem()
    {
        var sol = CelestialObjectFactory.CreateCelestialObject(
            new PhysicalData("Sol", Mass.FromEarthMasses(332946), Distance.FromGigaMetres(0.6957), CelestialObjectType.Star),
            null
        );

        var mercury = CelestialObjectFactory.CreateCelestialObject(
            new PhysicalData("Mercury", Mass.FromEarthMasses(0.0553), Distance.FromGigaMetres(0.00244), CelestialObjectType.Planet),
            new OrbitData(Distance.FromGigaMetres(57.90917567), 0.205635, Angle.FromDegrees(7.0047), Angle.FromDegrees(29.1241), Angle.FromDegrees(48.3313), Angle.FromDegrees(168.6562))
        );

        var venus = CelestialObjectFactory.CreateCelestialObject(
            new PhysicalData("Venus", Mass.FromEarthMasses(0.8150), Distance.FromGigaMetres(0.006052), CelestialObjectType.Planet),
            new OrbitData(Distance.FromGigaMetres(108.208925506684), 0.006773, Angle.FromDegrees(3.3946), Angle.FromDegrees(54.891), Angle.FromDegrees(76.6799), Angle.FromDegrees(48.0052))
        );

        var earth = CelestialObjectFactory.CreateCelestialObject(
            new PhysicalData("Earth", Mass.FromEarthMasses(1), Distance.FromGigaMetres(0.006371), CelestialObjectType.Planet),
            new OrbitData(Distance.FromGigaMetres(149.597870691), 0.016709, Angle.FromDegrees(0), Angle.FromDegrees(282.9404), Angle.FromDegrees(0), Angle.FromDegrees(356.047))
        );

        var mars = CelestialObjectFactory.CreateCelestialObject(
            new PhysicalData("Mars", Mass.FromEarthMasses(0.107), Distance.FromGigaMetres(0.00339), CelestialObjectType.Planet),
            new OrbitData(Distance.FromGigaMetres(227.93663722813), 0.093405, Angle.FromDegrees(1.8497), Angle.FromDegrees(286.5016), Angle.FromDegrees(49.5574), Angle.FromDegrees(18.6021))
        );

        var jupiter = CelestialObjectFactory.CreateCelestialObject(
            new PhysicalData("Jupiter", Mass.FromEarthMasses(317.8), Distance.FromGigaMetres(0.069911), CelestialObjectType.Planet),
            new OrbitData(Distance.FromGigaMetres(778.412026728313), 0.048498, Angle.FromDegrees(1.303), Angle.FromDegrees(273.8777), Angle.FromDegrees(100.4542), Angle.FromDegrees(19.895))
        );

        var saturn = CelestialObjectFactory.CreateCelestialObject(
            new PhysicalData("Saturn", Mass.FromEarthMasses(95.2), Distance.FromGigaMetres(0.058232), CelestialObjectType.Planet),
            new OrbitData(Distance.FromGigaMetres(1426.72541250233), 0.055546, Angle.FromDegrees(2.4886), Angle.FromDegrees(339.3939), Angle.FromDegrees(113.6634), Angle.FromDegrees(316.967))
        );

        var uranus = CelestialObjectFactory.CreateCelestialObject(
            new PhysicalData("Uranus", Mass.FromEarthMasses(14.5), Distance.FromGigaMetres(0.025362), CelestialObjectType.Planet),
            new OrbitData(Distance.FromGigaMetres(2870.97221979699), 0.047318, Angle.FromDegrees(0.7733), Angle.FromDegrees(96.6612), Angle.FromDegrees(74.0005), Angle.FromDegrees(142.5905))
        );

        var neptune = CelestialObjectFactory.CreateCelestialObject(
            new PhysicalData("Neptune", Mass.FromEarthMasses(17.1), Distance.FromGigaMetres(0.024622), CelestialObjectType.Planet),
            new OrbitData(Distance.FromGigaMetres(4498.25291049344), 0.008606, Angle.FromDegrees(1.77), Angle.FromDegrees(272.8461), Angle.FromDegrees(131.7806), Angle.FromDegrees(260.2471))
        );

        var pluto = CelestialObjectFactory.CreateCelestialObject(
            new PhysicalData("Pluto", Mass.FromEarthMasses(0.0025), Distance.FromGigaMetres(0.001186), CelestialObjectType.Planet),
            new OrbitData(Distance.FromGigaMetres(5906.37627208103), 0.24883, Angle.FromDegrees(0.29914960832), Angle.FromDegrees(1.98548656), Angle.FromDegrees(1.9250982), Angle.FromDegrees(0))
        );
    }

    void CreateSkyBox()
    {
        var shader = Shader.Find("Skybox/6 Sided");
        var material = new Material(shader);
        material.SetTexture("_FrontTex", Resources.Load<Texture2D>("Skybox/SpaceSkybox01/skybox_pz")); // Front [+Z]
        material.SetTexture("_BackTex", Resources.Load<Texture2D>("Skybox/SpaceSkybox01/skybox_nz")); // Back [-Z]
        material.SetTexture("_LeftTex", Resources.Load<Texture2D>("Skybox/SpaceSkybox01/skybox_px")); // Left [+X] 
        material.SetTexture("_RightTex", Resources.Load<Texture2D>("Skybox/SpaceSkybox01/skybox_nx")); // Right [-X] 
        material.SetTexture("_UpTex", Resources.Load<Texture2D>("Skybox/SpaceSkybox01/skybox_py")); // Up [+Y]
        material.SetTexture("_DownTex", Resources.Load<Texture2D>("Skybox/SpaceSkybox01/skybox_ny")); // Down[-Y]

        RenderSettings.skybox = material;
    }

    void PrintStats()
    {
        StringBuilder builder = new StringBuilder();

        builder.AppendLine(String.Format("{0,-20}\t{1,10:N0}\t{2,15:N0}", "Name", "Radius", "SemiMajorAxis"));

        foreach (GameObject a in celestials)
        {
            float radius = a.transform.localScale.x;
            float semiMajorAxis = a.transform.position.x;

            builder.AppendLine(String.Format("{0,-20}\t{1,10:N0}\t\t{2,15:N0}", a.name, radius, semiMajorAxis));
        }

        Debug.Log(builder.ToString());
    }
}
