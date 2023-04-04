using Application.Base.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class Block : IRequestHandler<BlockRequest, ResultModel>
    {
        public Task<ResultModel> Handle(BlockRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
