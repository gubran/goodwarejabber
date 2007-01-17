using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Goodware.Jabber.GUI {
	public enum Show { chat, away, dnd, xa };

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
		private String groupName;
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

		private Image[] images = new Image[] {
			Properties.Resources.user,
			Properties.Resources.user_away,
			Properties.Resources.user_busy
		};

		public GroupchatWindow(String groupName, String nick, JustTalk gui) {
			InitializeComponent();
			this.groupName = groupName;
			this.Text = groupName + " Group Chat";
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
			e.Graphics.DrawImage(images[(int)m.show], e.Bounds.Left + 1, e.Bounds.Top + 1, 12, 12);
			String temp = m.nick;
			if(!String.IsNullOrEmpty(m.statusMessage)) {
				temp += " (" + m.statusMessage + ")";
			}
			e.Graphics.DrawString(temp, e.Font, brush, new Point(e.Bounds.Left + 15, e.Bounds.Top), StringFormat.GenericDefault);
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
			//gui.SendMessage(this.Text + ".group@" + gui.model.ServerName + @"/" + nick, body);
			gui.model.sendMessage(this.groupName + ".group@" + gui.model.ServerName /*+ @"/" + nick*/, null, null, "groupchat", null, body);
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

			if(userNick.Equals(nick)) {	// It's me!
				switch(show) {
					case Goodware.Jabber.GUI.Show.chat:
						statusToolStripDropDownButton.Image = Properties.Resources.lightbulb;
						break;
					case Goodware.Jabber.GUI.Show.away:
						statusToolStripDropDownButton.Image = Properties.Resources.lightbulb_off;
						break;
					case Goodware.Jabber.GUI.Show.dnd:
						statusToolStripDropDownButton.Image = Properties.Resources.lightbulb_delete;
						break;
				}
			}
		}

		public void ReceiveMessage(String userNick, String body) {
			body = body.Replace('\\', ' ');
			body = body.Replace(":)", @"{\b:)}");
			body = body.Replace(";)", @"{\b;)}");
			body = body.Replace(":D", @"{\b:D}");
			body = body.Replace(":(", @"{\b:(}");
			body = body.Replace("8)", @"{\b8)}");
			body = body.Replace(":P", @"{\b:P}");
			if(userNick != null && members.ContainsKey(userNick)) {
				Member m = members[userNick];
				stringBuilder.Append(@"{\cf" + (m.colorIndex + 1) + @"{\b " + " " + userNick + @": }" + body + @"}\par");
			} else {
				stringBuilder.Append(@"{\cf1" + "  " + body + @"}\par");
			}
			dialogView.Rtf = stringBuilder.ToString() + "}";
			dialogView.Select(dialogView.TextLength, 0);
			dialogView.ScrollToCaret();
		}

		public void RemoveMember(String userNick) {
			if(members.ContainsKey(userNick)) {
				Member m = members[userNick]; 
				members.Remove(userNick);
				membersListBox.Items.Remove(m);
			}
		}

		protected override void OnClosing(CancelEventArgs e) {
			gui.model.sendPresence(this.groupName + ".group@" + gui.model.ServerName + @"/" + nick, "unavailable", null, null, null);
			RemoveGroupChatDelegate rgcd = new RemoveGroupChatDelegate(gui.RemoveGroupChat);
			gui.Invoke(rgcd, new Object[] { this.Text });

			base.OnClosing(e);
		}

		private void onlineToolStripMenuItem_Click(object sender, EventArgs e) {
			gui.model.sendPresence(this.groupName + ".group@" + gui.model.ServerName + @"/" + nick, null, Goodware.Jabber.GUI.Show.chat.ToString(), members[nick].statusMessage, null);
		}

		private void awayToolStripMenuItem_Click(object sender, EventArgs e) {
			gui.model.sendPresence(this.groupName + ".group@" + gui.model.ServerName + @"/" + nick, null, Goodware.Jabber.GUI.Show.away.ToString(), members[nick].statusMessage, null);
		}

		private void busyToolStripMenuItem_Click(object sender, EventArgs e) {
			gui.model.sendPresence(this.groupName + ".group@" + gui.model.ServerName + @"/" + nick, null, Goodware.Jabber.GUI.Show.dnd.ToString(), members[nick].statusMessage, null);
		}

		// Set the status message and keep old ones in history
		private String previousStatusMessage;
		private void statusMessageToolStripComboBox_KeyPress(object sender, KeyPressEventArgs e) {
			if(e.KeyChar == '\r') {
				gui.model.sendPresence(this.groupName + ".group@" + gui.model.ServerName + @"/" + nick, null, members[nick].show.ToString(), this.statusMessageToolStripComboBox.Text, null);
				if(!String.IsNullOrEmpty(previousStatusMessage)) {
					this.statusMessageToolStripComboBox.Items.Add(previousStatusMessage);
				}
				previousStatusMessage = this.statusMessageToolStripComboBox.Text;
			}
		}

		private void statusMessageToolStripComboBox_Click(object sender, EventArgs e) {
			this.statusMessageToolStripComboBox.SelectAll();
		}
	}
}