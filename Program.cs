private static void Main(string[] args)
{
    if (File.Exists(new AppSettingsReader().GetValue("oldfile", Type.GetType("System.String")).ToString()))
    {
        new WebClient().DownloadFile(new AppSettingsReader().GetValue("URL", Type.GetType("System.String")).ToString(), new AppSettingsReader().GetValue("newfile", Type.GetType("System.String")).ToString());
        if (new FileInfo(new AppSettingsReader().GetValue("oldfile", Type.GetType("System.String")).ToString()).Length != new FileInfo(new AppSettingsReader().GetValue("newfile", Type.GetType("System.String")).ToString()).Length)
        {
            SendMail();
            File.Delete(new AppSettingsReader().GetValue("oldfile", Type.GetType("System.String")).ToString());
            File.Copy(new AppSettingsReader().GetValue("newfile", Type.GetType("System.String")).ToString(), new AppSettingsReader().GetValue("oldfile", Type.GetType("System.String")).ToString());
        }
    }
    else
    {
        new WebClient().DownloadFile(new AppSettingsReader().GetValue("URL", Type.GetType("System.String")).ToString(), new AppSettingsReader().GetValue("oldfile", Type.GetType("System.String")).ToString());
        SendMail();
    }
}


  private static void SendMail()
{
    if (File.Exists(new AppSettingsReader().GetValue("newfile", Type.GetType("System.String")).ToString()))
    {
        MailMessage message = new MailMessage("from@example.com", "to@enduser.com", "New Ohio Liquor List", "Attached is the new list of liquor");
        message.Attachments.Add(new Attachment(new AppSettingsReader().GetValue("newfile", Type.GetType("System.String")).ToString()));
        new SmtpClient("localhost", 25).Send(message);
    }
}

 
