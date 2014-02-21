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
            UpdateState();
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Host != null)
                Host.Close();
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

        private void toolStripButtonPlay_Click(object sender, EventArgs e)
        {
            SpaceServer.Universe.IsRunning = toolStripButtonPlay.Checked;
        }

        private void toolStripButtonTest_Click(object sender, EventArgs e)
        {
            Universe.InternalTest();
        }

        private void toolStripButtonLoad_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != openFileDialog.ShowDialog())
                return;
            SpaceServer.Universe = Universe.Load(openFileDialog.FileName);
            if (SpaceServer.Universe == null)
                return;
            Host = new ServiceHost(typeof(SpaceServer));
            Host.Open();
            ShowNode(SpaceServer.Universe);
            UpdateState();
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            var u = SpaceServer.Universe;
            if (u == null || DialogResult.OK != saveFileDialog.ShowDialog())
                return;
            SpaceServer.Universe.Save(saveFileDialog.FileName);
        }

        private void UpdateState()
        {
            bool okay = SpaceServer.Universe != null;
            toolStripButtonPlay.Enabled = okay;
            toolStripButtonSave.Enabled = okay;
            toolStripButtonLoad.Enabled = !okay;
        }
    }
}
