using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

using SF.ServerLibrary;

namespace Server
{

    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        private ServiceHost Host { get; set; }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            this.Host = new ServiceHost(typeof(SpaceServer));
            this.Host.Open();
            if (SpaceServer.Instance == null)
                Close();
            else
            {
                this.ShowNode(SpaceServer.Instance);
            }
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Host != null)
                this.Host.Close();
        }

        private TreeNode ShowNode(object thing, TreeNode parentNode = null)
        {
            treeView.BeginUpdate();
            TreeBuilder builder;
            if (parentNode == null)
                builder = new TreeBuilder(treeView);
            else
                builder = new TreeBuilder(parentNode);
            TreeNode node = builder.ShowThing(thing);
            if (node != null && thing is System.Collections.ICollection)
                builder.AddCollectionNodes(node, (System.Collections.ICollection)thing, string.Empty);
            if (node != null)
                node.Expand();
            treeView.EndUpdate();
            if (treeView.SelectedNode != null)
                treeView.SelectedNode.EnsureVisible();
            return node;
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node == null || e.Node.Tag == null)
                return;
            propertyGrid.SelectedObject = node.Tag;
        }
    }
}
