using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembers.Model
{
    /// <summary>
    /// A class that uses a text file to store information about the gym members long-term.
    /// </summary>
    class MemberDB : ObservableObject
    {
        /// <summary>
        /// The list of members to be saved.
        /// </summary>
        private ObservableCollection<Member> members;
        /// <summary>
        /// Where the database is stored.
        /// </summary>
        private const string filepath = "../members.txt";

        /// <summary>
        /// Creates a new member database.
        /// </summary>
        /// <param name="m">The list to saved from or written to.</param>
        public MemberDB(ObservableCollection<Member> m)
        {
            members = m;
        }

        /// <summary>
        /// Reads the saved text file database into the program's list of members.
        /// </summary>
        /// <returns>The list containing the text file data read in.</returns>
        public ObservableCollection<Member> GetMemberships()
        {
            try
            {
                StreamReader input = new StreamReader(new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Read));

                while (input.Peek() != -1)
                {
                    string data = input.ReadLine();
                    string[] tokens = data.Split('|');
                    Member newMember = new Member(tokens[0],tokens[1], tokens[2]);
                    members.Add(newMember);
                }

                input.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid e-mail address format.");
            }
            return members;
        }

        /// <summary>
        /// Saves the program's list of members into the text file database.
        /// </summary>
        public void SaveMemberships()
        {
            StreamWriter output = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write));

            for (int i = 0; i < members.Count; i++)
            {
                Member current = members[i];
                string saveText = current.FirstName + "|" + current.LastName + "|" + current.Email;
                output.WriteLine(saveText);
            }

            output.Close();
        }
    }
}