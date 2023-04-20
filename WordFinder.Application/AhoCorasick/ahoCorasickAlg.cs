using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder.Application.AhoCorasick
{
    public class TrieNode
    {
        private readonly Dictionary<char, TrieNode> children = new Dictionary<char, TrieNode>();
        public string Word { get; set; }
        public bool IsWord => Word != null;

        public void Insert(string word)
        {
            var currentNode = this;
            foreach (var c in word)
            {
                if (!currentNode.children.ContainsKey(c))
                {
                    currentNode.children[c] = new TrieNode();
                }
                currentNode = currentNode.children[c];
            }
            currentNode.Word = word;
        }

        public TrieNode GetNode(char c)
        {
            if (children.ContainsKey(c))
            {
                return children[c];
            }
            return null;
        }
    }

    public class Trie
    {
        private readonly TrieNode root;

        public Trie(IEnumerable<string> words)
        {
            root = new TrieNode();
            foreach (var word in words)
            {
                root.Insert(word);
            }
        }

        public TrieNode Root => root;
    }


}
