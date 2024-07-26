﻿using LawyerProject.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
            {
                mail.To.Add(to);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:Username"], "Avukatım Cepte", System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);

            //Mail Servis kullanımı
            //await _mailService.SendMessageAsync("alparslanaydgn02@gmail.com", "Örnek Mail", "<strong>Bu bir örnek maildir.</strong>");
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();

            mail.AppendLine("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz." +
                "<br><strong><a target=\"_blank\" href=\"");

            string str = _configuration["AngularClientUrl"] + "/update-password/" + userId + "/" + resetToken;
            mail.AppendLine(str);
            mail.AppendLine("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\">Not: " +
                "Eğer ki bu talebi siz gerçekleştirmediyseniz lütfen maili dikkate almayınız.</span><br>Saygılarımızla...<br><br>" +
                "<br>Avukatım Cepte");

            await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString(), true);
        }
    }
}
