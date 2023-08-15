using Domain.Shared.Interfaces;
using Domain.Vaidator;

namespace Domain.Entities
{

    public class Person : BaseEntity, IValidar
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public string Gender { get; private set; }
        public bool Status { get; private set; }

        //EF
        protected Person() { }

        public Person(string firstName, string lastName, string address, string gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Gender = gender;
        }

        public void ChangeFirstName(string newFirstName)
            => FirstName = newFirstName;

        public void ChangeLastName(string newLastName)
            => LastName = newLastName;

        public void ChangeAddress(string newAddress)
            => Address = newAddress;

        public void ChangeGender(string newGender)
            => Gender = newGender;

        public void ChangeStatus(bool status)
            => Status = status;

        public void UpdatePerson(string newFirstName, string newLastName, string newAddress, string newGender)
        {
            FirstName = newFirstName;
            LastName = newLastName;
            Address = newAddress;
            Gender = newGender;
        }

        public void Validar()
            => Validate(new PersonValidator(), this);
    }
}