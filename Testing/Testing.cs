using System;
using System.Collections.Generic;
using Phoenix.Helpers;
using Phoenix.Json;

namespace Phoenix.Testing
{
    internal class TestableException : Exception
    {
        public TestableException(string messageError) : base(messageError) { }
    }

    public class Testing
    {
        private static List<Tuple<string, Action>> _describers = new List<Tuple<string, Action>>();
        private static List<Tuple<string, Action>> _tests = new List<Tuple<string, Action>>();

        protected TestMatchers<T> Expect<T>(T value)
        {
            return new TestMatchers<T>(value);
        }

        protected void Describe(string title, Action callback)
        {
            Tuple<string, Action> tuple = new Tuple<string, Action>(title, callback);
            _describers.Add(tuple);
        }

        private void _Test(string description, Action callback)
        {
            Tuple<string, Action> tuple = new Tuple<string, Action>(description, callback);
            _tests.Add(tuple);
        }

        protected void Test(string description, Action callback)
        {
            _Test(description, callback);
        }

        protected void It(string description, Action callback)
        {
            _Test(description, callback);
        }

        protected void RunTesting()
        {
            foreach (Tuple<string, Action> describe in _describers)
            {
                string separator = "-".PadLeft(describe.Item1.Length);

                Debugger.Log(separator);
                Debugger.Log($@"{describe.Item1}");
                Debugger.Log(separator);

                describe.Item2();

                foreach (Tuple<string, Action> test in _tests)
                {
                    try
                    {
                        test.Item2();
                        Debugger.Log($@"[SUCCESS] {test.Item1}");
                    }
                    catch (Exception)
                    {
                        Debugger.Log($@"[ERROR] {test.Item1}");
                    }
                }
            }
        }

        protected class TestMatchers<T>
        {
            private T _value;
            private const string MESSAGE_ERROR_BOOLEAN = "The validation value must be Boolean!";
            private const string MESSAGE_ERROR_INT = "The validation value must be Int!";
            private const string MESSAGE_ERROR = "Test failed!";

            public TestMatchers(T value)
            {
                _value = value;
            }

            private TestMatchers<T> Equal(bool expression, string messageError = MESSAGE_ERROR)
            {
                if (!expression)
                {
                    throw new TestableException(messageError);
                }
                return this;
            }

            public TestMatchers<T> ToBe<K>(K value)
            {
                try
                {
                    return Equal(_value.ToString() == value.ToString());
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                {
                    throw new TestableException(MESSAGE_ERROR);
                }
            }

            public TestMatchers<T> ToBeEqual<K>(K value)
            {
                try
                {
                    return Equal(_value.ToJson() == value.ToJson());
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                {
                    throw new TestableException(MESSAGE_ERROR);
                }
            }

            public TestMatchers<T> ToBeTruthy()
            {
                try
                {
                    return Equal(Convert.ToBoolean(_value) != false);
                }
                catch (Exception)
                {
                    throw new InvalidCastException(MESSAGE_ERROR_BOOLEAN);
                }
            }

            public TestMatchers<T> ToBeFalsy()
            {
                try
                {
                    return Equal(Convert.ToBoolean(_value) != true);
                }
                catch (Exception)
                {
                    throw new InvalidCastException(MESSAGE_ERROR_BOOLEAN);
                }
            }


            public TestMatchers<T> ToBeNull()
            {
                try
                {
                    return Equal(TypeMatchers.IsNull(_value));
                }
                catch (Exception)
                {
                    throw new TestableException(MESSAGE_ERROR);
                }
            }

            public TestMatchers<T> ToBeLessThan(int value)
            {
                try
                {
                    return Equal(Convert.ToDouble(_value) < value);
                }
                catch (Exception)
                {
                    throw new InvalidCastException(MESSAGE_ERROR_INT);
                }
            }
        }
    }
}
