
using System;
using System.Collections;
using System.Web;

namespace BeatDave.Web.Infrastructure
{
    public static class ShortConversation
    {
        // Static Variables
        private static readonly DataStore _dataStore = new DataStore();

               
        // Public Static Properties
        public static DataStore Data { get { return _dataStore; } }


        // C'tor
        static ShortConversation()
        { }        


        // Inner Classes
        public class DataStore
        {
            // Instance Variables
            [ThreadStatic]
            private Hashtable _threadLocalData;


            // Private Properties        
            private Hashtable WebHashtable
            {
                get
                {
                    var key = "__ShortDataStore.WebHashtable.Key__";

                    var currentTable = HttpContext.Current.Items[key] as Hashtable;

                    if (currentTable == null)
                    {
                        currentTable = new Hashtable();
                        HttpContext.Current.Items[key] = currentTable;
                    }

                    return currentTable;
                }
            }

            private Hashtable ThreadLocalHashtable
            {
                get
                {
                    if (_threadLocalData == null)
                        _threadLocalData = new Hashtable();

                    return _threadLocalData;
                }
            }

            private Hashtable Data
            {
                get
                {
                    if (HttpContext.Current != null)
                        return this.WebHashtable;

                    return ThreadLocalHashtable;
                }
            }


            // Public Indexer
            public object this[object key]
            {
                get { return Data[key]; }
                set { Data[key] = value; }
            }
        }
    }
}