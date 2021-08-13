using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using QueryWorker;
using QueryWorker.Configurations;
using QueryWorker.Tests.Tools;
using Xunit;

namespace QueryWorker.Tests
{
    public class ConfigurationFinderTests
    {
        private ConfigurationFinder _finder =
            new ConfigurationFinder(typeof(ConfigurationFinderTests).Assembly);

        [Fact]
        public void FindConfiguration()
        {
            QueryNodeConfiguration<Product> c = _finder.Find<Product, SortingConfiguration<Product>>();
            Assert.NotNull(c);
        }
    }
}
