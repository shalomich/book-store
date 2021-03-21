using System;
using System.Collections.Generic;
using System.Text;
using Storage.Services;
using Xunit;

namespace Storage.Test
{/*
    public class EntityToFormConverterTest
    {
        private EntityToFormConverter _converter = new EntityToFormConverter();
        
        class ConverterTester
        {
            public string Email { set; get; }
            public int Age { set; get; }       
        }
        
        class UnvalidTester : ConverterTester
        {
            public float Money { set; get; }
        }

        [Fact]
        public void UsualConvert()
        {
             var expected = new Dictionary<string, string>()
            {
                {"Email", "text"},
                {"Age", "number" }
            };
            Assert.Equal(_converter.Convert<ConverterTester>(), expected);            
        }

        [Fact]
        public void ConvertWithConfiguration()
        {
            var expected = new Dictionary<string, string>()
            {
                {"Email", "email"},
                {"Age", "number" }
            };

            var propertieToFormElements = new Dictionary<string, string>()
            {
                {"Email", "email"}
            };

            var actual = _converter.Convert<ConverterTester>(propertieToFormElements);
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void ConvertWithUnvalidConfig()
        {
            var expected = new Dictionary<string, string>()
            {
                {"Email", "text"},
                {"Age", "number" }
            };

            var propertieToFormElements = new Dictionary<string, string>()
            {
                {"Email", "boolshit"}
            };

            var actual = _converter.Convert<ConverterTester>(propertieToFormElements);
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void ConvertWithUnvalidFieldType()
        {
            var expected = new Dictionary<string, string>()
            {
                {"Email", "email"},
                {"Age", "number" }
            };

            var propertieToFormElements = new Dictionary<string, string>()
            {
                {"Email", "email" },
                {"Money", "number"}
            };

            var actual = _converter.Convert<UnvalidTester>(propertieToFormElements);
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void ConvertWithUnvalidFieldName()
        {
            var expected = new Dictionary<string, string>()
            {
                {"Email", "email"},
                {"Age", "number" }
            };

            var propertieToFormElements = new Dictionary<string, string>()
            {
                {"Email", "email" },
                {"boolshit", "number"}
            };

            var actual = _converter.Convert<UnvalidTester>(propertieToFormElements);
            Assert.Equal(actual, expected);
        }

    }*/
}
