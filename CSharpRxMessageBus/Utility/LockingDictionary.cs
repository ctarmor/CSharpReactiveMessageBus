using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRxMessageBus.Utility
{
    class LockingDictionary<TK, T>  // : IReadOnlyDictionary<TK, T>
    {
        /// <summary>
        /// Internal dictionaru
        /// </summary>
        private readonly Dictionary<TK, T> _internal = new Dictionary<TK, T>();

        /// <summary>
        /// Get Value. Pass factory if the object will be factored.
        /// </summary>
        public T Get(TK key, Func<T> factory = null)
        {
            T rtn = default(T);

            if (key == null)
                return rtn;

            lock (_internal)
                if (!_internal.TryGetValue(key, out rtn) && factory != null)
                {
                    rtn = factory();
                    if (rtn != null)
                        _internal[key] = rtn;
                }

            return rtn;
        }

        /// <summary>
        /// Set Value. Pass factory if the object will be factored.
        /// </summary>
        public T Set(TK key, T item)
        {
            T rtn;

            lock (_internal)
            {
                _internal.TryGetValue(key, out rtn);

                _internal[key] = item;

            }

            return rtn;
        }


        /// <summary>
        /// remove specific key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Null if none was removed, else removed item</returns>
        public T Remove(TK key)
        {
            T rtn;

            lock (_internal)
                if (_internal.TryGetValue(key, out rtn))
                    _internal.Remove(key);

            return rtn;
        }


        /// <summary>
        /// remove specific first key. similar to queue.
        /// </summary>
        public T Remove()
        {
            T rtn;

            lock (_internal)
            {
                if (_internal.Keys.Count == 0)
                    return default(T);

                rtn = Remove(_internal.Keys.First());
            }

            return rtn;
        }

        /// <summary>
        /// Get value IEnumerable
        /// </summary>
        public IEnumerable<T> ToEnumerable()
        {
            lock (_internal)
                return _internal.Values.ToArray();
        }

        /// <summary>
        /// Get value IEnumerable
        /// </summary>
        public IEnumerable<T> ToEnumerable(Predicate<T> filter)
        {
            lock (_internal)
                return _internal
                    .Values
                    .Where(t => filter(t))
                    .ToArray();
        }


        /// <summary>
        /// Get value IEnumerable
        /// </summary>
        public KeyValuePair<TK, T>[] ToEnumerableKeyPair()
        {
            lock (_internal)
                return _internal.ToArray();
        }

        /// <summary>
        /// Add key / value
        /// </summary>
        /// <returns>Not null: replaced value, else null</returns>
        public T Add(TK key, T value)
        {
            T rtn;

            lock (_internal)
            {
                _internal.TryGetValue(key, out rtn);
                _internal[key] = value;
            }

            return rtn;
        }


        /// <summary>
        /// Count
        /// </summary>
        public int Count
        {
            get
            {
                lock (_internal)
                    return _internal.Count;
            }
        }

        /// <summary>
        /// Is key in map ? 
        /// </summary>
        public bool ContainsKey(TK key)
        {
            lock (_internal)
                return _internal.ContainsKey(key);
        }


        /// <summary>
        /// Clear Dictionary
        /// </summary>
        /// <returns></returns>
        public void Clear()
        {
            lock (_internal)
                _internal.Clear();
        }

        ///// <summary>
        ///// ToObservable
        ///// </summary>
        //public IObservable<T> ToObservable()
        //{
        //    lock (_internal)
        //        return _internal.Values.ToObservable();
        //}

        /// <summary>
        /// Perform action while collection is locked
        /// </summary>
        public void LockAction(Action<Dictionary<TK, T>> action)
        {
            lock (_internal)
                action(_internal);
        }

    }
}
