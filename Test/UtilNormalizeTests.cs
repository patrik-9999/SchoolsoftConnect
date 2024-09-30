namespace Test
{
    public class UtilNormalize13Tests
    {
        [Theory]
        [InlineData("20010101-0101", "20010101-0101")]
        [InlineData("19750101-0101", "19750101-0101")]
        [InlineData("200101010101", "20010101-0101")]
        [InlineData("196501010101", "19650101-0101")]
        [InlineData("010101-0101", "20010101-0101")]
        [InlineData("980101-0101", "19980101-0101")]
        [InlineData("0101010101", "20010101-0101")]
        [InlineData("7304011256", "19730401-1256")]
        [InlineData("", "")]
        [InlineData("Inte ett personnummer", "Inte ett personnummer")]
        public void TestNormalize13(string input, string expected)
        {
            Assert.Equal(expected, Common.Util.Normalize13(input));
        }
    }
    public class UtilNormalize12Tests
    {
        [Theory]
        [InlineData("20010101-0101", "200101010101")]
        [InlineData("19750101-0101", "197501010101")]
        [InlineData("200101010101", "200101010101")]
        [InlineData("196501010101", "196501010101")]
        [InlineData("010101-0101", "200101010101")]
        [InlineData("980101-0101", "199801010101")]
        [InlineData("0101010101", "200101010101")]
        [InlineData("7304011256", "197304011256")]
        [InlineData("", "")]
        [InlineData("Inte ett personnummer", "Inte ett personnummer")]
        public void TestNormalize12(string input, string expected)
        {
            Assert.Equal(expected, Common.Util.Normalize12(input));
        }
    }

}