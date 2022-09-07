using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClient.Model
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }

        public bool IsFailure => !IsSuccess;

        protected Result(bool success, string error)
        {
            IsSuccess = success;
            Error = error;
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }

        public static Result Ok()
        {
            return new Result(true, String.Empty);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, String.Empty);
        }
    }


    public class Result<T> : Result
    {
        public T Value { get; private set; }

        protected internal Result(T value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }
    }
}
