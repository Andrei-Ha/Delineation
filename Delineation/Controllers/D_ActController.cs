using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Delineation.Models;
using GemBox.Document;
using GemBox.Document.Tables;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
namespace Delineation.Controllers
{
    public class D_ActController : Controller
    {
        private readonly DelineationContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public D_ActController(DelineationContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Upload(int? id )
        {
            if (id != null)
            {
                //string path_jpeg = _webHostEnvironment.WebRootPath + "\\Output\\images\\" + id + ".jpeg";
                D_Act act = new D_Act() { Id = Convert.ToInt32(id) };
                return View(act);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Upload(List<IFormFile> postedFiles, int? id)
        {
            string log = "";
            string path = _webHostEnvironment.WebRootPath + "\\Output\\images\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string fileName1 = Path.GetFileName(postedFile.FileName);
                string fileName = id + "." + fileName1.Split('.')[1];
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    log += Path.Combine(path, fileName) + "/";
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                    ViewBag.Message += string.Format("<b>{0}</b> загружен.<br />", fileName1);
                }
            }
            ViewBag.xx = log;
            if (id != null)
            {
                D_Act act = new D_Act() { Id = Convert.ToInt32(id) };
                return View(act);
            }
            else
            {
                return View();
            }
        }
        // GET: D_Act
        public async Task<IActionResult> Index()
        {
            var delineationContext = _context.d_Acts.Include(d => d.Tc).ThenInclude(p => p.Res).ThenInclude(p=>p.Nach);
            return View(await delineationContext.ToListAsync());
        }

        // GET: D_Act/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Act = await _context.d_Acts
                .Include(d => d.Tc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (d_Act == null)
            {
                return NotFound();
            }

            return View(d_Act);
        }

        // GET: D_Act/Create
        public IActionResult Create()
        {
            ViewData["TcId"] = new SelectList(_context.D_Tces.OrderBy(p=>p.Date).Select(p=>new { Id = p.Id, text = "№" + p.Num + " от " + p.Date.ToString("dd.MM.yyyy") + "; " + p.FIO + "; " + p.Address }), "Id", "text");
            return View();
        }

        // POST: D_Act/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int TcId)
        {
            _context.d_Acts.Add(new D_Act { TcId = TcId });
            _context.SaveChanges();
            List<D_Act> list = _context.D_Act.ToList();
            D_Act act = list.LastOrDefault(p => p.TcId == TcId);
            return RedirectToAction(nameof(Edit), act);
        }

        // GET: D_Act/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Act = await _context.d_Acts.Include(p=>p.Tc).ThenInclude(p=>p.Res).FirstOrDefaultAsync(p=>p.Id==id);
            if (d_Act == null)
            {
                return NotFound();
            }
            /*ViewData["TcId"] = new SelectList(_context.D_Tces.OrderBy(p=>p.Date).Select(p=>new { Id = p.Id, text = p.Num + " от " + p.Date.ToString("dd.MM.yyyy") + "; " + p.FIO + "; " + p.Address }), "Id", "text", d_Act.TcId);*/
            return View(d_Act);
        }

        // POST: D_Act/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,TcId,IsEntity,EntityDoc,ConsBalance,DevBalance,ConsExpl,DevExpl,IsTransit,FIOtrans,Validity")] D_Act d_Act)
        {
            if (id != d_Act.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(d_Act);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!D_ActExists(d_Act.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            /*ViewData["TcId"] = new SelectList(_context.D_Tces.OrderBy(p=>p.Date).Select(p=>new { Id = p.Id, text = p.Num + " от " + p.Date.ToString("dd.MM.yyyy") + "; " + p.FIO + "; " + p.Address }), "Id", "text", d_Act.TcId);*/
            return View(d_Act);
        }

        // GET: D_Act/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Act = await _context.d_Acts
                .Include(d => d.Tc)
                .ThenInclude(l=>l.Res)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (d_Act == null)
            {
                return NotFound();
            }

            return View(d_Act);
        }

        // POST: D_Act/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var d_Act = await _context.d_Acts.FindAsync(id);
            _context.d_Acts.Remove(d_Act);
            await _context.SaveChangesAsync();
            //
            string webRootPath = _webHostEnvironment.WebRootPath;
            string path_docx = webRootPath + "\\Output\\docx\\" + id + ".docx";
            string path_html = webRootPath + "\\Output\\html\\" + id + ".html";
            string path_html_folder = webRootPath + "\\Output\\html\\" + id + "_files";
            string path_pdf = webRootPath + "\\Output\\pdf\\" + id + ".pdf";
            FileInfo docxToDel = new FileInfo(path_docx);
            if (docxToDel.Exists) docxToDel.Delete();
            FileInfo htmlToDel = new FileInfo(path_html);
            if (htmlToDel.Exists) htmlToDel.Delete();
            DirectoryInfo DirHtmlDel = new DirectoryInfo(path_html_folder);
            if (DirHtmlDel.Exists) DirHtmlDel.Delete(true);
            FileInfo pdfToDel = new FileInfo(path_pdf);
            if (pdfToDel.Exists) pdfToDel.Delete();
            return RedirectToAction(nameof(Index));
        }

        private bool D_ActExists(int id)
        {
            return _context.d_Acts.Any(e => e.Id == id);
        }
        public async Task<IActionResult> CreateAct(int? id)
        {
            _context.D_Persons.Load();
            D_Act act = _context.D_Act.Include(p => p.Tc).ThenInclude(o => o.Res).Where(i => i.Id == id).FirstOrDefault();
            CreateDoc(act);
            return RedirectToAction(nameof(Index));
        }
        private void CreateDoc(D_Act act)
        {
            bool IsEntity = act.IsEntity, IsTransit = act.IsTransit;
            string str_Entity = IsEntity ? "[Юридическое]" : "[Физическое]";
            string str_ConsDover = IsEntity ? "[действующ. на основании ]<" + act.EntityDoc + "> ": "[]";
            string str_Id = act.Id.ToString(),
                    str_City = "<" + act.Tc.Res.City + ">",
                    str_RESa = "<" + act.Tc.Res.RESa + ">",
                    str_RESom = "<" + act.Tc.Res.RESom + ">",
                    str_FIOnachRod = "<" + act.Tc.Res.FIOnachRod + ">",
                    str_Dover = "<" + act.Tc.Res.Dover + ">",
                    str_DateAct = "<" + act.Date.ToString("dd.MM.yyyy") + ">",
                    str_EntityDoc = "<" + act.EntityDoc + ">",
                    str_NumTc = "<" + act.Tc.Num + ">",
                    str_DateTc = "<" + act.Tc.Date.ToString("dd.MM.yyyy") + ">",
                    str_RES = "<" + act.Tc.Res.Name + ">",
                    str_Company = "<" + act.Tc.Company + ">",
                    str_FIOcons = "<" + act.Tc.FIO + ">",
                    str_ObjName = "<" + act.Tc.ObjName + ">",
                    str_Address = "<" + act.Tc.Address + ">",
                    str_Pow = "<" + act.Tc.Pow + ">",
                    str_Category = "<" + act.Tc.Category.ToString() + ">",
                    str_Point = "<" + act.Tc.Point + ">",
                    str_Pillar = "<" + act.Tc.Pillar + ">",
                    str_InvNum = "<" + act.Tc.InvNum.ToString() + ">",
                    str_ConsBalance = "<" + act.ConsBalance + ">",
                    str_DevBalance = "<" + act.DevBalance + ">",
                    str_ConsExpl = "<" + act.ConsExpl + ">",
                    str_DevExpl = "<" + act.DevExpl + ">",
                    str_FIOtrans = "<" + act.FIOtrans + ">",
                    str_Validity = "<" + act.Validity + ">",
                    str_Nach = "<" + act.Tc.Res.Nach.Surname + " " + act.Tc.Res.Nach.Name.Substring(0, 1) + "." + act.Tc.Res.Nach.Patronymic.Substring(0, 1) + "." + ">",
                    str_ZamNach = "<" + act.Tc.Res.ZamNach.Surname + " " + act.Tc.Res.ZamNach.Name.Substring(0, 1) + "." + act.Tc.Res.ZamNach.Patronymic.Substring(0, 1) + "." + ">",
                    str_GlInzh = "<" + act.Tc.Res.GlInzh.Surname + " " + act.Tc.Res.GlInzh.Name.Substring(0, 1) + "." + act.Tc.Res.GlInzh.Patronymic.Substring(0, 1) + "." + ">",
                    str_Buh = "<" + act.Tc.Res.Buh.Surname + " " + act.Tc.Res.Buh.Name.Substring(0, 1) + "." + act.Tc.Res.Buh.Patronymic.Substring(0, 1) + "." + ">";
            string[] arrCons = str_FIOcons.Split(' ');
            string str_Cons = "<" + arrCons[0] + " " + arrCons[1]?.Substring(0, 1) + "." + arrCons[2]?.Substring(0, 1) + "." + ">";
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string webRootPath = _webHostEnvironment.WebRootPath;
            ViewBag.pathContent = contentRootPath;
            ViewBag.pathWeb = webRootPath;
            string path_docx = webRootPath + "\\Output\\docx\\" + str_Id + ".docx";
            string path_html = webRootPath + "\\Output\\html\\" + str_Id + ".html";
            string path_pdf = webRootPath + "\\Output\\pdf\\" + str_Id + ".pdf";
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
            var cell_left = new TableCell(doc, new Paragraph(doc, new Run(doc, "г. " + str_City)) { ParagraphFormat = { Alignment = HorizontalAlignment.Left } });
            var cell_right = new TableCell(doc, new Paragraph(doc, new Run(doc, str_DateAct)) { ParagraphFormat = { Alignment = HorizontalAlignment.Right } });
            row.Cells.Add(cell_left);
            row.Cells.Add(cell_right);
            section1.Blocks.Add(table);
            //---
            string str_act = "\nРУП «Брестэнерго» именуемое в дальнейшем «Энергоснабжающая организация», в лице начальника " + str_RESa + " РЭС филиала «Пинские электрические сети» РУП «Брестэнерго» "+str_FIOnachRod+" действующего на основании доверенности "+str_Dover+"г. с одной стороны, и "+str_Entity+" лицо "+str_Company+" именуемое в дальнейшем «Потребитель», в лице "+str_FIOcons+" " + str_ConsDover + " с другой стороны составили настоящий АКТ о нижеследующем.\t";
            section1.Blocks.Add(new Paragraph(doc, str_act) { ParagraphFormat = { Alignment = HorizontalAlignment.Justify } });
            //---
            string str_ty = "На день составления Акта технические условия "+str_NumTc+" от "+str_DateTc+" \n " +
                "на внешнее электроснабжение объекта";
            section1.Blocks.Add(new Paragraph(doc, str_ty) { ParagraphFormat = { Alignment = HorizontalAlignment.Center } });
            //---
            string str_building = str_ObjName +
                ", находящегося по адресу: " + str_Address + " выполнены\t";
            section1.Blocks.Add(new Paragraph(doc, str_building) { ParagraphFormat = { Alignment = HorizontalAlignment.Justify } });
            //---
            string str_par = "\tРазрешенная к использованию мощность "+str_Pow+" кВт.\t\n" +
                "\tЭлектроустановки потребителя относятся к "+str_Category+" категории " +
                "по надежности электроснабжения.\t\n" +
                "\tСхема внешнего электроснабжения относится к "+str_Category+" категории по надежности электроснабжения.\t\n" +
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
                new Run(doc, str_Point + " оп. №"+str_Pillar+" на балансе "+str_RESa+" РЭС."),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, str_ConsBalance +" от " + str_Point + " оп. №" + str_Pillar + " до ВРУ - ??? по ул. Ленина 41-2 и внутреннее эл. оборудование на балансе Потребителя"),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "Граница раздела между "+str_RESom+" РЭС и "+str_FIOcons+" "+str_DevBalance+" от "+str_Point+" оп. №" + str_Pillar),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new Run(doc, "II. По Эксплутационной ответственности:") { CharacterFormat = { Style = ActFont } },
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, str_Point + " оп. №" + str_Pillar + " на балансе " + str_RESa + " РЭС."),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, str_ConsExpl +" от "+ str_Point + " оп. №" + str_Pillar+" до ВРУ - ??? по ул. Ленина 41-2 и внутреннее эл. оборудование на балансе Потребителя"),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "Граница раздела между " + str_RESom + " РЭС и " + str_FIOcons + " "+str_DevExpl+" от " + str_Point + " оп. №" + str_Pillar),
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
                new Run(doc, str_Validity),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "_____"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "_____"),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "_____")
                ));
            var cell2_right = new TableCell(doc, new Paragraph(doc,
                new Run(doc, str_Nach),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, str_Cons),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, str_FIOtrans),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, ""),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, str_GlInzh),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, str_ZamNach),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, str_Buh)
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
    }
}
