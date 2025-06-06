﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services {
    
    internal class UnityMainThread : MonoBehaviour {
        
        internal static UnityMainThread instance;
        
        private readonly Queue<Action> jobs = new Queue<Action>();

        void Awake() {
            instance = this;
        }

        void Update() {
            while (jobs.Count > 0) 
                jobs.Dequeue().Invoke();
        }

        internal void addJob(Action newJob) {
            jobs.Enqueue(newJob);
        }
        
    }
    
}