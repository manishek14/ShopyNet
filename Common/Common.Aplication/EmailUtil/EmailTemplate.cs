namespace Common.Aplication.EmailUtil
{
    public static class EmailTemplate
    {
        public static string BuildView(this string body)
        {
            var styleBody = @"
                * {
                    -webkit-font-smoothing: antialiased;
                }
                body {
                    margin: 0;
                    padding: 0;
                    min-width: 100%;
                    -webkit-font-smoothing: antialiased;
                    mso-line-height-rule: exactly;
                    background-color: #f4f4f4;
                }
                table {
                    border-spacing: 0;
                    color: #333333;
                }
                img {
                    border: 0;
                    max-width: 100%;
                    height: auto;
                }
                .wrapper {
                    width: 100%;
                    table-layout: fixed;
                    -webkit-text-size-adjust: 100%;
                    -ms-text-size-adjust: 100%;
                }
                .webkit {
                    max-width: 600px;
                }
                .outer {
                    margin: 0 auto;
                    width: 100%;
                    max-width: 600px;
                }
                .full-width-image img {
                    width: 100%;
                    max-width: 600px;
                    height: auto;
                }
                .inner {
                    padding: 10px;
                }
                p {
                    margin: 0;
                    padding-bottom: 10px;
                    line-height: 1.6;
                }
                .h1 {
                    font-size: 21px;
                    font-weight: bold;
                    margin-top: 15px;
                    margin-bottom: 5px;
                    font-family: Arial, sans-serif;
                }
                .h2 {
                    font-size: 18px;
                    font-weight: bold;
                    margin-top: 10px;
                    margin-bottom: 5px;
                }
                .one-column.contents {
                    text-align: left;
                }
                .one-column p {
                    font-size: 14px;
                    margin-bottom: 10px;
                }
                .two-column {
                    text-align: center;
                    font-size: 0;
                }
                .two-column .column {
                    width: 100%;
                    max-width: 300px;
                    display: inline-block;
                    vertical-align: top;
                }
                .contents {
                    width: 100%;
                }
                .two-column .contents {
                    font-size: 14px;
                    text-align: left;
                }
                .two-column img {
                    width: 100%;
                    max-width: 280px;
                    height: auto;
                }
                .two-column .text {
                    padding-top: 10px;
                }
                .three-column {
                    text-align: center;
                    font-size: 0;
                    padding-top: 10px;
                    padding-bottom: 10px;
                }
                .three-column .column {
                    width: 100%;
                    max-width: 200px;
                    display: inline-block;
                    vertical-align: top;
                }
                .three-column .contents {
                    font-size: 14px;
                    text-align: center;
                }
                .three-column img {
                    width: 100%;
                    max-width: 180px;
                    height: auto;
                }
                .three-column .text {
                    padding-top: 10px;
                }
                .img-align-vertical img {
                    display: inline-block;
                    vertical-align: middle;
                }
                .footer {
                    background-color: #1f3ca6;
                    color: #ffffff;
                    text-align: center;
                    padding: 15px 10px;
                }
                .footer a {
                    color: #ffffff;
                    text-decoration: none;
                }
                .header {
                    background-color: #1f3ca6;
                    height: 6px;
                    border-top-left-radius: 10px;
                    border-top-right-radius: 10px;
                }
                .footer-bottom {
                    background-color: #1f3ca6;
                    height: 6px;
                    border-bottom-left-radius: 10px;
                    border-bottom-right-radius: 10px;
                }
                .content-box {
                    background-color: #f7f7f7;
                    padding: 20px 40px;
                }
                .content-inner {
                    direction: rtl;
                    font-family: Tahoma, Arial, sans-serif;
                    font-size: 13px;
                    line-height: 1.8;
                    color: #333333;
                }

                @media only screen and (max-width: 480px) {
                    .outer {
                        max-width: 100% !important;
                    }
                    .content-box {
                        padding: 15px 20px !important;
                    }
                    .h1 {
                        font-size: 18px !important;
                    }
                    .h2 {
                        font-size: 16px !important;
                    }
                    .content-inner {
                        font-size: 12px !important;
                    }
                }
            ";

            var tempBody = $@"
                <center class='wrapper'>
                    <table class='outer' align='center' cellpadding='0' cellspacing='0' border='0'>
                        <tr>
                            <td>
                                <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                                    <tr>
                                        <td class='header'></td>
                                    </tr>
                                </table>

                                <table class='one-column' border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#f7f7f7'>
                                    <tr>
                                        <td class='content-box'>
                                            <div class='content-inner'>
                                                {body}
                                            </div>
                                        </td>
                                    </tr>
                                </table>

                                <table width='100%' cellpadding='0' cellspacing='0' border='0' bgcolor='#1f3ca6'>
                                    <tr>
                                        <td class='footer'>
                                            <font style='font-size:13px; text-decoration:none; color:#ffffff; font-family:Verdana, Geneva, sans-serif;'>
                                                <h4 style='margin:0;'>
                                                    <a href='https://www.manishek.ir' target='_blank'>
                                                        مانی شکفته مهندس بک اند
                                                    </a>
                                                </h4>
                                            </font>
                                        </td>
                                    </tr>
                                </table>

                                <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                                    <tr>
                                        <td class='footer-bottom'></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </center>";

            var emailBody = $@"
                <!DOCTYPE html>
                <html lang='fa' dir='rtl'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                    <title>Email</title>
                    <style type='text/css'>
                        {styleBody}
                    </style>
                </head>
                <body>
                    {tempBody}
                </body>
                </html>";

            return emailBody;
        }
    }
}