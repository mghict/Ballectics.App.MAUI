namespace Ballectics.App.Models
{
    public class ResultModel
    {
        public string Error { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = false;

        public void SetError(string error)
        {
            IsSuccess = false;
            Error = error;
        }
        public void SetSuccess()
        {
            IsSuccess = true;
            Error = string.Empty;
        }

        public static ResultModel Fail(List<ApiError> errors)
        {
            return new ResultModel()
            {
                IsSuccess = false,
                Error = string.Join(" | ", errors.Select(e => e.Message))
            };
        }
        public static ResultModel Success()
        {

            return new ResultModel()
            {
                IsSuccess = true,
                Error = string.Empty
            };
        }
        public ResultModel()
        {
        }

        public ResultModel(bool isSuccess, string error)
        {
            if (isSuccess)
            {
                SetSuccess();
                return;
            }

            SetError(error);
        }

    }

    public class ResultModel<T> : ResultModel
        where T : class
    {
        public T? Value { get; set; }

        public ResultModel() : base()
        {
            Value = default(T);
        }

        public ResultModel(bool isSuccess, string error) : base(isSuccess, error)
        {
            Value = default(T);
        }

        public ResultModel(bool isSuccess, string error, T value) : base(isSuccess, error)
        {
            Value = value;
        }
    }
}
