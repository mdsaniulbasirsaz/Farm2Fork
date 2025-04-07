using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Farm2Fork.Repositories;

namespace Farm2Fork.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendOtpEmailAsync(string email, string otp)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("saniulsaj@gmail.com", "asxq ydev fucf xkqg"),
                EnableSsl = true,
            };

            string htmlContent = $@"
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Welcome to Farm2Fork</title>
                    <style>
                        body {{
                            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                            background-color: #f5f7fa;
                            margin: 0;
                            padding: 0;
                            color: #333;
                            line-height: 1.6;
                        }}
                        .email-container {{
                            max-width: 600px;
                            margin: 40px auto;
                            background-color: #ffffff;
                            padding: 30px;
                            border-radius: 12px;
                            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
                        }}
                        .header {{
                            text-align: center;
                        }}
                        .logo {{
                            width: 80px;
                            height: 80px;
                            border-radius: 50%;
                            background-color: #f8f9fa;
                            margin-bottom: 20px;
                        }}
                        .header h2 {{
                            color: #2e7d32;
                            font-size: 28px;
                        }}
                        .otp-container {{
                            background-color: #f1f8e9;
                            border-left: 4px solid #7cb342;
                            padding: 20px;
                            border-radius: 6px;
                            text-align: center;
                        }}
                        .otp {{
                            font-size: 32px;
                            font-weight: bold;
                            color: #2e7d32;
                            letter-spacing: 5px;
                            font-family: monospace;
                        }}
                        .footer {{
                            text-align: center;
                            color: #78909c;
                            font-size: 13px;
                        }}
                    </style>
                </head>
                <body>
                    <div class=""email-container"">
                        <div class=""header"">
                            <img src=""https://img.freepik.com/free-vector/hand-drawn-flat-design-farmers-market-logo_23-2149337484.jpg"" class=""logo"" alt=""Farm2Fork Logo""/>
                            <h2>Welcome to Farm2Fork</h2>
                            <p>Thank you for joining our community!</p>
                        </div>
                        <div class=""otp-container"">
                            <div class=""otp"">{otp}</div>
                            <div>This code is valid for 10 minutes</div>
                        </div>
                        <p>If you didn't sign up, you can ignore this email.</p>
                        <div class=""footer"">
                            <p>Farm2Fork &copy; 2025. All Rights Reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";

            var message = new MailMessage
            {
                From = new MailAddress("saniulsaj@gmail.com"),
                Subject = "Your OTP Code [Farm2Fork]",
                Body = htmlContent,
                IsBodyHtml = true
            };

            message.To.Add(email);

            await smtpClient.SendMailAsync(message);
        }
    }
}
