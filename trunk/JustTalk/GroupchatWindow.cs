using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Goodware.Jabber.GUI {
	public enum Show { chat, away, xa, dnd };

	public partial class GroupchatWindow : Form {
		private delegate void RemoveGroupChatDelegate(String groupName);

		private class Member {
			internal String nick;
			internal Show show;
			internal String statusMessage;
			internal int colorIndex;

			public Member(String nick, Show show, String statusMessage, int colorIndex) {
				this.nick = nick;
				this.show = show;
				this.statusMessage = statusMessage;
				this.colorIndex = colorIndex;
			}
			
			public override String ToString() {
				return nick;
			}
		};

		private Dictionary<String, Member> members;
		private StringBuilder stringBuilder;
		private String nick;
		private JustTalk gui;
		private Color[] colors = new Color[] {
			Color.Gray,
			Color.Blue,
			Color.BlueViolet,
			Color.Brown,
			Color.CadetBlue,
			Color.Chocolate,
			Color.Coral,
			Color.CornflowerBlue,
			Color.Crimson,
			Color.DarkBlue,
			Color.DarkGoldenrod,
			Color.DarkGreen,
			Color.DarkMagenta	
		};

		public GroupchatWindow(String groupName, String nick, JustTalk gui) {
			InitializeComponent();
			this.Text = groupName + "Group Chat";
			this.nick = nick;
			this.gui = gui;

			members = new Dictionary<string, Member>();
			this.ReceivePresence(nick, Goodware.Jabber.GUI.Show.chat, "");

			stringBuilder = new StringBuilder(@"{\rtf1\ansi{\fonttbl\f0\fswiss Helvetica;}{\colortbl ;");
			foreach(Color c in colors)	{
				stringBuilder.Append(@"\red" + c.R + @"\green" + c.G + @"\blue" + c.B + ";");
			}
			stringBuilder.Append(@"}\fs16");

		}

		private void membersListBox_DrawItem(object sender, DrawItemEventArgs e) {
			e.DrawBackground();							// Draw the background of the ListBox control for each item.int n = e.Index % colors.Length;

			Member m = (Member)membersListBox.Items[e.Index];
			Brush brush = new SolidBrush(colors[m.colorIndex]);		
			e.Graphics.DrawImage(Properties.Resources.user, e.Bounds.Left + 1, e.Bounds.Top + 1, 12, 12);
			e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(),
						e.Font, brush, new Point(e.Bounds.Left + 15, e.Bounds.Top + 1), StringFormat.GenericDefault);
		}

		private void inputTextBox_KeyDown(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.Enter) {
				if(!inputTextBox.Text.EndsWith("\n")) {
					SendMessage(inputTextBox.Text);					
				}
				inputTextBox.Clear();
			}
		}

		private void SendMessage(String body) {
			gui.SendMessage(this.Text + ".group@" + gui.model.ServerName + @"/" + nick, body);
		}

		public void ReceivePresence(String userNick, Show show, String statusMessage) {
			if(members.ContainsKey(userNick)) {
				members[userNick].show = show;
				members[userNick].statusMessage = statusMessage;
				this.Invalidate(true);
			} else {
				Member member = new Member(userNick, show, statusMessage, new Random().Next(1, colors.Length));
				members[userNick] = member;
				membersListBox.Items.Add(member);
			}
		}

		public void RemoveMember(String userNick) {
			if(members.ContainsKey(userNick)) {
				Member m = members[userNick]; 
				members.Remove(userNick);
				membersListBox.Items.Remove(m);
			}
		}

		protected override void OnClosing(CancelEventArgs e) {
			gui.model.sendPresence(this.Text + ".group@" + gui.model.ServerName + @"/" + nick, "unavailable", null, null, null);
			RemoveGroupChatDelegate rgcd = new RemoveGroupChatDelegate(gui.RemoveGroupChat);
			gui.Invoke(rgcd, new Object[] { this.Text });

			base.OnClosing(e);
		}
	}
}