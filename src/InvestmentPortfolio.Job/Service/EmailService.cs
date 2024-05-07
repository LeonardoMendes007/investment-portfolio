﻿using System.Net.Mail;
using System.Net;
using InvestmentPortfolio.Job.Options;
using InvestmentPortfolio.Domain.Interfaces.Services;
using InvestmentPortfolio.Domain.Entities.Product;

namespace InvestmentPortfolio.Job.Service;
public class EmailService : IEmailService
{
    private readonly EmailOptions _emailOptions;

    public EmailService(EmailOptions emailOptions)
    {
        _emailOptions = emailOptions;
    }

    public async Task SendproductExpiredEmailAsync(Product product)
    {
        using (var message = new MailMessage())
        {
            message.From = new MailAddress(_emailOptions.Email);
            message.To.Add(_emailOptions.To);
            message.Subject = $"Faltam {(product.ExpirationDate - DateTime.Now).TotalDays} para o produto de ID {product.Id} expirar.";
            
            message.IsBodyHtml = true;

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "Templates", "EmailTemplate.txt");
            var templateEmail = File.ReadAllText(filePath);

            templateEmail = templateEmail.Replace("{Id}", product.Id.ToString());
            templateEmail = templateEmail.Replace("{Name}", product.Name);
            templateEmail = templateEmail.Replace("{InitialPrice}", product.InitialPrice.ToString());
            templateEmail = templateEmail.Replace("{CurrentPrice}", product.CurrentPrice.ToString());
            templateEmail = templateEmail.Replace("{ExpirationDate}", product.ExpirationDate.ToString("dd/MM/yyyy HH:mm:ss"));

            message.Body = templateEmail;

            using (var smtpClient = new SmtpClient(_emailOptions.SmtpServer, _emailOptions.SmtpPort))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_emailOptions.Email, _emailOptions.Password);
                smtpClient.EnableSsl = true;

                try
                {
                    await smtpClient.SendMailAsync(message);
                    Console.WriteLine("Email sent successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
            }
        }
    }
}
