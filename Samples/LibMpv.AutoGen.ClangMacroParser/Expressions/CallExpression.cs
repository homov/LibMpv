using System.Collections.Generic;
using System.Linq;

namespace LibMpv.AutoGen.ClangMacroParser.Expressions
{
    public class CallExpression : IExpression
    {
        public CallExpression(string name, IEnumerable<IExpression> args)
        {
            Name = name;
            Arguments = args.ToArray();
        }

        public IReadOnlyCollection<IExpression> Arguments { get; }

        public string Name { get; }
    }
}
