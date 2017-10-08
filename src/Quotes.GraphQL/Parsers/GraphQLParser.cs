using Quotes.GraphQL.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quotes.GraphQL.Parsers
{
    public class GraphQLParser : IGraphQLParser
    {
        public string Parse(string queryType, string queryName, string[] subSelection, object @object = null,
            string objectTypeName = null)
        {
            var query = queryType + "{" + queryName;

            if (@object != null)
            {
                query += "(";

                if (objectTypeName != null)
                {
                    query += objectTypeName + ":" + "{";
                }

                var queryData = string.Empty;
                foreach (var propertyInfo in @object.GetType().GetProperties())
                {
                    var value = propertyInfo.GetValue(@object);
                    if (value == null) continue;
                    var type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                    var valueQuotes = type == typeof(string) ? "\"" : string.Empty;

                    var queryPart = char.ToLowerInvariant(propertyInfo.Name[0]) + propertyInfo.Name.Substring(1) +
                                    ":" + valueQuotes + value + valueQuotes;
                    queryData += queryData.Length > 0 ? "," + queryPart : queryPart;
                }
                query += (objectTypeName != null ? queryData + "}" : queryData) + ")";
            }

            if (subSelection.Length > 0)
            {
                query += subSelection.Aggregate("{", (current, s) => current + (current.Length > 1 ? "," + s : s)) +
                         "}";
            }

            query += "}";

            return query;
        }

        /// <summary>
        /// Parse tree from root
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string ParseTree(TreeNode node)
        {
            return $"{{{ParseNode(node)}}}";
        }

        /// <summary>
        /// Parse node and if and his children if it has some
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string ParseNode(TreeNode node)
        {
            var stringBuilder = new StringBuilder(node.Value);

            return node.Childrens.Count <= 0
                ? stringBuilder.ToString()
                : stringBuilder.Append($"{{{ParseChildrens(node.Childrens)}}}").ToString();
        }

        /// <summary>
        /// For every children recursively call ParseNode
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private string ParseChildrens(IList<TreeNode> nodes)
        {
            var stringBuilder = new StringBuilder();
            foreach (var children in nodes)
            {
                stringBuilder.Append(ParseNode(children));
                AppendCommanIfNeeded(nodes, children, stringBuilder);
            }
            return stringBuilder.ToString();
        }

        private static void AppendCommanIfNeeded(IList<TreeNode> nodes, TreeNode children, StringBuilder stringBuilder)
        {
            if (!children.Equals(nodes.Last()))
            {
                stringBuilder.Append(",");
            }
        }
    }
}