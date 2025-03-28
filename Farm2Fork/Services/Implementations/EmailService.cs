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

            var message = new MailMessage
            {
                From = new MailAddress("saniulsaj@gmail.com"),
                Subject = "Your OTP Code [Farm2Fork]",
                Body = $@"
                <html>
                    <head>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 0;
                            }}
                            .email-container {{
                                width: 100%;
                                max-width: 600px;
                                margin: 20px auto;
                                background-color: #ffffff;
                                padding: 20px;
                                border-radius: 8px;
                                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                            }}
                            .header {{
                                text-align: center;
                                color: #333;
                            }}
                            .otp {{
                                font-size: 24px;
                                font-weight: bold;
                                color: #4CAF50;
                                margin: 20px 0;
                            }}
                            .footer {{
                                text-align: center;
                                color: #777;
                                font-size: 12px;
                                margin-top: 20px;
                            }}
                            .button {{
                                display: inline-block;
                                background-color: #4CAF50;
                                color: #fff;
                                text-decoration: none;
                                padding: 10px 20px;
                                border-radius: 4px;
                                font-weight: bold;
                                margin-top: 20px;
                            }}
                            .icon {{
                                width: 50px;
                                height: 50px;
                                margin-bottom: 20px;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='email-container'>
                            <div class='header'>
                                <img src='https://your-website.com/path/to/icon.png' class='icon' alt='Farm2Fork Icon'/>
                                <h2>Welcome to Farm2Fork</h2>
                                <p>We are excited to have you on board!</p>
                            </div>
                            <p>Thank you for signing up. To complete your registration, please use the following OTP:</p>
                            <div class='otp'>
                                {otp}
                            </div>
                            <p>This OTP is valid for 10 minutes. If you did not request this, please ignore this email.</p>
                            <a href='#' class='button'>Verify OTP</a>
                            <div class='footer'>
                                <p>If you have any questions, feel free to contact us at support@farm2fork.com</p>
                                <p>Farm2Fork &copy; 2025. All Rights Reserved.</p>
                            </div>
                        </div>
                    </body>
                </html>",
                IsBodyHtml = true,
            };
            message.To.Add(email);

            await smtpClient.SendMailAsync(message);
        }
    }
}
