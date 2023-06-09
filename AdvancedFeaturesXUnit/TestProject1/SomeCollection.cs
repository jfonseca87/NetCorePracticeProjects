using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestProject1
{
    [CollectionDefinition("SomeCollection")]
    public class SomeCollection : ICollectionFixture<SomeFixture>
    {
    }
}
