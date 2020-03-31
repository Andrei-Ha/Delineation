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
using System.Net;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sql;


namespace Delineation.Controllers
{
    public class HomeController : Controller
    {
        //private string ConnectionString = "Data Source=pirr1;Persist Security Info=True;User ID=pinsk;Password=pes";
        //private string ConnStringOracle = "Data Source=//10.181.64.2/pirr2nora;User Id = pinsk;Password=pes";
        private string ConnStringOracle = "Data Source=//10.181.64.7/orcl7;User Id = sel;Password=2222";
        private string ConnStringSql = "Server=Pirr2n; database=pinskbase; User Id = ppinsk; Password=pes";
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        public IActionResult SavePNG(string png, string svg)
        {
            var decodeURL_svg = WebUtility.UrlDecode(svg);
            var base64Data_svg = decodeURL_svg.Split(',');
            string path_svg = _webHostEnvironment.WebRootPath + "\\Temp\\mypict.svg";
            using (FileStream fs = new FileStream(path_svg, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64Data_svg[1]);
                    bw.Write(data);
                }
            }
            //---
            var decodeURL_png = WebUtility.UrlDecode(png);
            var base64Data_png = decodeURL_png.Split(',');
            string path_png = _webHostEnvironment.WebRootPath + "\\Temp\\mypict.png";
            using (FileStream fs = new FileStream(path_png, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64Data_png[1]);
                    bw.Write(data);
                }
            }
            //---
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            CreateAct();
            return View();
        }
        public IActionResult Open()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            string str_result = "";
            using (OracleConnection con = new OracleConnection(ConnStringOracle))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.BindByName = true;
                    //cmd.CommandText = "select strange_str, substr(doc_code,TO_NUMBER(INSTR(doc_code,'-',1,2))+1) as NUM from VL10 where DOC_CODE=:id";
                    cmd.CommandText = "select strange_str from VL10 where substr(doc_code,TO_NUMBER(INSTR(doc_code,'-',1,2))+1)=209";
                    OracleParameter id = new OracleParameter("id", "50515405-VL10-209");
                    cmd.Parameters.Add(id);
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        str_result += reader["strange_str"];
                    }
                    reader.Dispose();
                }
            }
            /*using (SqlConnection con = new SqlConnection(ConnStringSql))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    //cmd.BindByName = true;
                    cmd.CommandText = "select NAIM from dbo.SPRPODR where KOD=54200";
                    //OracleParameter id = new OracleParameter("id", 54000);
                    //cmd.Parameters.Add(id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        str_result += reader["NAIM"];
                    }
                    reader.Dispose();
                }
            }*/
            ViewBag.strOra = str_result;
            return View();
        }
        
        public void CreateAct()
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
            Run run1 = new Run(doc, "АКТ");
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
            section1.Blocks.Add(new Paragraph(doc, str_act) { ParagraphFormat = { Alignment = HorizontalAlignment.Justify } });
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
            ///////////////////////////////////---sectiion2---
            Section section2 = new Section(doc);
            doc.Sections.Add(section2);
            ///---
            string path_pict = (_webHostEnvironment.WebRootPath + "\\output\\pict.jpg");
            Picture pict = new Picture(doc, path_pict, 160, 106, LengthUnit.Millimeter);
            Paragraph paragraph21 = new Paragraph(doc) { ParagraphFormat = { Alignment = HorizontalAlignment.Center } };
            section2.Blocks.Add(paragraph21);
            paragraph21.Inlines.Add(new Run(doc, "Схема питания электроустановки:"));
            paragraph21.Inlines.Add(new SpecialCharacter(doc, SpecialCharacterType.LineBreak));
            paragraph21.Inlines.Add(pict);
            //---
            section2.Blocks.Add(new Paragraph(doc,
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new Run(doc, "ПРИМЕЧАНИЕ"),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new Run(doc, "1.Границы по схеме обозначаются: балансовой принадлежности - красной линией; эксплуатационной ответственности - синей."),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new Run(doc, "2.При изменении срока действия Акта, присоединенных мощностей, схемы внешнего электроснабжения, категории надежности электроснабжения, границ балансовой принадлежности и эксплуатационной ответственности Акт подлежит замене."),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new Run(doc, "3.Доверенность потребителя на подписание акта разграничения хранится в энергоснабжающей организации."),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new Run(doc, "4.На схеме питания электроустановки указываются места установки приборов учета, параметры силовых и измерительных трансформаторов и ЛЭП."),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new Run(doc, "5.Потребителю запрещается без согласования с диспетчером энергоснабжающей организации самовольно производить переключения и изменять схему внешнего электроснабжения."),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new Run(doc, "6.Потребителю запрещается без согласования с энергоснабжающей организацией подключать к своим электроустановкам сторонних потребителей."),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak)
                )
            { ParagraphFormat = { Alignment = HorizontalAlignment.Justify } }
            );
            //---
            var table2 = new Table(doc) { TableFormat = { PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage) } };
            table2.TableFormat.Borders.SetBorders(MultipleBorderTypes.All, BorderStyle.None, Color.Black, 1);
            var row21 = new TableRow(doc);
            table2.Rows.Add(row21);
            var cell2_left = new TableCell(doc, new Paragraph(doc,
                new Run(doc, "Представитель энергоснабжающей организации"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "Представитель Потребителя"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "Представитель владельца"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "транзитных электрических сетей"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "Срок действия акта"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "Главный инженер"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "Зам.начальника РЭС по сбыту энерги"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "Бухгалтер РЭС")
                ));
            var cell2_center = new TableCell(doc, new Paragraph(doc,
                new Run(doc, "_____"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "_____"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "_____"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "_____"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "_____"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "_____"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "_____")
                ));
            var cell2_right = new TableCell(doc, new Paragraph(doc,
                new Run(doc, "В.Ф. Булавин"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "-//-"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "-//-"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "-//-"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "А.И. Литвинчук"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "А.М. Германович"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "Т.В. Велесницкая")
                ));
            row21.Cells.Add(cell2_left);
            row21.Cells.Add(cell2_center);
            row21.Cells.Add(cell2_right);
            section2.Blocks.Add(table2);
            //---save---//
            doc.Save(path_docx);
            doc.Save(path_html);
            doc.Save(path_pdf);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
