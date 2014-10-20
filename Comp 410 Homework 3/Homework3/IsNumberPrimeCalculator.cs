using System;
using System.Collections.Generic;
using System.Threading;


namespace Homework3 {
    internal class IsNumberPrimeCalculator
    {
        private readonly ICollection<long> _primeNumbers;
        //private readonly Queue<long> _numbersToCheck;
        private readonly BoundBuffer<long> _numbersToCheck; // Change here
        //private readonly Buffer<long> _numbersToCheck;
        public IsNumberPrimeCalculator(ICollection<long> primeNumbers, BoundBuffer<long> numbersToCheck)
        { //Change here
            _primeNumbers = primeNumbers;
            _numbersToCheck = numbersToCheck;
        }

        public void CheckIfNumbersArePrime()
        {
            while (true)
            {
                //var numberToCheck = _numbersToCheck.remove();
                var numberToCheck = _numbersToCheck.Dequeue(); //change here
                if (IsNumberPrime(numberToCheck))
                {
                    _primeNumbers.Add(numberToCheck);
                }
            }

        }

        private bool IsNumberPrime(long numberWeAreChecking)
        {
           
            
              /*
              const long firstNumberToCheck = 3;

              if (numberWeAreChecking % 2 == 0) {
                return false;
              }
              var lastNumberToCheck = Math.Sqrt(numberWeAreChecking);
              for (var currentDivisor = firstNumberToCheck; currentDivisor < lastNumberToCheck; currentDivisor += 2) {
                if (numberWeAreChecking % currentDivisor == 0) {
                    return false;
                }
              }
               return true;   */



            if (numberWeAreChecking <= 3) 
            {
                return numberWeAreChecking > 1;
            } 
            else if (numberWeAreChecking % 2 == 0 || numberWeAreChecking % 3 == 0) 
            {
                return false;
            } 
            else
            {
                for (int i = 5; i * i <= numberWeAreChecking; i += 6)
                {
                    if (numberWeAreChecking % i == 0 || numberWeAreChecking % (i + 2) == 0) 
                        {
                            return false;
                        }
                }
                return true;
            }
        
        }
    }
}