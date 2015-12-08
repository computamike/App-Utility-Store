using Moq;
using System.Collections.Generic;
using System.Data.Entity;

namespace TestAPI.Moqs
{
    public class MockedDbContext<T> : Mock<T> where T : DbContext
    {
        public Dictionary<string, object> Tables
        {
            get { return _Tables ?? (_Tables = new Dictionary<string, object>()); }
        } private Dictionary<string, object> _Tables;
    }
}
