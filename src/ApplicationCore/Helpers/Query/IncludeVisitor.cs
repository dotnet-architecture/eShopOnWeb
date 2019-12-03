using System.Linq.Expressions;

namespace Microsoft.eShopWeb.ApplicationCore.Helpers.Query
{
    public class IncludeVisitor : ExpressionVisitor
    {
        public string Path { get; private set; } = string.Empty;

        protected override Expression VisitMember(MemberExpression node)
        {
            Path = string.IsNullOrEmpty(Path) ? node.Member.Name : $"{Path}.{node.Member.Name}";

            return base.VisitMember(node);
        }
    }
}
