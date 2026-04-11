using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared
{
    public class Result
    {
        protected readonly List<Error> _errors = [];

        public bool IsSuccess => _errors.Count() == 0;
        public bool IsFailure => !IsSuccess;

        public IReadOnlyList<Error> Errors => _errors;//public property to access error

        protected Result()
        {

        }

        protected Result(Error error)
        {
            _errors.Add(error);
        }

        protected Result(List<Error> errors)
        {
            _errors.AddRange(errors);
        }

        public static Result ok()
        {
            return new Result();
        }

        public static Result Fail(Error error)
        {
            return new Result(error);
        }

        public static Result Fail(List<Error> errors)
        {
            return new Result(errors);
        }



        


    }


    public class Result<TValue> : Result
    {
        private readonly TValue _Value;

        public TValue Value => IsSuccess ? _Value : throw new InvalidOperationException("can not access this value of false");


        private Result(TValue value) : base()
        {
            _Value = value;
        }

        private Result(Error error) : base(error)
        {
            _Value = default!;
        }
        private Result(List<Error> errors) : base(errors)
        {
            _Value = default!;
        }


        public static Result<TValue> Ok(TValue value) => new Result<TValue>(value);
        public static new Result<TValue> Fail(Error error) => new Result<TValue>(error);
        public static new Result<TValue> Fail(List<Error> errors) => new Result<TValue>(errors);
    }
}
