using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_PhoneBook {
    internal class PhoneBook {

        public static List<Contact> Contacts { get; set; } = new List<Contact>() {
                new Contact ("Zbyszek","500500500"),

                new Contact ("Marcin","600660543"),

                new Contact ("Marta","700588543"),

                new Contact ("Adam","800555543"),

                new Contact ("Ania","900544543") 

        };




        /// <summary>
        /// Display contact
        /// </summary>
        /// <param name="contact">Contact</param>
        private void DisplayContactDetails(Contact contact) {
            Console.WriteLine($"Contact: {contact.Name}, {contact.Number}");
        }


        /// <summary>
        /// Display all contacts in specyfic list of contacts
        /// </summary>
        /// <param name="contacts">List of contacts</param>
        private void DisplayContactsDetails(List<Contact> contacts) {
            foreach (var contact in contacts) {
                DisplayContactDetails(contact);
            }
        }

        /// <summary>
        /// Add new contact
        /// </summary>
        /// <param name="contact">Date of contact</param>
        public void AddContact(Contact contact) {
            Contacts.Add(contact);
        }


        /// <summary>
        /// Remove exist contatc
        /// </summary>
        /// <param name="contact"></param>
        public void RemoveContact(string number) {
            var contact = Contacts.FirstOrDefault(x => x.Number == number);
            if (contact == null) {
                Console.WriteLine("Contact not found");
            }
            else {
                Contacts.Remove(contact);
            }
        }

        /// <summary>
        /// Display contact by searching number
        /// </summary>
        /// <param name="number">Searchin contact by this number</param>
        public void DisplayContact(string number) {
            var contact = Contacts.FirstOrDefault(x => x.Number == number);
            if (contact == null) {
                Console.WriteLine("Contact not found");
            }
            else {
                DisplayContactDetails(contact);
            }
        }

        /// <summary>
        /// Display all contacts
        /// </summary>
        public void DisplayAllContacts() {
            DisplayContactsDetails(Contacts);
        }

        /// <summary>
        /// Search contacts contians phrase
        /// </summary>
        /// <param name="searchPhrase">Searching phares on in all contacts</param>
        public void DisplayMatchingContacts(string searchPhrase) {
            var machingContacts = Contacts.Where(x => x.Name.Contains(searchPhrase)).ToList();
            DisplayContactsDetails(machingContacts);
        }
    }
}
