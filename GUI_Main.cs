using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenPop.Mime;
using OpenPop.Mime.Header;
using OpenPop.Mime.Traverse;
using OpenPop.Pop3;
using OpenPop.Pop3.Exceptions;
using OpenPop.Common.Logging;
using Message = OpenPop.Mime.Message;


namespace MailClient
{
    public partial class GUI_Main : Form
    {
        private readonly Pop3Client pop3Client;
        private readonly Dictionary<int, Message> messages = new Dictionary<int, Message>();

        public GUI_Main()
        {
            InitializeComponent();
            pop3Client = new Pop3Client();
        }

        private void newMailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI_MailCreate MailForm = new GUI_MailCreate();
            MailForm.Show();
        }

        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GUI_Option OptionForm = new GUI_Option();
            OptionForm.Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tbstNew_Click(object sender, EventArgs e)
        {
            GUI_MailCreate MailForm = new GUI_MailCreate();
            MailForm.Show();
        }

        private void tbstGet1_Click_1(object sender, EventArgs e)
        {
            ReceiveMails();
        }

        private void ReceiveMails()
        {
            // Disable buttons while working
            tsbtNew.Enabled = false;
            tsbtGet.Enabled = false;
            progressBar1.Value = 0;

            try
            {
                if (pop3Client.Connected)
                    pop3Client.Disconnect();
                pop3Client.Connect(Properties.Settings.Default.popHost, Properties.Settings.Default.popPort, Properties.Settings.Default.popUseSSL);
                pop3Client.Authenticate(Properties.Settings.Default.popUsername, Properties.Settings.Default.popPassword);
                int count = pop3Client.GetMessageCount();
                totalMessages1.Text =  "Total Messages: " + count.ToString();
                messageTextBox.Text = "";
                listMessages.Nodes.Clear();

                int success = 0;
                int fail = 0;
                for (int i = count; i >= 1; i -= 1)
                {
                    // Check if the form is closed while we are working. If so, abort
                    if (IsDisposed)
                        return;

                    // Refresh the form while fetching emails
                    // This will fix the "Application is not responding" problem
                    Application.DoEvents();

                    try
                    {
                        Message message = pop3Client.GetMessage(i);

                        // Add the message to the dictionary from the messageNumber to the Message
                        messages.Add(i, message);

                        // Create a TreeNode tree that mimics the Message hierarchy
                        TreeNode node = new TreeNodeBuilder().VisitMessage(message);

                        // Set the Tag property to the messageNumber
                        // We can use this to find the Message again later
                        node.Tag = i;

                        // Show the built node in our list of messages
                        listMessages.Nodes.Add(node);

                        success++;
                    }
                    catch (Exception e)
                    {
                        DefaultLogger.Log.LogError(
                            "TestForm: Message fetching failed: " + e.Message + "\r\n" +
                            "Stack trace:\r\n" +
                            e.StackTrace);
                        fail++;
                    }

                    progressBar1.Value = (int)(((double)(count - i) / count) * 100);
                }

                MessageBox.Show(this, "Mail received!\nSuccesses: " + success + "\nFailed: " + fail, "Message fetching done");

                if (fail > 0)
                {
                    MessageBox.Show(this,
                                    "Since some of the emails were not parsed correctly (exceptions were thrown)\r\n" +
                                    "please consider sending your log file to the developer for fixing.\r\n" +
                                    "If you are able to include any extra information, please do so.",
                                    "Help improve OpenPop!");
                }
            }
            catch (InvalidLoginException)
            {
                MessageBox.Show(this, "The server did not accept the user credentials!", "POP3 Server Authentication");
            }
            catch (PopServerNotFoundException)
            {
                MessageBox.Show(this, "The server could not be found", "POP3 Retrieval");
            }
            catch (PopServerLockedException)
            {
                MessageBox.Show(this, "The mailbox is locked. It might be in use or under maintenance. Are you connected elsewhere?", "POP3 Account Locked");
            }
            catch (LoginDelayException)
            {
                MessageBox.Show(this, "Login not allowed. Server enforces delay between logins. Have you connected recently?", "POP3 Account Login Delay");
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "Error occurred retrieving mail. " + e.Message, "POP3 Retrieval");
            }
            finally
            {
                // Enable the buttons again
                tsbtNew.Enabled = true;
                tsbtGet.Enabled = true;
                pop3Client.Disconnect();
                progressBar1.Value = 100;
            }
        }

        private void ListMessagesMessageSelected(object sender, TreeViewEventArgs e)
        {
            // Fetch out the selected message
            Message message = messages[GetMessageNumberFromSelectedNode(listMessages.SelectedNode)];

            // If the selected node contains a MessagePart and we can display the contents - display them
            if (listMessages.SelectedNode.Tag is MessagePart)
            {
                MessagePart selectedMessagePart = (MessagePart)listMessages.SelectedNode.Tag;
                if (selectedMessagePart.IsText)
                {
                    // We can show text MessageParts
                    messageTextBox.Text = selectedMessagePart.GetBodyAsText();
                }
                else
                {
                    // We are not able to show non-text MessageParts (MultiPart messages, images, pdf's ...)
                    messageTextBox.Text = "<<OpenPop>> Cannot show this part of the email. It is not text <<OpenPop>>";
                }
            }
            else
            {
                // If the selected node is not a subnode and therefore does not
                // have a MessagePart in it's Tag property, we genericly find some content to show

                // Find the first text/plain version
                MessagePart plainTextPart = message.FindFirstPlainTextVersion();
                if (plainTextPart != null)
                {
                    // The message had a text/plain version - show that one
                    messageTextBox.Text = plainTextPart.GetBodyAsText();
                }
                else
                {
                    // Try to find a body to show in some of the other text versions
                    List<MessagePart> textVersions = message.FindAllTextVersions();
                    if (textVersions.Count >= 1)
                        messageTextBox.Text = textVersions[0].GetBodyAsText();
                    else
                        messageTextBox.Text = "<<OpenPop>> Cannot find a text version body in this message to show <<OpenPop>>";
                }
            }
        }

        private static int GetMessageNumberFromSelectedNode(TreeNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            // Check if we are at the root, by seeing if it has the Tag property set to an int
            if (node.Tag is int)
            {
                return (int)node.Tag;
            }

            // Otherwise we are not at the root, move up the tree
            return GetMessageNumberFromSelectedNode(node.Parent);
        }
    }

    internal class TreeNodeBuilder : IAnswerMessageTraverser<TreeNode>
    {
        public TreeNode VisitMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            // First build up the child TreeNode
            TreeNode child = VisitMessagePart(message.MessagePart);

            // Then create the topmost root node with the subject as text
            TreeNode topNode = new TreeNode(message.Headers.Subject, new[] { child });

            return topNode;
        }

        public TreeNode VisitMessagePart(MessagePart messagePart)
        {
            if (messagePart == null)
                throw new ArgumentNullException("messagePart");

            // Default is that this MessagePart TreeNode has no children
            TreeNode[] children = new TreeNode[0];

            if (messagePart.IsMultiPart)
            {
                // If the MessagePart has children, in which case it is a MultiPart, then
                // we create the child TreeNodes here
                children = new TreeNode[messagePart.MessageParts.Count];

                for (int i = 0; i < messagePart.MessageParts.Count; i++)
                {
                    children[i] = VisitMessagePart(messagePart.MessageParts[i]);
                }
            }

            // Create the current MessagePart TreeNode with the found children
            TreeNode currentNode = new TreeNode(messagePart.ContentType.MediaType, children);

            // Set the Tag attribute to point to the MessagePart.
            currentNode.Tag = messagePart;

            return currentNode;
        }
    }

}
