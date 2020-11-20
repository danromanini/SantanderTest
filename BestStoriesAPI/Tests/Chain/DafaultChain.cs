using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Chain
{
    public class DafaultChain<TParam, TResult> : IChain<TParam, TResult>
    {
        private readonly TResult _result;

        public IChain<TParam, TResult> Next { get; set; }

        public DafaultChain(TResult result) => _result = result;

        public Task<TResult> Execute(TParam chainParam) => Task.FromResult(_result);
    }
}
