using Microsoft.EntityFrameworkCore;

public class BankContext : DbContext
{

    private readonly String DbPath = "./Database/Bank.sqlite3";

    public DbSet<ExampleModel> Examples {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder options) 
    {
        options.UseSqlite($"Data Source={DbPath}");
    }

}