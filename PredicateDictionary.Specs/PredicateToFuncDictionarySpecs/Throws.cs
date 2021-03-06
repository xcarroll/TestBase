using System.Collections.Generic;
using Xunit;

namespace PredicateDictionary.Specs.PredicateToFuncDictionarySpecs
{
    public class Throws
    {
        [Fact]
        public void GivenAKeyNotSatisfyingAnyPredicates()
        {
            var uut = new PredicateToFuncDictionary<string, int>();

            Assert.Throws<KeyNotFoundException>(() => uut[""]);
            Assert.Throws<KeyNotFoundException>(() => uut[s => true]);
        }
    }
}
