using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public sealed class SingletonPattern
    {
        private SingletonPattern()
        {
        }

        public string name { get; set; }

        private static SingletonPattern _obj;
        private static readonly Object LockObj = new object();
        public static SingletonPattern GetInstance
        {
            get
            {
                if (_obj == null)
                    lock (LockObj)
                        if (_obj == null)
                            _obj = new SingletonPattern();
                return _obj;
            }
        }

    }
}
