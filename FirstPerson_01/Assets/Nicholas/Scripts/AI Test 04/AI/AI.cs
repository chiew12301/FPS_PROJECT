using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Nicholas.Scripts.AI_Test_04.AI
{
    [RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]
    public class AI : MonoBehaviour
    {
        private NavMeshAgent NavMeshAgent;
        private FiniteStateMachine fsm;

        public void Awake()
        {
            NavMeshAgent = this.GetComponent<NavMeshAgent>();
            fsm = this.GetComponent<FiniteStateMachine>();
        }

        public void Start()
        {
            
        }

        public void Update()
        {
            
        }
    }
}
