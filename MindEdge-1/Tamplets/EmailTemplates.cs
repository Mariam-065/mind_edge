namespace MindEdge_1.Templates
{
    public static class EmailTemplates
    {
        public static string GetVerificationCodeTemplate(string code)
        {
            var backgroundColor = "#F8F3EA"; 
            var cardColor = "#FFFFFF";     
            var primaryTextColor = "#2A180C"; 
            var secondaryTextColor = "#7C6D5D";
            var accentColor = "#DBCFC2"; 
            var codeTextColor = "#8B5E3C"; 

            return $@"
                <div style=""font-family: 'Segoe UI', Roboto, Helvetica, Arial, sans-serif; max-width: 450px; margin: 20px auto; padding: 30px; border-radius: 20px; background-color: {cardColor}; box-shadow: 0 4px 15px rgba(42, 24, 12, 0.05); border: 1px solid #eee; text-align: center;"">
                    <h2 style=""color: {primaryTextColor}; font-weight: 600; margin-bottom: 20px;"">Mind Edge</h2>
                    <p style=""font-size: 17px; color: {secondaryTextColor}; font-style: italic;"">Hello! Use the code below to verify your account:</p>
                    
                    <div style=""margin: 30px 0; padding: 25px; background: linear-gradient(135deg, {backgroundColor} 0%, {accentColor} 100%); border-radius: 12px; border: 1px solid #eee;"">
                        <span style=""font-size: 36px; font-weight: 800; letter-spacing: 8px; color: {codeTextColor}; font-family: 'Courier New', Courier, monospace;"">
                            {code}
                        </span>
                    </div>

                    <p style=""font-size: 13px; color: {secondaryTextColor}; font-style: italic; line-height: 1.6;"">
                        This code is temporary and will expire soon.<br>
                        If you didn't request this, you can safely ignore this email.
                    </p>
                    
                    <div style=""margin-top: 30px; border-top: 1px solid #eee; padding-top: 20px;"">
                        <p style=""font-size: 11px; color: #bbb; text-transform: uppercase; letter-spacing: 1px;"">
                            &copy; 2026 Mind Edge Project • Professional Back-end System
                        </p>
                    </div>
                </div>";
        }
    }
}