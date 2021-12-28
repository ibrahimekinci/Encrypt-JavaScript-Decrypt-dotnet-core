using Cryptojs.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Cryptojs.Web.Pages
{
    public class IndexRequestModel
    {
        public string UserName { get; set; } = string.Empty;
        public string UserNameEncrypted { get; set; }
        public string Password { get; set; } = string.Empty;
        public string PasswordEncrypted { get; set; }
        [DataType(DataType.MultilineText)]
        public string Note { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string NoteEncrypted { get; set; }
    }
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [FromForm]
        public IndexRequestModel Model { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            ViewData["result"] = "- not started";
        }
        public void OnPost()
         {
            var usernameDecrypted = AESEncryption.DecryptStringAES(Model.UserNameEncrypted);
            bool usernameIsTrue = Model.UserName == usernameDecrypted;

            var passwordDecrypted = AESEncryption.DecryptStringAES(Model.PasswordEncrypted);
            bool passwordIsTrue = Model.Password == passwordDecrypted;

            var noteDecrypted = AESEncryption.DecryptStringAES(Model.NoteEncrypted);
            bool noteIsTrue = Model.Note == noteDecrypted;

            var result = usernameIsTrue && passwordIsTrue && noteIsTrue;

            ViewData["result"] = $"- result : {result}";
        }
    }
}
