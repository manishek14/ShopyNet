using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Query
{
    public interface IBaseQuery<TResponse> : IRequest<TResponse> where TResponse : class
    {

    }
}
