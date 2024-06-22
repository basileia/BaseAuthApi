namespace BaseAuthApp_BAL.Models
{
    public class Result<TValue, TError>
    {
        public readonly TValue? Value;
        public readonly TError? Error;

        public readonly bool IsSuccess;

        private Result(TValue value)
        {
            IsSuccess = true;
            Value = value;
            Error = default;
        }

        private Result(TError error)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
        }

        public static implicit operator Result<TValue, TError>(TValue value) => new Result<TValue, TError>(value);

        public static implicit operator Result<TValue, TError>(TError error) => new Result<TValue, TError>(error);
    }
}
