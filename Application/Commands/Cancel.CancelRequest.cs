using Application.Base.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CancelRequest : BaseCommandViewModel<ResultModel>
    {
    }
}
