﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ClassesForExercise
{
    public class Battery
    {
        const int MAX_CAPACITY = 1000;
        private static Random r = new Random();
        //Add events to the class to notify upon threshhold reached and shut down!
        #region events
        #endregion
        public delegate void ReachThreshold(Object sender, int percent);
        public event ReachThreshold OnThreshold;
        public delegate void ShutDown(Object sender);
        public event ShutDown OnShutDown;
         private int Threshold { get; }
        public int Capacity { get; set; }
        public int Percent
        {
            get
            {
                return 100 * Capacity / MAX_CAPACITY;
            }
        }
        public Battery()
        {
            Capacity = MAX_CAPACITY;
            Threshold = 400;
        }

        public void Usage()
        {
            Capacity -= r.Next(50, 150);
            //Add calls to the events based on the capacity and threshhold
            #region Fire Events
            if (Capacity < Threshold)
            {
                if (OnThreshold != null)
                    OnThreshold(this, Percent);
            }
            #endregion
             if (Capacity <= 0)
            {
                if(OnShutDown != null)
                    OnShutDown(this);
                
            }
           

        }

    }

    class ElectricCar
    {
        public Battery Bat { get; set; }
        private int id;

        //Add event to notify when the car is shut down
        public event Action OnCarShutDown;
        
        

        public ElectricCar(int id)
        {
            this.id = id;
            Bat = new Battery();
            #region Register to battery events
            #endregion
            Bat.OnThreshold += (sender, pecent) => Console.WriteLine($"Battery reached threshold {pecent}");
            Bat.OnShutDown += (sender) =>
            {
                Console.WriteLine(" Battery reached  to Zero , car is shutting down ");
                if (OnCarShutDown != null)
                    OnCarShutDown();
            };
        }
        public void StartEngine()
        {
            while (Bat.Capacity > 0)
            {
                Console.WriteLine($"{this} {Bat.Percent}% Thread: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
                Bat.Usage();
            }
        }

        //Add code to Define and implement the battery event implementations
        #region events implementation
        #endregion

        public override string ToString()
        {
            return $"Car: {id}";
        }

    }

}
