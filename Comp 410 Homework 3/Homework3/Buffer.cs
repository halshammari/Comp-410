using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace Homework3
{
    class Buffer<T>
    {
        
        private static int BUFFER_SIZE = 10000;       // The buffer array size
       
        private int inPos = 0;   

        private int outPos = 0;  

        private int count = 0;                      //current items in the buffer

        private Object[] buffer = new Object[BUFFER_SIZE];                  //This is the Array

       // private Semaphore mutex = new Semaphore(1, 1);                  // Mutual Exclusion

        private SpinLock _lock = new SpinLock();    
                       
        private Semaphore empty = new Semaphore(BUFFER_SIZE, BUFFER_SIZE);     // Monitoring the empty elements numbers in the array.

        private Semaphore full = new Semaphore(0, BUFFER_SIZE);               //Monitoring the filled elements numbers in the array.

        public void insert(Object item)
        {

               empty.WaitOne(); 
               var taken = false;

              _lock.Enter(ref taken);
               //mutex.WaitOne();                        //Mutual Exclusion
            
               ++count;
               buffer[inPos] = item;
               inPos = (inPos + 1) % BUFFER_SIZE;
               _lock.Exit();

              
              //mutex.Release();                     //Mutual Exclusion
               full.Release(1);
        }

       public long remove()
        {
            Object item = null;

                full.WaitOne(); 
                //If buffer is empty,consumer should stops.
               var taken = false;


               _lock.Enter(ref taken);
               //mutex.WaitOne();                //Mutual Exclusion
               
               --count;
               item = buffer[outPos];
               outPos = (outPos + 1) % BUFFER_SIZE;
               _lock.Exit();



               //mutex.Release();               //Mutual Exclusion
               empty.Release(1);       	  
               //If buffer was full, the Producer should stars.
               return (long)item;
        }

        public int Size()
        {
            return buffer.Length;
        }

    }
}