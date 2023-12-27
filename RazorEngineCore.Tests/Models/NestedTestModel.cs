namespace RazorEngineCore.Tests.Models
{
    public class NestedTestModel
    {
        public string Name { get; set; }
        public int[] Items { get; set; }

        public class TestModelInnerClass1
        {
            public string Name { get; set; }
            public int[] Items { get; set; }

            public class TestModelInnerClass2
            {
                public string Name { get; set; }
                public int[] Items { get; set; }
            }
        }
    }
}
