using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Word;

namespace DocEditApp.Controllers
{
    public class HtmlConvertController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HtmlConvertController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Content("file not selected");
            }

            object path = Path.Combine(
                Directory.GetCurrentDirectory(), "Content\\Documents\\",
                file.FileName);

            using (var stream = new FileStream((string)path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            object missingType = Type.Missing;
            object readOnly = true;
            object isVisible = false;
            object documentFormat = 8;
            string randomName = DateTime.Now.Ticks.ToString();
            object htmlFilePath = Path.Combine(
                Directory.GetCurrentDirectory(), "Content\\Documents\\",
                randomName + ".htm");// Server.MapPath("~/Temp/") + randomName + ".htm";
            string directoryPath = Path.Combine(
                Directory.GetCurrentDirectory(), "Content\\Documents\\",
                randomName + "_files");//Server.MapPath("~/Temp/") + randomName + "_files";

            //Open the word document in background
            var applicationclass = new ApplicationClass();
            applicationclass.Documents.Open(ref path,
                                            ref readOnly,
                                            ref missingType, ref missingType, ref missingType,
                                            ref missingType, ref missingType, ref missingType,
                                            ref missingType, ref missingType, ref isVisible,
                                            ref missingType, ref missingType, ref missingType,
                                            ref missingType, ref missingType);
            applicationclass.Visible = false;
            Microsoft.Office.Interop.Word.Document document = applicationclass.ActiveDocument;

            //Save the word document as HTML file
            document.SaveAs(ref htmlFilePath, ref documentFormat, ref missingType,
                            ref missingType, ref missingType, ref missingType,
                            ref missingType, ref missingType, ref missingType,
                            ref missingType, ref missingType, ref missingType,
                            ref missingType, ref missingType, ref missingType,
                            ref missingType);

            //Close the word document
            document.Close(ref missingType, ref missingType, ref missingType);

            //Delete the Uploaded Word File
            //File.Delete(Server.MapPath("~/Temp/") + Path.GetFileName(FileUpload1.PostedFile.FileName));

            //Read the Html File as Byte Array and Display it on browser
            byte[] bytes;
            using (FileStream fs = new FileStream(htmlFilePath.ToString(), FileMode.Open, FileAccess.Read))
            {
                BinaryReader reader = new BinaryReader(fs);
                bytes = reader.ReadBytes((int)fs.Length);
                fs.Close();
            }
            //Response.BinaryWrite(bytes);
            //Response.Flush();

            //Delete the Html File
            //File.Delete(htmlFilePath.ToString());
            //foreach (string file in Directory.GetFiles(directoryPath))
            //{
            //    File.Delete(file);
            //}
            //Directory.Delete(directoryPath);
            //Response.End();

            string wordHTML = System.IO.File.ReadAllText(htmlFilePath.ToString());

            return Content(wordHTML, "text/html"); //View(wordHTML);
        }
    }
}
