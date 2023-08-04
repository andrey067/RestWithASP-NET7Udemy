using Domain.Shared.Interfaces;
using Domain.Vaidator;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Book: BaseEntity, IValidar
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public decimal Price { get; private set; }
        public DateTime LaunchDate { get; private set; }

        //EF
        protected Book() { }

        public Book(string title, string author, decimal price, DateTime launchDate)
        {
            Title = title;
            Author = author;
            Price = price;
            LaunchDate = launchDate;
        }

        // Método para atualizar o título do livro
        public void UpdateTitle(string newTitle)
        {
            // Validar o novo título, se necessário
            Title = newTitle;
        }

        // Método para atualizar o autor do livro
        public void UpdateAuthor(string newAuthor)
        {
            // Validar o novo autor, se necessário
            Author = newAuthor;
        }

        // Método para atualizar o preço do livro
        public void UpdatePrice(decimal newPrice)
        {
            // Validar o novo preço, se necessário
            Price = newPrice;
        }

        // Método para atualizar a data de lançamento do livro
        public void UpdateLaunchDate(DateTime newLaunchDate)
        {
            // Validar a nova data de lançamento, se necessário
            LaunchDate = newLaunchDate;
        }

        public void Validar()
             => Validate(new BookValidator(), this);
    }
}
