﻿using System;
using System.Collections.Generic;
using System.Data;
using Phoenix.Core;
using Phoenix.Json;

namespace Phoenix.Helpers
{
    public static class Mathf
    {
        /// <summary>
        /// A method that checks if the first argument is less than the second.
        /// </summary>
        public static bool LessThan<T, R>(T one, R two)
        {
            Utils.ProtectedConstraintOnNumber(one);
            Utils.ProtectedConstraintOnNumber(two);

            return Convert.ToDouble(one) < Convert.ToDouble(two);
        }

        /// <summary>
        /// A method that checks if the first argument is greater than the second.
        /// </summary>
        public static bool GreaterThan<T, R>(T one, R two)
        {
            Utils.ProtectedConstraintOnNumber(one);
            Utils.ProtectedConstraintOnNumber(two);

            return Convert.ToDouble(one) > Convert.ToDouble(two);
        }

        /// <summary>
        /// A method that checks the equality of the first and second arguments.
        /// </summary>
        public static bool IsEqual<T>(T one, T two)
        {
            return Convert.ToString(one) == Convert.ToString(two);
        }

        /// <summary>
        /// A method that checks if the first and second arguments are not equal.
        /// </summary>
        public static bool IsNotEqual<T>(T one, T two)
        {
            return Convert.ToString(one) != Convert.ToString(two);
        }

        /// <summary>
        /// A method that checks the equality of the first and second arguments by checking the internal values.
        /// </summary>
        public static bool IsStrongEqual<T, R>(T one, R two)
        {
            return one.ToJson() == two.ToJson();
        }

        /// <summary>
        /// A method that returns an array of numbers with a specific range.
        /// </summary>
        public static int[] Range(int min, int max = 0, int step = 1)
        {
            if (step < 0)
            {
                throw new PhoenixException(
                    "The step cannot be less than zero!",
                    new ArgumentOutOfRangeException()
                );
            }

            List<int> result = new List<int>();

            for (int i = max != 0 ? min : 0; max != 0 ? i <= max : i <= min; i += step)
            {
                result.Add(i);
            }

            return result.ToArray();
        }

        /// <summary>
        /// A method that evaluates the specified expression.
        /// </summary>
        public static object Eval(string expression)
        {
            return new DataTable().Compute(expression, null);
        }

        /// <summary>
        /// A method that simulates a for loop.
        /// </summary>
        public static void For(int count, Action<int> action)
        {
            for (int i = 0; i < count; i++)
            {
                action(i);
            }
        }
    }
}
