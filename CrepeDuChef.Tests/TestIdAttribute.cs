namespace CrepeDuChef.Tests
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TestIdAttribute : Attribute
    {
        public string Id { get; }
        public TestIdAttribute(string id)
        {
            Id = id;
        }
    }
}
