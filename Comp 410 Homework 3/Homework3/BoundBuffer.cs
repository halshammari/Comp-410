using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Homework3
{
    class BoundBuffer<T>
    {
        private Queue<T> _queue = new Queue<T>();
	    //private Mutex _lock = new Mutex();
        private SpinLock _lock = new SpinLock();
	    private Semaphore _full = new Semaphore(0,10);
	    private Semaphore _empty = new Semaphore(10,10);

	    public void Enqueue(T item)
	    {
		    _empty.WaitOne();
		   // _lock.WaitOne();
            var taken = false;
            _lock.Enter(ref taken);
		    _queue.Enqueue(item);
		    _lock.Exit();
		    _full.Release();
	    }

	    public T Dequeue()
	    {
		    T value;
		    _full.WaitOne();
            var taken = false;
		    _lock.Enter(ref taken);
		    value = _queue.Dequeue();
		    _lock.Exit();
		    _empty.Release();
		    return value;

	    }

        public int Size()
        {
            return _queue.Count();
        }
    }
}
