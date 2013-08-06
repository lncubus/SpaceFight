using System;
using System.Reflection;
using System.Windows.Forms;

namespace Server
{
    public class TreeBuilder
    {
        readonly public TreeNodeCollection TreeNodes;

        public TreeBuilder(TreeView treeView)
        {
            TreeNodes = treeView.Nodes;
        }

        public TreeBuilder(TreeNode parentNode)
        {
            TreeNodes = parentNode.Nodes;
        }

        public TreeNode ShowThing(object thing)
        {
            if (thing == null)
                return null;
            TreeNode node = CreateTreeNode(thing, null);
            if (node != null)
                TreeNodes.Add(node);
            return node;
        }

        protected TreeNode CreateTreeNode(object thing, string text)
        {
            TreeNode node = new TreeNode();
            node.Tag = thing;
            node.Text = text ?? CreateTreeNodeText(thing);
            if (!(thing is System.Drawing.FontFamily))
                PopulateChildNodes(node);
            return node;
        }

        public static string CreateTreeNodeText(object thing)
        {
            if (thing == null)
                return null;

            string text = thing.ToString();
            Type thingType = thing.GetType();
            const string DefaultNamespace = "SF.";
            if (text.StartsWith(DefaultNamespace, StringComparison.Ordinal))
            {
                // Use plain type name instead:
                text = thingType.Name;
            }
            string name = GetPropertyValue(thing, thingType, "Name");
            if (name == "0")
                name = null;
            if (string.IsNullOrEmpty(name))
                return text;
            return name + " - " + text;
        }

        public static string GetPropertyValue(object thing, Type thingType, string propertyName)
        {
            PropertyInfo nameProperty = thingType.GetProperty(propertyName);
            if (nameProperty == null)
                return null;
            object nameValue = nameProperty.GetValue(thing, null);
            if (nameValue == null)
                return null;
            string name = nameValue.ToString();
            if (string.IsNullOrEmpty(name))
                return null;
            return name;
        }

        public void AddCollectionNodes(TreeNode parent,
            System.Collections.ICollection enumerable, string name)
        {
            if (enumerable == null)
                return;
            TreeNode enumNode = CreateTreeNode(enumerable, name);
            if (enumerable.Count > 0)
                enumNode.Text = enumNode.Text + " [" + enumerable.Count + "]";
            if (enumerable is System.Collections.IDictionary)
                InternalAddDictionaryNodes(enumNode,
                    (System.Collections.IDictionary)enumerable);
            else
                InternalAddCollectionNodes(enumNode, enumerable);
            parent.Nodes.Add(enumNode);
        }

        private void InternalAddDictionaryNodes(TreeNode enumNode,
            System.Collections.IDictionary dictionary)
        {
            foreach (System.Collections.DictionaryEntry pair in dictionary)
            {
                string name = pair.Key.ToString();
                // Check child node is collection itself too
                System.Collections.ICollection childCollection =
                    pair.Value as System.Collections.ICollection;
                if (childCollection != null)
                    AddCollectionNodes(enumNode, childCollection, name);
                else
                {
                    TreeNode childNode = CreateTreeNode(pair.Value, name);
                    if (childNode == null)
                        continue;
                    enumNode.Nodes.Add(childNode);
                }
            }
        }

        protected void InternalAddCollectionNodes(TreeNode enumNode,
            System.Collections.ICollection enumerable)
        {
            foreach (object child in enumerable)
            {
                TreeNode childNode = CreateTreeNode(child, null);
                if (childNode == null)
                    continue;
                enumNode.Nodes.Add(childNode);
                // Check child node is collection itself too
                System.Collections.ICollection childCollection = child as System.Collections.ICollection;
                if (childCollection != null)
                    AddCollectionNodes(childNode, childCollection, null);
            }
        }

        public void PopulateChildNodes(TreeNode parent)
        {
            parent.Nodes.Clear();
            if (parent.Tag == null)
                return;
            object instance = parent.Tag;
            if (instance is string || instance is Type)
                return;
            PropertyInfo[] properties = instance.GetType().GetProperties();
            FieldInfo[] fields = instance.GetType().GetFields();
            if (properties.Length > 0)
                foreach (PropertyInfo property in properties)
                    ShowProperty(parent, instance, property);
            if (fields.Length > 0)
                foreach (FieldInfo field in fields)
                    ShowField(parent, instance, field);
        }

        protected void ShowProperty(TreeNode parent, object instance, PropertyInfo property)
        {
            if (property.GetIndexParameters().Length > 0 || property.Name == "SyncRoot")
                return;
            if (instance is System.Collections.IDictionary &&
                (property.Name == "Comparer" || property.Name == "Keys" || property.Name == "Values"))
                return;
            object value = null;
            try
            {
                value = property.GetValue(instance, null);
            }
            catch
            {
                // Ignore inner exceptions in property code
            }
            ShowMember(parent, property.Name, value);
        }

        protected void ShowField(TreeNode parent, object instance, FieldInfo field)
        {
            object value = null;
            try
            {
                value = field.GetValue(instance);
            }
            catch
            {
                // Ignore inner exceptions in property code
            }
            ShowMember(parent, field.Name, value);
        }

        protected void ShowMember(TreeNode parent, string name, object value)
        {
            if (value == null)
                return;
            // Don't add nodes for primitive properties and arrays
            Type valueType = value.GetType();
            if (valueType.IsValueType || value is string)
                return;
            System.Collections.ICollection valueCollection = value as System.Collections.ICollection;
            if (valueCollection != null)
                AddCollectionNodes(parent, valueCollection, name);
            else if (valueType.IsClass)
            {
                TreeNode elemNode = CreateTreeNode(value, name);
                if (elemNode != null)
                    parent.Nodes.Add(elemNode);
            }
        }
    }
}
