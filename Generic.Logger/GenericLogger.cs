using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Generic.Logger
{
    public class GenericLogger
    {
        private string smtp = "SMTP";
        private string email = "email@domain.io";
        private string senha = "senha_email";
        private int smtpPort = 0;
        
        public void ArquivoLog(String strProcesso, String strLog)
        {
            try
            {
                String strArquivo = String.Format("Log_{0}_{1}_{2}.txt", System.DateTime.Now.Day, System.DateTime.Now.Month, System.DateTime.Now.Year);
                FileStream file = new FileStream(String.Format("c:/xml/{0}", strArquivo), FileMode.Append);

                StreamWriter streamEscreve = new StreamWriter(file);
                streamEscreve.WriteLine(String.Format("[{0} - {1}] {2}", strProcesso, System.DateTime.Now, strLog));

                streamEscreve.Close();

            }
            catch 
            {
            }
        }

        public void ArquivoLog(String strProcesso, String strLog, String nomeArquivo)
        {
            try
            {
                String strArquivo = String.Format("Log_{3}_{0}_{1}_{2}.txt", System.DateTime.Now.Day, System.DateTime.Now.Month, System.DateTime.Now.Year, nomeArquivo);
                FileStream file = new FileStream(String.Format("c:/xml/{0}", strArquivo), FileMode.Append);

                StreamWriter streamEscreve = new StreamWriter(file);
                streamEscreve.WriteLine(String.Format("[{0} - {1}] {2}", strProcesso, System.DateTime.Now, strLog));

                streamEscreve.Close();

            }
            catch
            {
            }
        }

        public void ArquivoLog(String strProcesso, Exception ex)
        {
            try
            {
                String strArquivo = String.Format("Log_{0}_{1}_{2}.txt", System.DateTime.Now.Day, System.DateTime.Now.Month, System.DateTime.Now.Year);
                FileStream file = new FileStream(String.Format("c:/xml/{0}", strArquivo), FileMode.Append);

                StreamWriter streamEscreve = new StreamWriter(file);
                streamEscreve.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------");
                streamEscreve.WriteLine(String.Format("Metodo: {0}", strProcesso));
                streamEscreve.WriteLine(String.Format("Data/Hora: {0}", System.DateTime.Now));
                streamEscreve.WriteLine(String.Format("Error Mensage: {0}", ex.Message));
                streamEscreve.WriteLine(String.Format("StackTrace: {0}", ex.StackTrace));
                if (ex.InnerException != null)
                {
                    streamEscreve.WriteLine(String.Format("InnerException Message: {0}", ex.InnerException.Message));
                    streamEscreve.WriteLine(String.Format("InnerException StackTrace: {0}", ex.InnerException.StackTrace));
                    if (ex.InnerException.InnerException != null)
                        streamEscreve.WriteLine(String.Format("InnerException StackTrace: {0}", ex.InnerException.InnerException));
                }
                streamEscreve.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
                streamEscreve.Close();

            }
            catch
            {
            }
        }

        public void ArquivoLog(String strProcesso, Exception ex, String nomeArquivo)
        {
            try
            {
                String strArquivo = String.Format("Log_{3}_{0}_{1}_{2}.txt", System.DateTime.Now.Day, System.DateTime.Now.Month, System.DateTime.Now.Year, nomeArquivo);
                FileStream file = new FileStream(String.Format("c:/xml/{0}", strArquivo), FileMode.Append);

                StreamWriter streamEscreve = new StreamWriter(file);
                streamEscreve.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
                streamEscreve.WriteLine(String.Format("Metodo: {0}", strProcesso));
                streamEscreve.WriteLine(String.Format("Data/Hora: {0}", System.DateTime.Now));
                streamEscreve.WriteLine(String.Format("Error Mensage: {0}", ex.Message));
                streamEscreve.WriteLine(String.Format("StackTrace: {0}", ex.StackTrace));
                if (ex.InnerException != null)
                {
                    streamEscreve.WriteLine(String.Format("InnerException Message: {0}", ex.InnerException.Message));
                    streamEscreve.WriteLine(String.Format("InnerException StackTrace: {0}", ex.InnerException.StackTrace));
                    if (ex.InnerException.InnerException != null)
                        streamEscreve.WriteLine(String.Format("InnerException StackTrace: {0}", ex.InnerException.InnerException));
                }
                streamEscreve.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
                streamEscreve.Close();

            }
            catch
            {
            }
        }

        public void EmailLog(String strProcesso, Exception ex)
        {
            try
            {
                StringBuilder mensagem = new StringBuilder();

                mensagem.AppendLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
                mensagem.AppendLine(String.Format("Metodo: {0}", strProcesso));
                mensagem.AppendLine(String.Format("Data/Hora: {0}", System.DateTime.Now));
                mensagem.AppendLine(String.Format("Error Mensage: {0}", ex.Message));
                mensagem.AppendLine(String.Format("StackTrace: {0}", ex.StackTrace));
                if (ex.InnerException != null)
                {
                    mensagem.AppendLine(String.Format("InnerException Message: {0}", ex.InnerException.Message));
                    mensagem.AppendLine(String.Format("InnerException StackTrace: {0}", ex.InnerException.StackTrace));
                    if (ex.InnerException.InnerException != null)
                        mensagem.AppendLine(String.Format("InnerException StackTrace: {0}", ex.InnerException.InnerException));
                }
                mensagem.AppendLine("-------------------------------------------------------------------------------------------------------------------------------------------------");

                var client = new SmtpClient(smtp, smtpPort)
                {
                    Credentials = new NetworkCredential(email, senha),
                    EnableSsl = false
                };

                client.Send(email, "suporte@domain.com", "Erro Sistema", mensagem.ToString());
            }
            catch
            {
            }
        }

        public void EmailEnvioMens(String strAssunto, String strMensagem, String emailDestino)
        {
            try
            {
                StringBuilder mensagem = new StringBuilder();

                mensagem.AppendLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
                mensagem.AppendLine(String.Format("Assunto: {0}", strAssunto));
                mensagem.AppendLine(String.Format("Data/Hora: {0}", System.DateTime.Now));
                mensagem.AppendLine(String.Format("Mensagem: {0}", strMensagem));

                mensagem.AppendLine("-------------------------------------------------------------------------------------------------------------------------------------------------");

                var client = new SmtpClient(smtp, smtpPort)
                {
                    Credentials = new NetworkCredential(email, senha),
                    EnableSsl = false
                };

                if (emailDestino != "")
                    client.Send(email, emailDestino, strAssunto, mensagem.ToString());

            }
            catch
            {
            }
        }        

        public void EmailFloders(String folders, String strMensagem, String emailDestino)
        {
            try
            {
                StringBuilder mensagem = new StringBuilder();

                mensagem.AppendLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
                //mensagem.AppendLine(String.Format("Assunto: {0}", strServico));
                mensagem.AppendLine(String.Format("Data/Hora: {0}", System.DateTime.Now));
                mensagem.AppendLine(String.Format("Mensagem: {0}", strMensagem));

                mensagem.AppendLine("-------------------------------------------------------------------------------------------------------------------------------------------------");

                var client = new SmtpClient(smtp, smtpPort)
                {
                    Credentials = new NetworkCredential(email, senha),
                    EnableSsl = false
                };

                client.Send(email, emailDestino, "", mensagem.ToString());


            }
            catch
            {
            }
        }

        public void EmailHtml(String strAssunto, String strMensagem, String emailDestino)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(email);


                var client = new SmtpClient(smtp, smtpPort)
                {
                    Credentials = new NetworkCredential(email, senha),
                    EnableSsl = false
                };

                StringBuilder body = new StringBuilder();

                body.Append("<html>");
                body.Append("<body style=\"font-family:Myriad Pro; text-align: center; font-size: 14px\">");
                body.Append("<img src='http://dealerbr.app2m.io/images/dealerbr_logo.png'/>");
                body.Append("<br>");
                body.Append("<br>");
                body.Append("<h1 style=\"font-family:arial; font-size: 30px; text-align: center; color: #263238\">" + strMensagem + "</h1>");
                body.Append("<br>");
                body.Append("<br>");
                body.Append("<br>");
                //body.Append("<p style=\"font-family:Myriad Pro; font-size: 20px; text-align: center;\">Desenvolvido por: <img src='https://media.licdn.com/mpr/mpr/shrink_200_200/AAEAAQAAAAAAAAcSAAAAJDdiOTgyMWU5LWNkNDEtNDk2OS1iYmJmLWM2MDgyYTk0OTliMQ.png'/></p>");
                body.Append("</body>");
                body.Append("</html>");

                mailMessage.To.Add(emailDestino);
                mailMessage.Subject = strAssunto;
                mailMessage.Body = body.ToString();
                mailMessage.IsBodyHtml = true;

                client.Send(mailMessage);

            }
            catch
            {
            }
        }
    }
}