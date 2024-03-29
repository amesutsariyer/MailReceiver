﻿using MailReceiver.Base.Entity;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace MailReceiver.Base
{

    public static class MailService
    {
        public static Response SendMail(MailRequest request)
        {
            Response response = new Response();
            try
            {
                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = request.Credential.UserName,
                        Password = request.Credential.Password
                    };
                    client.Credentials = credential;
                    client.Host = request.HostName;
                    client.Port = request.Port;
                    client.EnableSsl = false;
                    var message = GetMailWithOutImg(request);
                    client.Send(message);
                }
                response.Message = "Başarılı.";
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
        private static MailMessage GetMailWithOutImg(MailRequest request)
        {
            var emailMessage = new MailMessage();
            foreach (var item in request.ToArray)
            {
                emailMessage.Bcc.Add(new MailAddress(item));
            }
            emailMessage.From = new MailAddress(request.From);
            emailMessage.Subject = request.Subject;
            emailMessage.Body = GetMailTemplate1(request.Content);
            emailMessage.IsBodyHtml = true;
            return emailMessage;
        }
        private static AlternateView GetEmbeddedImage(String filePath)
        {
            LinkedResource res = new LinkedResource(filePath);
            res.ContentId = Guid.NewGuid().ToString();
            string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);
            return alternateView;
        }

        private static MailMessage GetMailWithImg()
        {
            var emailMessage = new MailMessage();
            emailMessage.AlternateViews.Add(GetEmbeddedImage(Path.Combine(Environment.CurrentDirectory, @"MailDownloadFile", "download.png")));
            emailMessage.To.Add(new MailAddress("ahmetmesutsariyer@gmail.com"));
            emailMessage.From = new MailAddress("info@ahmetsariyer.com");
            emailMessage.Subject = "Subsctiption Test";
            emailMessage.IsBodyHtml = true;
            return emailMessage;
        }
        private static string GetMailTemplate1(Content content)
        {
            string sb = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" +
            "<html>" +
            "    <head>" +
            "        <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />" +
            "        " +
            "        <!-- Facebook sharing information tags -->" +
            "        <meta property=\"og:title\" content=\"*|MC:SUBJECT|*\" />" +
            "        " +
            "        <title>*|MC:SUBJECT|*</title>" +
            "		<style type=\"text/css\">" +
            "			/* Client-specific Styles */" +
            "			#outlook a{padding:0;} /* Force Outlook to provide a \"view in browser\" button. */" +
            "			body{width:100% !important;} .ReadMsgBody{width:100%;} .ExternalClass{width:100%;} /* Force Hotmail to display emails at full width */" +
            "			body{-webkit-text-size-adjust:none;} /* Prevent Webkit platforms from changing default text sizes. */" +
            "			/* Reset Styles */" +
            "			body{margin:0; padding:0;}" +
            "			img{border:0; height:auto; line-height:100%; outline:none; text-decoration:none;}" +
            "			table td{border-collapse:collapse;}" +
            "			#backgroundTable{height:100% !important; margin:0; padding:0; width:100% !important;}" +
            "			/* Template Styles */" +
            "			/* /\\/\\/\\/\\/\\/\\/\\/\\/\\/\\ STANDARD STYLING: COMMON PAGE ELEMENTS /\\/\\/\\/\\/\\/\\/\\/\\/\\/\\ */" +
            "			/**" +
            "			* @tab Page" +
            "			* @section background color" +
            "			* @tip Set the background color for your email. You may want to choose one that matches your company's branding." +
            "			* @theme page" +
            "			*/" +
            "			body, #backgroundTable{" +
            "				/*@editable*/ background-color:#FAFAFA;" +
            "			}" +
            "			/**" +
            "			* @tab Page" +
            "			* @section email border" +
            "			* @tip Set the border for your email." +
            "			*/" +
            "			#templateContainer{" +
            "				/*@editable*/ border: 1px solid #DDDDDD;" +
            "			}" +
            "			/**" +
            "			* @tab Page" +
            "			* @section heading 1" +
            "			* @tip Set the styling for all first-level headings in your emails. These should be the largest of your headings." +
            "			* @style heading 1" +
            "			*/" +
            "			h1, .h1{" +
            "				/*@editable*/ color:#202020;" +
            "				display:block;" +
            "				/*@editable*/ font-family:Arial;" +
            "				/*@editable*/ font-size:34px;" +
            "				/*@editable*/ font-weight:bold;" +
            "				/*@editable*/ line-height:100%;" +
            "				margin-top:0;" +
            "				margin-right:0;" +
            "				margin-bottom:10px;" +
            "				margin-left:0;" +
            "				/*@editable*/ text-align:left;" +
            "			}" +
            "			/**" +
            "			* @tab Page" +
            "			* @section heading 2" +
            "			* @tip Set the styling for all second-level headings in your emails." +
            "			* @style heading 2" +
            "			*/" +
            "			h2, .h2{" +
            "				/*@editable*/ color:#202020;" +
            "				display:block;" +
            "				/*@editable*/ font-family:Arial;" +
            "				/*@editable*/ font-size:30px;" +
            "				/*@editable*/ font-weight:bold;" +
            "				/*@editable*/ line-height:100%;" +
            "				margin-top:0;" +
            "				margin-right:0;" +
            "				margin-bottom:10px;" +
            "				margin-left:0;" +
            "				/*@editable*/ text-align:left;" +
            "			}" +
            "			/**" +
            "			* @tab Page" +
            "			* @section heading 3" +
            "			* @tip Set the styling for all third-level headings in your emails." +
            "			* @style heading 3" +
            "			*/" +
            "			h3, .h3{" +
            "				/*@editable*/ color:#202020;" +
            "				display:block;" +
            "				/*@editable*/ font-family:Arial;" +
            "				/*@editable*/ font-size:26px;" +
            "				/*@editable*/ font-weight:bold;" +
            "				/*@editable*/ line-height:100%;" +
            "				margin-top:0;" +
            "				margin-right:0;" +
            "				margin-bottom:10px;" +
            "				margin-left:0;" +
            "				/*@editable*/ text-align:left;" +
            "			}" +
            "			/**" +
            "			* @tab Page" +
            "			* @section heading 4" +
            "			* @tip Set the styling for all fourth-level headings in your emails. These should be the smallest of your headings." +
            "			* @style heading 4" +
            "			*/" +
            "			h4, .h4{" +
            "				/*@editable*/ color:#202020;" +
            "				display:block;" +
            "				/*@editable*/ font-family:Arial;" +
            "				/*@editable*/ font-size:22px;" +
            "				/*@editable*/ font-weight:bold;" +
            "				/*@editable*/ line-height:100%;" +
            "				margin-top:0;" +
            "				margin-right:0;" +
            "				margin-bottom:10px;" +
            "				margin-left:0;" +
            "				/*@editable*/ text-align:left;" +
            "			}" +
            "			/* /\\/\\/\\/\\/\\/\\/\\/\\/\\/\\ STANDARD STYLING: HEADER /\\/\\/\\/\\/\\/\\/\\/\\/\\/\\ */" +
            "			/**" +
            "			* @tab Header" +
            "			* @section header style" +
            "			* @tip Set the background color and border for your email's header area." +
            "			* @theme header" +
            "			*/" +
            "			#templateHeader{" +
            "				/*@editable*/ background-color:#FFFFFF;" +
            "				/*@editable*/ border-bottom:0;" +
            "			}" +
            "			/**" +
            "			* @tab Header" +
            "			* @section header text" +
            "			* @tip Set the styling for your email's header text. Choose a size and color that is easy to read." +
            "			*/" +
            "			.headerContent{" +
            "				/*@editable*/ color:#202020;" +
            "				/*@editable*/ font-family:Arial;" +
            "				/*@editable*/ font-size:34px;" +
            "				/*@editable*/ font-weight:bold;" +
            "				/*@editable*/ line-height:100%;" +
            "				/*@editable*/ padding:0;" +
            "				/*@editable*/ text-align:center;" +
            "				/*@editable*/ vertical-align:middle;" +
            "			}" +
            "			/**" +
            "			* @tab Header" +
            "			* @section header link" +
            "			* @tip Set the styling for your email's header links. Choose a color that helps them stand out from your text." +
            "			*/" +
            "			.headerContent a:link, .headerContent a:visited, /* Yahoo! Mail Override */ .headerContent a .yshortcuts /* Yahoo! Mail Override */{" +
            "				/*@editable*/ color:#336699;" +
            "				/*@editable*/ font-weight:normal;" +
            "				/*@editable*/ text-decoration:underline;" +
            "			}" +
            "			#headerImage{" +
            "				height:auto;" +
            "				max-width:600px !important;" +
            "			}" +
            "			/* /\\/\\/\\/\\/\\/\\/\\/\\/\\/\\ STANDARD STYLING: MAIN BODY /\\/\\/\\/\\/\\/\\/\\/\\/\\/\\ */" +
            "			/**" +
            "			* @tab Body" +
            "			* @section body style" +
            "			* @tip Set the background color for your email's body area." +
            "			*/" +
            "			#templateContainer, .bodyContent{" +
            "				/*@editable*/ background-color:#FFFFFF;" +
            "			}" +
            "			/**" +
            "			* @tab Body" +
            "			* @section body text" +
            "			* @tip Set the styling for your email's main content text. Choose a size and color that is easy to read." +
            "			* @theme main" +
            "			*/" +
            "			.bodyContent div{" +
            "				/*@editable*/ color:#505050;" +
            "				/*@editable*/ font-family:Arial;" +
            "				/*@editable*/ font-size:14px;" +
            "				/*@editable*/ line-height:150%;" +
            "				/*@editable*/ text-align:left;" +
            "			}" +
            "			/**" +
            "			* @tab Body" +
            "			* @section body link" +
            "			* @tip Set the styling for your email's main content links. Choose a color that helps them stand out from your text." +
            "			*/" +
            "			.bodyContent div a:link, .bodyContent div a:visited, /* Yahoo! Mail Override */ .bodyContent div a .yshortcuts /* Yahoo! Mail Override */{" +
            "				/*@editable*/ color:#336699;" +
            "				/*@editable*/ font-weight:normal;" +
            "				/*@editable*/ text-decoration:underline;" +
            "			}" +
            "			/**" +
            "			* @tab Body" +
            "			* @section button style" +
            "			* @tip Set the styling for your email's button. Choose a style that draws attention." +
            "			*/" +
            "			.templateButton{" +
            "				-moz-border-radius:3px;" +
            "				-webkit-border-radius:3px;" +
            "				/*@editable*/ background-color:#336699;" +
            "				/*@editable*/ border:0;" +
            "				border-collapse:separate !important;" +
            "				border-radius:3px;" +
            "			}" +
            "			/**" +
            "			* @tab Body" +
            "			* @section button style" +
            "			* @tip Set the styling for your email's button. Choose a style that draws attention." +
            "			*/" +
            "			.templateButton, .templateButton a:link, .templateButton a:visited, /* Yahoo! Mail Override */ .templateButton a .yshortcuts /* Yahoo! Mail Override */{" +
            "				/*@editable*/ color:#FFFFFF;" +
            "				/*@editable*/ font-family:Arial;" +
            "				/*@editable*/ font-size:15px;" +
            "				/*@editable*/ font-weight:bold;" +
            "				/*@editable*/ letter-spacing:-.5px;" +
            "				/*@editable*/ line-height:100%;" +
            "				text-align:center;" +
            "				text-decoration:none;" +
            "			}" +
            "			.bodyContent img{" +
            "				display:inline;" +
            "				height:auto;" +
            "			}" +
            "			/* /\\/\\/\\/\\/\\/\\/\\/\\/\\/\\ STANDARD STYLING: FOOTER /\\/\\/\\/\\/\\/\\/\\/\\/\\/\\ */" +
            "			/**" +
            "			* @tab Footer" +
            "			* @section footer style" +
            "			* @tip Set the background color and top border for your email's footer area." +
            "			* @theme footer" +
            "			*/" +
            "			#templateFooter{" +
            "				/*@editable*/ background-color:#FFFFFF;" +
            "				/*@editable*/ border-top:0;" +
            "			}" +
            "			/**" +
            "			* @tab Footer" +
            "			* @section footer text" +
            "			* @tip Set the styling for your email's footer text. Choose a size and color that is easy to read." +
            "			* @theme footer" +
            "			*/" +
            "			.footerContent div{" +
            "				/*@editable*/ color:#707070;" +
            "				/*@editable*/ font-family:Arial;" +
            "				/*@editable*/ font-size:12px;" +
            "				/*@editable*/ line-height:125%;" +
            "				/*@editable*/ text-align:center;" +
            "			}" +
            "			/**" +
            "			* @tab Footer" +
            "			* @section footer link" +
            "			* @tip Set the styling for your email's footer links. Choose a color that helps them stand out from your text." +
            "			*/" +
            "			.footerContent div a:link, .footerContent div a:visited, /* Yahoo! Mail Override */ .footerContent div a .yshortcuts /* Yahoo! Mail Override */{" +
            "				/*@editable*/ color:#336699;" +
            "				/*@editable*/ font-weight:normal;" +
            "				/*@editable*/ text-decoration:underline;" +
            "			}" +
            "			.footerContent img{" +
            "				display:inline;" +
            "			}" +
            "			/**" +
            "			* @tab Footer" +
            "			* @section utility bar style" +
            "			* @tip Set the background color and border for your email's footer utility bar." +
            "			* @theme footer" +
            "			*/" +
            "			#utility{" +
            "				/*@editable*/ background-color:#FFFFFF;" +
            "				/*@editable*/ border:0;" +
            "			}" +
            "			/**" +
            "			* @tab Footer" +
            "			* @section utility bar style" +
            "			* @tip Set the background color and border for your email's footer utility bar." +
            "			*/" +
            "			#utility div{" +
            "				/*@editable*/ text-align:center;" +
            "			}" +
            "			#monkeyRewards img{" +
            "				max-width:190px;" +
            "			}" +
            "		</style>" +
            "	</head>" +
            "    <body leftmargin=\"0\" marginwidth=\"0\" topmargin=\"0\" marginheight=\"0\" offset=\"0\">" +
            "    	<center>" +
            "        	<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"100%\" width=\"100%\" id=\"backgroundTable\">" +
            "            	<tr>" +
            "                	<td align=\"center\" valign=\"top\" style=\"padding-top:20px;\">" +
            "                    	<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" id=\"templateContainer\">" +
            "                        	<tr>" +
            "                            	<td align=\"center\" valign=\"top\">" +
            "                                    <!-- // Begin Template Header \\\\ -->" +
            "                                	<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" id=\"templateHeader\">" +
            "                                        <tr>" +
            "                                            <td class=\"headerContent\">" +
            "                                            " +
            "                                            	<!-- // Begin Module: Standard Header Image \\\\ -->" +
            "                                            	<img src=\"http://213.128.89.156/plesk-site-preview/uskudardenetim.com/Images/header2.png?v2 \" style=\"max-width:600px;\" id=\"headerImage campaign-icon\" mc:label=\"header_image\" mc:edit=\"header_image\" mc:allowdesigner mc:allowtext />" +
            "                                            	<!-- // End Module: Standard Header Image \\\\ -->" +
            "                                            " +
            "                                            </td>" +
            "                                        </tr>" +
            "                                    </table>" +
            "                                    <!-- // End Template Header \\\\ -->" +
            "                                </td>" +
            "                            </tr>" +
            "                        	<tr>" +
            "                            	<td align=\"center\" valign=\"top\">" +
            "                                    <!-- // Begin Template Body \\\\ -->" +
            "                                	<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" id=\"templateBody\">" +
            "                                    	<tr>" +
            "                                            <td valign=\"top\">" +
            "                                " +
            "                                                <!-- // Begin Module: Standard Content \\\\ -->" +
            "                                                <table border=\"0\" cellpadding=\"20\" cellspacing=\"0\" width=\"100%\">" +
            "                                                    <tr>" +
            "                                                        <td valign=\"top\" class=\"bodyContent\">" +
            "                                                            <div mc:edit=\"std_content00\">" +
            "                                                                <h2 class=\"h2\">" + content.Header + "</h2>" +
            "                                                                <strong>" + content.StrongSubHeader + ":</strong>" + "  " + content.SubHeader +
            "                                                                <br />" +
            "                                                            </div>" +
            "														</td>" +
            "                                                    </tr>" +
            "                                                    <tr>" +
            "                                                    	<td align=\"center\" valign=\"top\" style=\"padding-top:0;\">" +
            "                                                        	<table border=\"0\" cellpadding=\"15\" cellspacing=\"0\" class=\"templateButton\">" +
            "                                                            	<tr>" +
            "                                                                	<td valign=\"middle\" class=\"templateButtonContent\">" +
            "                                                                    	<div mc:edit=\"std_content01\">" +
            "                                                                        	<a style=\" color:white !important\" href=\" "+ content.Link +" \" target =\"_blank\">Devamını Oku</a>" +
            "                                                                        </div>" +
            "                                                                    </td>" +
            "                                                                </tr>" +
            "                                                            </table>" +
            "                                                        </td>" +
            "                                                    </tr>" +
            "                                                </table>" +
            "                                                <!-- // End Module: Standard Content \\\\ -->" +
            "                                                " +
            "                                            </td>" +
            "                                        </tr>" +
            "                                    </table>" +
            "                                    <!-- // End Template Body \\\\ -->" +
            "                                </td>" +
            "                            </tr>" +
            "                        	<tr>" +
            "                            	<td align=\"center\" valign=\"top\">" +
            "                                    <!-- // Begin Template Footer \\\\ -->" +
            "                                	<table border=\"0\" cellpadding=\"10\" cellspacing=\"0\" width=\"600\" id=\"templateFooter\">" +
            "                                    	<tr>" +
            "                                        	<td valign=\"top\" class=\"footerContent\">" +
            "                                            " +
            "                                                <!-- // Begin Module: Transactional Footer \\\\ -->" +
            "                                                <table border=\"0\" cellpadding=\"10\" cellspacing=\"0\" width=\"100%\">" +
            "                                                    <tr>" +
            "                                                        <td valign=\"top\">" +
            "                                                            <div mc:edit=\"std_footer\">" +
            "																<em>Copyright &copy; " + DateTime.Now.Year.ToString() + ", Tüm Hakları Saklıdır.</em>" +
            "																<br />" +
            "																<strong>Mail Adresimiz:</strong>" +
            "																<br />" +
            "																info@uskudardenetim.com " +
            "                                                            </div>" +
            "                                                        </td>" +
            "                                                    </tr>" +
            "                                                    <tr>" +
            "                                                        <td valign=\"middle\" id=\"utility\">" +
            "                                                            <div mc:edit=\"std_utility\">" +
            "                                                                &nbsp;<a href=\"www.uskudardenetim.com \" target=\"www.uskudardenetim.com\">Web Sitesi</a> | <a href=\"www.uskudardenetim.com\">Abonelikten Çık</a> " +
            "                                                            </div>" +
            "                                                        </td>" +
            "                                                    </tr>" +
            "                                                </table>" +
            "                                                <!-- // End Module: Transactional Footer \\\\ -->" +
            "                                            " +
            "                                            </td>" +
            "                                        </tr>" +
            "                                    </table>" +
            "                                    <!-- // End Template Footer \\\\ -->" +
            "                                </td>" +
            "                            </tr>" +
            "                        </table>" +
            "                        <br />" +
            "                    </td>" +
            "                </tr>" +
            "            </table>" +
            "        </center>" +
            "    </body>" +
            "</html>";
            return sb;
        }
    }
}
