using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Base.ViewModels
{
    public class ResultModel
    {
        public IDModel? ID { get; set; }
        public ValidationResult? FailedResults { get; set; }
    }
    public class IDModel
    {
        public string? ID { get; set; }
    }
}
