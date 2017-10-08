using Quotes.GraphQL.Tree;

namespace Quotes.GraphQL.Parsers
{
    public interface IGraphQLParser
    {
        string Parse(string queryType, string queryName, string[] subSelection, object @object = null,
            string objectTypeName = null);

        string ParseTree(TreeNode node);
    }
}