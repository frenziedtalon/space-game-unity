using System;
using UnityEngine;

namespace Assets.Entities
{
    public abstract class EntityBase : MonoBehaviour
    {
        public Guid Id { get; }

        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        // Awake is called when the class is instantiated or before the first frame on Startup
        public virtual void Awake()
        {

        }

        // Start is called before the first frame update
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }

        // FixedUpdate can be called multiple times per frame
        // dependent on the physics engine
        public virtual void FixedUpdate()
        {

        }
    }
}
