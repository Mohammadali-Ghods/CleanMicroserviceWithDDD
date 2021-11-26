using FluentValidation.Results;

namespace Application.ViewModel
{
    public class ResultModel
    {
        public CustomerViewModel SucceedResult { get; set; }
        public ValidationResult FailedResults { get; set; }
    }
}
