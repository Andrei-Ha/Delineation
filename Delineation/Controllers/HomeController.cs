using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Delineation.Models;
using GemBox.Document;
using GemBox.Document.Tables;
using Microsoft.AspNetCore.Hosting;

namespace Delineation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string webRootPath = _webHostEnvironment.WebRootPath;
            ViewBag.pathContent = contentRootPath;
            ViewBag.pathWeb = webRootPath;
            string path_docx = webRootPath + "\\Output\\act.docx";
            string path_html = webRootPath + "\\Output\\act.html";
            string path_pdf = webRootPath + "\\Output\\act.pdf";
            ViewBag.path_docx = path_docx;
            var doc = new DocumentModel();
            var ActFont = new CharacterStyle("ActFont") { CharacterFormat = { Spacing = 1, Bold = true } };
            doc.Styles.Add(ActFont);
            doc.DefaultParagraphFormat.LineSpacing = 1;
            doc.DefaultCharacterFormat.FontName = "Times New Roman";
            doc.DefaultCharacterFormat.Size = 12;
            //---section1---//
            Section section1 = new Section(doc);
            doc.Sections.Add(section1);
            Paragraph paragraph = new Paragraph(doc) { ParagraphFormat = { Alignment = HorizontalAlignment.Center } };
            section1.Blocks.Add(paragraph);
            Run run1 = new Run(doc,"АКТ");
            Run run2 = new Run(doc, "разграничения балансовой принадлежности электросетей") { CharacterFormat = { Style = ActFont } };
            Run run3 = new Run(doc, "и эксплуатационной ответственности сторон") { CharacterFormat = { Style = ActFont } };
            paragraph.Inlines.Add(run1);
            paragraph.Inlines.Add(new SpecialCharacter(doc, SpecialCharacterType.LineBreak));
            paragraph.Inlines.Add(run2);
            paragraph.Inlines.Add(new SpecialCharacter(doc, SpecialCharacterType.LineBreak));
            paragraph.Inlines.Add(run3);
            //---table---//
            var table = new Table(doc) { TableFormat = { PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage) } };
            table.TableFormat.Borders.SetBorders(MultipleBorderTypes.All, BorderStyle.None, Color.Black, 1);
            var row = new TableRow(doc);
            table.Rows.Add(row);
            var cell_left = new TableCell(doc, new Paragraph(doc, new Run(doc, "г. Пинск")) { ParagraphFormat = { Alignment = HorizontalAlignment.Left } });
            var cell_right = new TableCell(doc, new Paragraph(doc, new Field(doc, FieldType.Date, "\\@ \"dd MMMM yyyy\"")) { ParagraphFormat = { Alignment = HorizontalAlignment.Right } });
            row.Cells.Add(cell_left);
            row.Cells.Add(cell_right);
            section1.Blocks.Add(table);
            //---
            string str_act = "\nРУП «Брестэнерго» именуемое в дальнейшем «Энергоснабжающая организация», в лице начальника Пинского городского РЭС филиала «Пинские электрические сети» РУП «Брестэнерго» Булавина Виталия Федоровича действующего на основании доверенности №3501 от 17.07.2019г. с одной стороны, и Физическое (Юридическое) лицо именуемое в дальнейшем «Потребитель», в лице Михолап Марии Николаевны  действующей(его) на основании (доверенности № ) с другой стороны составили настоящий АКТ о нижеследующем.\t";
            section1.Blocks.Add(new Paragraph(doc,str_act) { ParagraphFormat = { Alignment = HorizontalAlignment.Justify } });
            //---
            string str_ty = "На день составления Акта технические условия № 31/326 от 26.01.2019 \n " +
                "на внешнее электроснабжение объекта";
            section1.Blocks.Add(new Paragraph(doc, str_ty) { ParagraphFormat = { Alignment = HorizontalAlignment.Center } });
            //---
            string str_building = "Реконструкция нежилого помещения №2, расположенного по адресу:г. Пинск, ул. Ленина 41 под административно-торговый объект" +
                " находящийся по адресу: г. Пинск, ул. Ленина 41-2\t";
            section1.Blocks.Add(new Paragraph(doc, str_building) { ParagraphFormat = { Alignment = HorizontalAlignment.Justify } });
            //---
            string str_par = "\tРазрешенная к использованию мощность 15,0 кВт.\t\n" +
                "\tЭлектроустановки потребителя относятся к 3 категории " +
                "по надежности электроснабжения.\t\n" +
                "\tСхема внешнего электроснабжения относится к 3 категории по надежности электроснабжения.\t\n" +
                "\tЭнергоснабжающая организация не несет ответственности перед Потребителем за перерывы в электроснабжении при несоответствии схемы электроснабжения категории электроприемников Потребителя и повреждении оборудования, не находящегося у нее на балансе.\t\n" +
                "\tВ соответствии с главой 3 Правил электроснабжения границы раздела устанавливаются следующими:\t\n";
            Paragraph paragraph2 = new Paragraph(doc, str_par) { ParagraphFormat = { Alignment = HorizontalAlignment.Justify } };
            section1.Blocks.Add(paragraph2);
            //---
            section1.Blocks.Add(new Paragraph(doc,
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new Run(doc, "I. По балансовой принадлежности:") { CharacterFormat = { Style = ActFont } },
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "ВЛ-0,4 кВ от ТП-12 Л-1 оп. №13 на балансе Пинского гор. РЭС."),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "КЛ-0,23 кВ от ВЛ-0,4 кВ от ТП-12 Л-1 оп. №13 до ВРУ - нежилого помещения по ул. Ленина 41-2 и внутреннее эл. оборудование на балансе Михолап М.Н."),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "Граница раздела между Пинским гор. РЭС и Михолап М.Н. на контактном присоединении КЛ-0,23 кВ от ВЛ-0,4 кВ от ТП-12 Л-1 оп. №13"),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new Run(doc, "II. По Эксплутационной ответственности:") { CharacterFormat = { Style = ActFont } },
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "ВЛ-0,4 кВ от ТП-12 Л-1 оп. №13 на балансе Пинского гор. РЭС."),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "КЛ-0,23 кВ от ВЛ-0,4 кВ от ТП-12 Л-1 оп. №13 до ВРУ - нежилого помещения по ул. Ленина 41-2 и внутреннее эл. оборудование на балансе Михолап М.Н."),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "Граница раздела между Пинским гор. РЭС и Михолап М.Н. на контактном присоединении КЛ-0,23 кВ от ВЛ-0,4 кВ от ТП-12 Л-1 оп. №13"),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak)
                )
            { ParagraphFormat = { Alignment = HorizontalAlignment.Justify } });
            //---save---//
            doc.Save(path_docx);
            doc.Save(path_html);
            doc.Save(path_pdf);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
