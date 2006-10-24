using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Goodware.Jabber.Library {
    public class Packet
    {
		public Packet(String element) {
			Element = (element);
		}

		public Packet(String element, String value) {
			Element = (element);
			children.Add(value);
		}
		public Packet(Packet parent, String element, String nmspace, Dictionary<String, String> atts) {
			Element = (element);
			Namespace = (nmspace);
			Parent = (parent);
			if (atts != null) {
				attributes = atts;
			} else {
				clearAttributes();
			}
			
		}

		String nmspace;
		public String Namespace {
			get {
				return this.nmspace;
			}
			set {
				this.nmspace = value;
			}


		}

		[System.Obsolete("use property instead")]
		public void setNamespace(String name) {
			this.nmspace = name;
		}
		[System.Obsolete("use property instead")]
		public String getNamespace() {
			return this.nmspace;
		}

		String element;
		public String Element {
			get {
				return this.element;
			}
			set {
				this.element = value;
			}
		}

		[System.Obsolete("use property instead")]
		public void setElement(String element) {
			this.element = element;
		}
		[System.Obsolete("use property instead")]
		public String getElement() {
			return this.element;
		}

		Packet parent;
		public Packet Parent {
			get {
				return this.parent;
			}
			set {
				this.parent = value;
				if (value != null) {
					value.children.Add(this);
				}

			}

		}

		[System.Obsolete("use property instead")]
		public Packet getParent() {
			return this.parent;
		}
		[System.Obsolete("use property instead")]
		public void setParent(Packet parent) {
			this.parent = parent;
			if (parent != null) {
				parent.children.Add(this);
			}
		}

		ArrayList children = new ArrayList();
		public ArrayList Children {
			get {
				return this.children;
			}

		}


		[System.Obsolete("use property instead")]
		public ArrayList getChildren() {
			return this.children;
		}

		public Packet getFirstChild(String subelement) {
			foreach (Object child in children) {
				if (child is Packet) {
					
					Packet childPacket = (Packet)child;
					if (childPacket.Element.Equals(subelement)) {
						return childPacket;
					}
				}
			}
			return null;
		}

		public String getValue() {
			StringBuilder value = new StringBuilder();
			foreach (Object child in children) {
				if (child is String) {
					value.Append((String)child);
				}
			}
			return value.ToString().Trim();
		}

		public String getChildValue(String subelement) {
			Packet child = this.getFirstChild(subelement);
			if (child != null) {
				return child.getValue();
			} else {
				return null;
			}
		}

		Session session;
		public Session Session {
			get {
				if (this.session != null) {
					return this.session;
				} else if (this.parent != null) {
					return parent.Session;
				} else {
					return null;
				}
			}
			set {
				this.session = value;
			}
		}

		[System.Obsolete("use property instead")]
		public void setSession(Session session) {
			this.session = session;
		}

		[System.Obsolete("use property instead")]
		public Session getSession() {
			if (session != null) {
				return session;
			}
			if (parent != null) {
				return parent.getSession();
			}
			return null;
		}

		Dictionary<String, String> attributes = new Dictionary<string, string>();
		public String this[String att] {
			get {
				try {
					return attributes[att];
				} catch (Exception ex) {
					return null;
				}

			}
			set {
				if (value == null) {
					removeAttribute(att);
				} else {
					attributes[att] = value;
				}
			}
		}


		[System.Obsolete("use indexer instead")]
		public String getAttribute(String attribute) {
			try {
				return attributes[attribute];
			} catch (Exception ex) {
				return null;
			}

		}
		[System.Obsolete("use indexer instead")]
		public void setAttribute(String attribute, String value) {
			if (value == null) {
				removeAttribute(attribute);
			} else {
				attributes[attribute] = value;
			}
		}

		public void removeAttribute(String attribute) {
			attributes.Remove(attribute);
		}

		public void clearAttributes() {
			attributes.Clear();
		}

		public String To {
			get {
				try {
					return attributes["to"];
				} catch (Exception ex) {
					return null;
				}

			}
			set {
				attributes["to"] = value;
			}


		}

		[System.Obsolete("use property instead")]
		public String getTo() {
			try {
				return attributes["to"];
			} catch (Exception ex) {
				return null;
			}

		}
		[System.Obsolete("use property instead")]
		public void setTo(String recipient) {
			attributes["to"] = recipient;
		}

		public String From {
			get {
				try {
					return attributes["from"];
				} catch (Exception ex) {
					return null;
				}

			}
			set {
				attributes["from"] = value;
			}


		}

		[System.Obsolete("use property instead")]
		public String getFrom() {
			try {
				return attributes["from"];
			} catch (Exception ex) {
				return null;
			}

		}
		[System.Obsolete("use property instead")]
		public void setFrom(String sender) {
			attributes["from"] = sender;
		}

		public String Type {
			get {
				try {
					return attributes["type"];
				} catch (Exception ex) {
					return null;
				}
			}
			set {
				attributes["type"] = value;
			}


		}

		[System.Obsolete("use property instead")]
		public String getType() {
			try {
				return attributes["type"];
			} catch (Exception ex) {
				return null;
			}

		}
		[System.Obsolete("use property instead")]
		public void setType(String type) {
			attributes["type"] = type;
		}

		public String ID {
			get {
				try {
					return attributes["id"];
				} catch (Exception ex) {
					return null;
				}

			}
			set {
				attributes["id"] = value;
			}


		}

		[System.Obsolete("use property instead")]
		public String getID() {
			try {
				return attributes["id"];
			} catch (Exception ex) {
				return null;
			}

		}
		[System.Obsolete("use property instead")]
		public void setID(String id) {
			attributes["id"] = id;
		}

		public override String ToString() {
			StringWriter output = new StringWriter();
			output.Write("<");
			output.Write(element);

			foreach (KeyValuePair<String, String> kvp in attributes) {
				output.Write(" ");
				output.Write(kvp.Key);
				output.Write("='");
				output.Write(kvp.Value);
				output.Write("'");
			}

			if (children.Count == 0) {
				if (this.element == "stream:stream" || this.element == "/stream:stream") {
					output.Write(">");
					return output.ToString();
				} else {
					output.Write("/>");
					return output.ToString();
				}
			}

			output.WriteLine(">");

			foreach (Object child in children) {
				output.WriteLine(child.ToString());
			}
			output.Write("</");
			output.Write(element);
			output.Write(">");

			return output.ToString();
		}

	}

}