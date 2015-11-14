using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices; // DllImport


namespace xml_aion
{
    class Program
    {
        static void Main(string[] args)
        {
            skill_base_clients skill_base_clients = new skill_base_clients();

            string uri = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\client_skills.xml"; // your big XML file

            foreach (var buff in XmlHelper.StreamBooks(uri))
            {
                string tmp = buff.file.ToLower();
                if (tmp.Contains(".dds"))
                    tmp = tmp.Remove(tmp.Length - 4, 4);
                skill_base_clients.Add(new skill_base_client { id = buff.id, file = tmp });
            }

            XmlSerializer x = new XmlSerializer(typeof(skill_base_clients));
            TextWriter writer = new StreamWriter("boby_client_skills.xml");
            x.Serialize(writer, skill_base_clients);

            foreach (var item in skill_base_clients)
            {
                skill_base_client skill = item as skill_base_client;

                if (File.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\skills\\" + skill.file + ".txt"))
                    File.Move(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\skills\\" + skill.file + ".txt", Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\skills\\" + skill.file + ".png");
            }
        }
    }

    public class skill_base_clients : ICollection
    {
        public string CollectionName;
        private ArrayList empArray = new ArrayList();

        public skill_base_client this[int index]
        {
            get { return (skill_base_client)empArray[index]; }
        }

        public void CopyTo(Array a, int index)
        {
            empArray.CopyTo(a, index);
        }
        public int Count
        {
            get { return empArray.Count; }
        }
        public object SyncRoot
        {
            get { return this; }
        }
        public bool IsSynchronized
        {
            get { return false; }
        }
        public IEnumerator GetEnumerator()
        {
            return empArray.GetEnumerator();
        }

        public void Add(skill_base_client newEmployee)
        {
            empArray.Add(newEmployee);
        }
    }

    public class skill_base_client
    {
        public string id { get; set; }
        public string file { get; set; }
    }

    public static class XmlHelper
    {
        public static IEnumerable<skill_base_client> StreamBooks(string uri)
        {
            using (XmlReader reader = XmlReader.Create(uri))
            {
                string id = null;
                string file = null;

                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element
                        && reader.Name == "skill_base_client")
                    {
                        while (reader.Read())
                        {
                            if (reader.Name == "id")
                            {
                                id = reader.ReadString();
                                break;
                            }
                        }
                        while (reader.Read())
                        {
                            if (reader.Name == "skillicon_name")
                            {
                                file = reader.ReadString();
                                break;
                            }
                        }
                        yield return new skill_base_client() { id = id, file = file };
                    }
                }
            }
        }
    }
}
