namespace InvestmentPortfolio.Infra.Options;
public class EmailOptions
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string To { get; set; }
}
