using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WepApi.Models;

namespace WepApi.Repositories.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id=1,Title="Karagöz",Price=51 },
                new Book { Id=2,Title="Hacivat",Price=123 },
                new Book { Id=3,Title="Keloglan",Price=61 }
                ); 
        }
    }
}
