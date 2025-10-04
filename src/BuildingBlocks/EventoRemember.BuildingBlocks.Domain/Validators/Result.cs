namespace EventoRemember.BuildingBlocks.Domain.Validators
{
    public class Result<T>
    {
        private Result(T? value, bool isSuccess, IReadOnlyCollection<string> errors)
        {
            Value = value;
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result<T> Success(T value) 
            => new Result<T>(value, true, new List<string>());

        public static Result<T> Failure(IEnumerable<string> errors) 
            => new Result<T>(default, false, errors.ToList()); 

        public T? Value { get; }
        public bool IsSuccess { get; }
        public IReadOnlyCollection<string> Errors { get; } = new List<string>();
    }

    public class Result
    {
        private Result(bool isSuccess, IReadOnlyCollection<string> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Success()
            => new Result(true, new List<string>());

        public static Result Failure(IEnumerable<string> errors)
            => new Result(false, errors.ToList());

        public bool IsSuccess { get; }
        public IReadOnlyCollection<string> Errors { get; } = new List<string>();
    }
}
