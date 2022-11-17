using System;
namespace CachingSamplesApi.Service
{
	public class DatabaseService
	{
        public Dictionary<string, object> Data { get; set; }

        public DatabaseService()
        {
            Data = new Dictionary<string, object>();
        }
    }
}

