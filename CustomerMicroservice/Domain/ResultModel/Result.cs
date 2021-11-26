using Domain.Models;
using FluentValidation.Results;

namespace Domain.ResultModel
{
    public class Result
    {
        public Customer SucceedResult { get; set; }
        public  ValidationResult FailedResults { get; set; }
    }

  
}
