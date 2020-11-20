using Domain.Interfaces;

namespace Domain.Patterns.ChainOfResponsability
{
    public class ChainBuilder<TParam, TResult>
    {
        private IChain<TParam, TResult> _first = null;
        private IChain<TParam, TResult> _current = null;

        public ChainBuilder<TParam, TResult> Then(IChain<TParam, TResult> chain)
        {
            if (_first == null || _current == null)
            {
                _current = chain;
                _first = _current;
            }
            else
            {
                _current.Next = chain;
                _current = chain;
            }

            return this;
        }

        public IChain<TParam, TResult> Build()
        {
            return _first;
        }
    }
}
