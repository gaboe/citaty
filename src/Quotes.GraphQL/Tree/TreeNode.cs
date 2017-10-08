using System.Collections.Generic;

namespace Quotes.GraphQL.Tree
{
    public class TreeNode
    {
        public string Value { get; set; }
        public IList<TreeNode> Childrens { get; set; } = new List<TreeNode>();
    }
}